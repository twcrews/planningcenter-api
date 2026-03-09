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

    private DocumentationTransforms.ExcludedVertexEntry? FindExcludedVertex(
        ProductDefinition product, string? versionId, string? vertexId)
    {
        string productName = product.ToString();

        return overrides.Value.ExcludedVertices.FirstOrDefault(e =>
            MatchesVertex(e, productName, versionId, vertexId));
    }

    private static bool MatchesVertex(
        DocumentationTransforms.ExcludedVertexEntry entry,
        string productName,
        string? versionId,
        string? vertexId)
    {
        return (entry.Product is null || entry.Product.Equals(productName, StringComparison.OrdinalIgnoreCase))
            && (entry.Version is null || entry.Version == versionId)
            && entry.Vertex.Equals(vertexId, StringComparison.OrdinalIgnoreCase);
    }

    private bool IsResourceGenerationExcluded(ProductDefinition product, string? versionId, string? vertexId)
    {
        DocumentationTransforms.ExcludedVertexEntry? excludedVertex
            = FindExcludedVertex(product, versionId, vertexId);
        return excludedVertex?.ShouldGenerateResource == false;
    }

    private bool IsClientGenerationExcluded(ProductDefinition product, string? versionId, string? vertexId)
    {
        DocumentationTransforms.ExcludedVertexEntry? excludedVertex
            = FindExcludedVertex(product, versionId, vertexId);
        return excludedVertex?.ShouldGenerateClients == false;
    }

    private bool? GetCollectionOverride(ProductDefinition product, string? versionId, string? vertexId, string edge)
    {
        string productName = product.ToString();

        DocumentationTransforms.CollectionOverrideEntry? overrideEntry = overrides
            .Value.CollectionOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(
                productName, StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertexId, StringComparison.OrdinalIgnoreCase))
                && e.Edge.Equals(edge, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying collection override for {Product}.{Version}.{Vertex}.{Edge}: {IsCollection}",
                product, versionId, vertexId, edge, overrideEntry.IsCollection);
            return overrideEntry.IsCollection;
        }

        return null;
    }

    private DocumentationTransforms.VertexNameOverrideEntry? FindNameOverride(
        ProductDefinition product, string? versionId, string? vertexId)
    {
        string productName = product.ToString();

        return overrides.Value.VertexNameOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(
                productName, StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertexId, StringComparison.OrdinalIgnoreCase)));
    }

    private string? GetModelNameOverride(ProductDefinition product, string? versionId, string? vertexId)
    {
        DocumentationTransforms.VertexNameOverrideEntry? overrideEntry =
            FindNameOverride(product, versionId, vertexId);

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying model name override for {Product}.{Version}.{Vertex}: {ModelName}",
                product, versionId, vertexId, overrideEntry.ClrModelName);
            return overrideEntry.ClrModelName;
        }

        return null;
    }

    private string? GetResourceNameOverride(ProductDefinition product, string? versionId, string? vertexId)
    {
        DocumentationTransforms.VertexNameOverrideEntry? overrideEntry =
            FindNameOverride(product, versionId, vertexId);

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying resource name override for {Product}.{Version}.{Vertex}: {ResourceName}",
                product, versionId, vertexId, overrideEntry.ClrResourceName);
            return overrideEntry.ClrResourceName;
        }

        return null;
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

        
        _logger.LogTrace("Building resource for vertex with ID: {VertexId}", vertexId);
        return new()
        {
            JsonName = vertexId,
            AttributesClrType = GetModelNameOverride(product, versionId, vertexId) ?? vertexName.ToPascalCase(),
            ResourceClrType = GetResourceNameOverride(product, versionId, vertexId)
                ?? vertexName.ToPascalCase() + "Resource",
            Description = vertexDescription,
            Deprecated = vertexDeprecated,
            CollectionOnly = vertexCollectionOnly,
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
            Children = vertex.Relationships?.OutboundEdges.Data
                .Select(edge => BuildChild(edge, product, versionId, vertexId)) ?? [],
            Postable = vertex.Relationships?.Permissions.Data.Attributes.CanCreate ?? false,
            Patchable = vertex.Relationships?.Permissions.Data.Attributes.CanUpdate ?? false,
            Deletable = vertex.Relationships?.Permissions.Data.Attributes.CanDestroy ?? false,
            ShouldGenerateClients = !IsClientGenerationExcluded(product, versionId, vertexId),
            ShouldGenerateResource = !IsResourceGenerationExcluded(product, versionId, vertexId)
        };
    }

    private ResourceAttribute BuildAttribute(ProductDefinition product, string versionId, string vertex, Models.Attribute attribute)
    {
        string type = attribute.TypeAnnotation.Name;

        DocumentationTransforms.AttributeTypeOverrideEntry? overrideEntry = overrides.Value.AttributeTypeOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Attribute.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying attribute type override for {Product}.{Version}.{Vertex}.{Attribute}: {Type}",
                product, versionId, vertex, attribute.Name, overrideEntry.ClrType);
            type = overrideEntry.ClrType;
        }

        string? converter = null;

        DocumentationTransforms.AttributeJsonConverterEntry? converterEntry = overrides.Value.AttributeJsonConverters
            .FirstOrDefault(c => (c.Product is null || c.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (c.Version is null || c.Version == versionId)
                && (c.Vertex is null || c.Vertex.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && c.Attribute.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase));

        if (converterEntry is not null)
        {
            _logger.LogInformation(
                "Applying attribute JSON converter for {Product}.{Version}.{Vertex}.{Attribute}: {Converter}",
                product, versionId, vertex, attribute.Name, converterEntry.Converter);
            converter = converterEntry.Converter;
        }

        _logger.LogTrace("Building attribute: {AttributeName}", attribute.Name);
        return new()
        {
            JsonName = attribute.Name,
            ClrName = attribute.Name.ToPascalCase(),
            ClrType = type.ToClrType(),
            JsonConverter = converter,
            Description = attribute.Description ?? "Planning Center does not provide a description for this attribute."
        };
    }

    private ResourceRelationship BuildRelationship(ProductDefinition product, string versionId, string vertex, Relationship relationship)
    {
        _logger.LogTrace("Building relationship: {RelationshipName}", relationship.Name);

        string resourceType = relationship.GraphType + "Resource";
        DocumentationTransforms.RelationshipTypeOverrideEntry? overrideEntry = overrides.Value.RelationshipTypeOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product, StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Relationship.Equals(relationship.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying relationship type override for {Product}.{Version}.{Vertex}.{Relationship}: {Type}",
                product, versionId, vertex, relationship.Name, overrideEntry.ClrType);
            resourceType = overrideEntry.ClrType;
        }

        return new()
        {
            JsonName = relationship.Name,
            ClrName = relationship.Name.ToPascalCase(),
            ClrAttributesType = relationship.GraphType,
            ClrResourceType = resourceType,
            IsCollection = relationship.Association == "to_many",
            Description = $"Related `{relationship.Name.ToPascalCase()}` resource."
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

        string slug = new Uri(edge.Attributes.Path).Segments[^1];
        bool isCollection = GetCollectionOverride(product, versionId, vertex, edge.Attributes.Name)
            ?? edge.Attributes.Name.IsPlural();
        IEnumerable<ResourceChildFilter> filters = edge.Attributes.Scopes
            .Select(s => new ResourceChildFilter 
            { 
                ClrMethodName = $"FilterBy{s.Name.ToPascalCase()}", 
                Value = s.Name, 
                Description = s.ScopeHelp ?? $"Filter results by the `{s.Name.ToPascalCase()}` scope." 
            });

        string? jsonType = edge.Relationships.Head.Data.Id 
            ?? throw new InvalidOperationException($"Edge '{edge.Attributes.Name}' is missing an ID property.");
        string type = jsonType.ToPascalCase();

        DocumentationTransforms.EdgeTypeOverrideEntry? overrideEntry = overrides.Value.EdgeTypeOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Edge.Equals(edge.Attributes.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying edge type override for {Product}.{Version}.{Vertex}.{Edge}: {Type}",
                product, versionId, vertex, edge.Attributes.Name, overrideEntry.ClrType);
            type = overrideEntry.ClrType;
        }

        return new()
        {
            JsonName = edge.Attributes.Name,
            ClrName = edge.Attributes.Name.ToPascalCase(),
            Description = $"Associated `{edge.Attributes.Name.ToPascalCase()}`.",
            Slug = slug,
            Filters = filters,
            IsCollection = isCollection,
            Deprecated = edge.Attributes.Deprecated,
            ClrAttributesType = type
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
}
