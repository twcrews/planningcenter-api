using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Extensions;
using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Models.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.DocParser.Services;

class DocumentationBuilder(
    ILogger<DocumentationBuilder> logger,
    IPlanningCenterClient client,
    IOptions<AppSettings.DocumentationBuilderOptions> options,
    IOptions<DocumentationTransforms> overrides)
    : IDocumentationBuilder
{
    private readonly SemaphoreSlim _semaphore = new(options.Value.ConcurrentConnections);
    private readonly ILogger<DocumentationBuilder> _logger = logger;
    private readonly IPlanningCenterClient _client = client;

    public async Task<IEnumerable<Product>> BuildAllProductsAsync()
    {
        _logger.LogDebug("Building documentation for all products");

        Task<Product>[] productTasks = [.. ProductDefinition.All
            .Select(BuildProductAsync)];

        return await Task.WhenAll(productTasks);
    }

    public async Task<Product> BuildProductAsync(ProductDefinition product)
    {
        _logger.LogDebug("Building documentation for product: {ProductName}", product);

        await _semaphore.WaitAsync();
        GraphDocument graphDocument;
        try
        {
            graphDocument = await _client.GetGraphAsync(product);
        }
        finally
        {
            _semaphore.Release();
        }

        Task<Api.Models.Version>[] versionTasks =
        [
            .. graphDocument.Data.Relationships.Versions.Data
            .Select(version => BuildVersionAsync(product, version))
        ];

        Api.Models.Version[] versions = await Task.WhenAll(versionTasks);

        string title = graphDocument.Data.Attributes.Title ?? product.ToString();

        return new()
        {
            Name = product,
            Description = graphDocument.Data.Attributes.Description,
            Versions = versions
        };
    }

    private async Task<Api.Models.Version> BuildVersionAsync(ProductDefinition product, VersionResource version)
    {
        _logger.LogTrace("Building version with ID: {VersionId}", version.Id);

        await _semaphore.WaitAsync();
        GraphVersionDocument versionDocument;

        string versionId = version.Id ?? throw new InvalidOperationException("Version resource is missing an ID.");

        try
        {
            versionDocument = await _client.GetGraphVersionAsync(product, versionId);
        }
        finally
        {
            _semaphore.Release();
        }

        Task<Resource>[] resourceTasks =
        [
            .. versionDocument.Data.Relationships.Vertices.Data
            .Select(versionVertex => BuildResourceAsync(product, versionId, versionVertex))
        ];

        Resource[] resources = await Task.WhenAll(resourceTasks);

        string versionDocumentId = versionDocument.Data.Id ?? throw new InvalidOperationException("Version document is missing an ID.");

        return new()
        {
            Id = versionDocumentId,
            Beta = versionDocument.Data.Attributes.Beta,
            Details = versionDocument.Data.Attributes.Details,
            Resources = resources
        };
    }

    private async Task<Resource> BuildResourceAsync(
        ProductDefinition product, string versionId, VertexResource versionVertex)
    {
        await _semaphore.WaitAsync();
        VertexDocument vertexDocument;

        string versionVertexId = versionVertex.Id ?? throw new InvalidOperationException("Version vertex is missing an ID.");

        try
        {
            vertexDocument = await _client.GetVertexAsync(product, versionId, versionVertexId);
        }
        finally
        {
            _semaphore.Release();
        }

        return BuildResource(vertexDocument.Data, product, versionId);
    }

    private Resource BuildResource(VertexResource vertex, ProductDefinition product, string versionId)
    {
        string vertexId = vertex.Id ?? throw new InvalidOperationException("Vertex is missing an ID.");
        string vertexName = vertex.Attributes?.Name ?? throw new InvalidOperationException("Vertex is missing a name attribute.");
        string vertexDescription = vertex.Attributes.Description ?? "Planning Center does not provide a description for this resource.";
        bool vertexDeprecated = vertex.Attributes?.Deprecated ?? false;
        bool vertexCollectionOnly = vertex.Attributes?.CollectionOnly ?? false;

        DocumentationTransforms.ResourcePropertyOverrideEntry? overrideEntry = overrides.Value.ResourceOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && e.Resource.Equals(vertexId, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogDebug(
                "Applying resource property overrides for {Product}.{Version}.{Vertex}", product, versionId, vertexId);
        }
        
        _logger.LogTrace("Building resource for vertex with ID: {VertexId}", vertexId);
        return new()
        {
            JsonName = overrideEntry?.JsonName ?? vertexId,
            AttributesClrType = overrideEntry?.AttributesClrType ?? vertexName.ToPascalCase(),
            ResourceClrType = overrideEntry?.ResourceClrType ?? vertexName.ToPascalCase() + "Resource",
            Description = overrideEntry?.Description ?? vertexDescription,
            Deprecated = overrideEntry?.Deprecated ?? vertexDeprecated,
            CollectionOnly = overrideEntry?.CollectionOnly ?? vertexCollectionOnly,
            Postable = overrideEntry?.Postable ?? vertex.Relationships?.Permissions.Data.Attributes.CanCreate ?? false,
            Patchable = overrideEntry?.Patchable ?? vertex.Relationships?.Permissions.Data.Attributes.CanUpdate ?? false,
            Deletable = overrideEntry?.Deletable ?? vertex.Relationships?.Permissions.Data.Attributes.CanDestroy ?? false,
            ShouldGenerateClients = overrideEntry?.ShouldGenerateClients ?? true,
            ShouldGenerateResource = overrideEntry?.ShouldGenerateResource ?? true,

            Attributes = vertex.Relationships?.Attributes.Data
                .Where(attr => attr.Attributes.Name != "id")
                .Select(attr => BuildAttribute(product, versionId, vertexId, attr.Attributes)) ?? [],
            Relationships = vertex.Relationships?.Relationships.Data
                .Select(rel => BuildRelationship(product, versionId, vertexId, rel.Attributes)) ?? [],
            Actions = vertex.Relationships?.Actions.Data
                .Select(act => BuildAction(act.Attributes)) ?? [],
            CanInclude = vertex.Relationships?.CanInclude.Data
                .Select(inc => BuildIncludable(inc.Attributes))
                .DistinctBy(i => i.Value) ?? [],
            CanOrderBy = vertex.Relationships?.CanOrder.Data
                .Select(ord => BuildOrderable(ord.Attributes))
                .DistinctBy(o => o.Value) ?? [],
            CanQueryBy = vertex.Relationships?.CanQuery.Data
                .Select(qry => BuildQueryable(qry.Attributes))
                .DistinctBy(q => q.Parameter) ?? [],
            Children = (vertex.Relationships?.OutboundEdges.Data
                .Select(edge => BuildChild(edge, product, versionId, vertexId)) ?? [])
                .Concat(GetAdditionalResourceChildren(product, versionId, vertexId))
        };
    }

    private ResourceAttribute BuildAttribute(ProductDefinition product, string versionId, string vertex, Models.Attribute attribute)
    {
        string type = attribute.TypeAnnotation.Name;

        DocumentationTransforms.AttributePropertyOverrideEntry? overrideEntry = overrides.Value.AttributeOverrides
            .FirstOrDefault(e => 
                (e.Product is null || e.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Resource is null || e.Resource.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Attribute.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogDebug("Applying attribute overrides for {Product}.{Version}.{Vertex}.{Attribute}",
                product, versionId, vertex, attribute.Name);
        }

        _logger.LogTrace("Building attribute: {AttributeName}", attribute.Name);
        return new()
        {
            JsonName = overrideEntry?.JsonName ?? attribute.Name,
            ClrName = overrideEntry?.ClrName ?? attribute.Name.ToPascalCase(),
            ClrType = overrideEntry?.ClrType ?? type.ToClrType(),
            JsonConverter = overrideEntry?.JsonConverter,
            Description = overrideEntry?.Description ?? attribute.Description 
                ?? "Planning Center does not provide a description for this attribute."
        };
    }

    private ResourceRelationship BuildRelationship(ProductDefinition product, string versionId, string vertex, Relationship relationship)
    {
        _logger.LogTrace("Building relationship: {RelationshipName}", relationship.Name);

        string resourceType = relationship.GraphType + "Resource";
        DocumentationTransforms.RelationshipPropertyOverrideEntry? overrideEntry = overrides.Value.RelationshipOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product, StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Resource is null || e.Resource.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Relationship.Equals(relationship.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogDebug(
                "Applying relationship overrides for {Product}.{Version}.{Vertex}.{Relationship}",
                product, versionId, vertex, relationship.Name);
        }

        return new()
        {
            JsonName = overrideEntry?.JsonName ?? relationship.Name,
            ClrName = overrideEntry?.ClrName ?? relationship.Name.ToPascalCase(),
            AttributesClrType = overrideEntry?.AttributesClrType ?? relationship.GraphType,
            ResourceClrType = overrideEntry?.ResourceClrType ?? resourceType,
            IsCollection = overrideEntry?.IsCollection == "true" || relationship.Association == "to_many",
            Description = overrideEntry?.Description ?? $"Related `{relationship.Name.ToPascalCase()}` resource."
        };
    }

    private ResourceIncludable BuildIncludable(UrlParameter parameter)
    {
        _logger.LogTrace("Building includable parameter: {ParameterName}", parameter.Parameter);
        string value = parameter.Value ?? throw new InvalidOperationException($"Includable parameter '{parameter.Parameter}' is missing a value.");

        return new()
        {
            ClrMethodName = $"Include{parameter.Name.ToPascalCase()}",
            Value = value,
            Description = $"Include related `{parameter.Name.ToPascalCase()}` resources in the response.",
            CanAssignOnCreate = parameter.CanAssignOnCreate ?? false,
            CanAssignOnUpdate = parameter.CanAssignOnUpdate ?? false
        };
    }

    private ResourceOrderable BuildOrderable(UrlParameter parameter)
    {
        _logger.LogTrace("Building orderable parameter: {ParameterName}", parameter.Parameter);
        string value = parameter.Value ?? throw new InvalidOperationException($"Orderable parameter '{parameter.Parameter}' is missing a value.");

        return new()
        {
            ClrMethodName = $"OrderBy{parameter.Name.Replace(".", "_").ToPascalCase()}",
            Value = value,
            Description = $"Sort response items by the `{parameter.Name.ToPascalCase()}` attribute."
        };
    }

    private ResourceQueryable BuildQueryable(UrlParameter parameter)
    {
        _logger.LogTrace("Building queryable parameter: {ParameterName}", parameter.Parameter);
        return new()
        {
            ClrMethodName = $"Where{parameter.Name.ToPascalCase()}",
            Parameter = parameter.Parameter,
            ClrType = parameter.Type.ToClrType(),
            Description = $"Query response items by the `{parameter.Name.ToPascalCase()}` attribute."
        };
    }

    private ResourceChild BuildChild(EdgeResource edge, ProductDefinition product, string versionId, string vertex)
    {
        _logger.LogTrace("Building associated resource: {EdgeName}", edge.Attributes.Name);


        IEnumerable<ResourceChildFilter> filters = edge.Attributes.Scopes.Select(s => new ResourceChildFilter 
            { 
                ClrMethodName = $"FilterBy{s.Name.ToPascalCase()}", 
                Value = s.Name, 
                Description = s.ScopeHelp ?? $"Filter results by the `{s.Name.ToPascalCase()}` scope." 
            });
            

        DocumentationTransforms.ResourceChildPropertyOverrideEntry? overrideEntry = overrides.Value.ResourceChildOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Resource is null || e.Resource.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Child.Equals(edge.Attributes.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogDebug("Applying edge overrides for {Product}.{Version}.{Vertex}.{Edge}",
                product, versionId, vertex, edge.Attributes.Name);
        }

        return new()
        {
            JsonName = overrideEntry?.JsonName ?? edge.Attributes.Name,
            ClrName = overrideEntry?.ClrName ?? edge.Attributes.Name.ToPascalCase(),
            AttributesClrType = overrideEntry?.AttributesClrType 
                ?? edge.Relationships.Head.Data.Attributes?.Name.ToPascalCase() 
                ?? throw new InvalidOperationException(
                    $"Edge '{edge.Attributes.Name}' is missing an attributes property on its head vertex."),

            Description = overrideEntry?.Description ?? $"Associated `{edge.Attributes.Name.ToPascalCase()}`.",
            Slug = overrideEntry?.Slug ?? new Uri(edge.Attributes.Path).Segments[^1],

            // FIXME: The logic for determining whether an edge represents a collection is currently based on the
            // presence of "plural" edge names in the API documentation and overrides. This is not a reliable method. We
            // may be able to determine this more reliably by inspecting the structure of the referenced vertices.
            IsCollection = overrideEntry?.IsCollection ?? edge.Attributes.Name.IsPlural(),

            Deprecated = overrideEntry?.Deprecated ?? edge.Attributes.Deprecated,
            Filters = filters,
        };
    }

    private ResourceAction BuildAction(Models.Action action)
    {
        _logger.LogTrace("Building action: {ActionName}", action.Name);

        return new()
        {
            JsonName = action.Name,
            ClrMethodName = action.Name.ToPascalCase() + "Async",

            // FIXME: At time of writing, `Action.Name` is equal to the last segment of the path in every case. 
            // Replace the following with parsing logic on `Action.Path` if this changes.
            Path = action.Name,
            Requirements = action.CanRun,
            Description = action.Description ?? "Planning Center does not provide a description for this action.",
            AdditionalDetails = action.Details,
            Deprecated = action.Deprecated
        };
    }

    private IEnumerable<ResourceChild> GetAdditionalResourceChildren(
        ProductDefinition product, string versionId, string vertexId)
    {
        string productName = product.ToString();
        foreach (DocumentationTransforms.AdditionalResourceChildEntry entry in overrides.Value.AdditionalResourceChildren
            .Where(e => (e.Product is null || e.Product.Equals(productName, StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && e.Resource.Equals(vertexId, StringComparison.OrdinalIgnoreCase)))
        {
            _logger.LogDebug("Adding outbound edge for {Product}.{Version}.{Vertex}: {EdgeName}",
                product, versionId, vertexId, entry.Child.JsonName);
            yield return entry.Child;
        }
    }
}
