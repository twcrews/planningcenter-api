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
    IOptions<AppSettings.DocumentationBuilderOptions> options) 
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

        return new()
        {
            Name = product,
            Title = graphDocument.Data.Attributes.Title!,
            Description = graphDocument.Data.Attributes.Description,
            Versions = versions
        };
    }

    private async Task<Api.Models.Version> BuildVersionAsync(ProductDefinition product, VersionResource version)
    {
        _logger.LogTrace("Building version with ID: {VersionId}", version.Id);

        await _semaphore.WaitAsync();
        GraphVersionDocument versionDocument;
        try
        {
            versionDocument = await _client.GetGraphVersionAsync(product, version.Id!);
        }
        finally
        {
            _semaphore.Release();
        }

        Task<Resource>[] resourceTasks =
        [
            .. versionDocument.Data.Relationships.Vertices.Data
            .Select(versionVertex => BuildResourceAsync(product, version.Id!, versionVertex))
        ];

        Resource[] resources = await Task.WhenAll(resourceTasks);

        return new()
        {
            Id = versionDocument.Data.Id!,
            Beta = versionDocument.Data.Attributes.Beta,
            Details = versionDocument.Data.Attributes.Details,
            Resources = resources
        };
    }

    private AppSettings.DocumentationBuilderOptions.ExcludedVertexEntry? FindExcludedVertex(
        ProductDefinition product, string? versionId, string? vertexId)
    {
        string productName = product.ToString();

        return options.Value.ExcludedVertices.FirstOrDefault(e =>
            MatchesVertex(e, productName, versionId, vertexId));
    }

    private static bool MatchesVertex(
        AppSettings.DocumentationBuilderOptions.ExcludedVertexEntry entry,
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
        AppSettings.DocumentationBuilderOptions.ExcludedVertexEntry? excludedVertex 
            = FindExcludedVertex(product, versionId, vertexId);
        return excludedVertex?.GenerateResource == false;
    }

    private bool IsClientGenerationExcluded(ProductDefinition product, string? versionId, string? vertexId)
    {
        AppSettings.DocumentationBuilderOptions.ExcludedVertexEntry? excludedVertex 
            = FindExcludedVertex(product, versionId, vertexId);
        return excludedVertex?.GenerateClients == false;
    }

    private bool? GetCollectionOverride(ProductDefinition product, string? versionId, string? vertexId, string edge)
    {
        string productName = product.ToString();

        AppSettings.DocumentationBuilderOptions.CollectionOverrideEntry? overrideEntry = options
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

    private AppSettings.DocumentationBuilderOptions.NameOverrideEntry? FindNameOverride(
        ProductDefinition product, string? versionId, string? vertexId)
    {
        string productName = product.ToString();

        return options.Value.NameOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(
                productName, StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertexId, StringComparison.OrdinalIgnoreCase)));
    }

    private string? GetModelNameOverride(ProductDefinition product, string? versionId, string? vertexId)
    {
        AppSettings.DocumentationBuilderOptions.NameOverrideEntry? overrideEntry =
            FindNameOverride(product, versionId, vertexId);

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying model name override for {Product}.{Version}.{Vertex}: {ModelName}",
                product, versionId, vertexId, overrideEntry.ModelName);
            return overrideEntry.ModelName;
        }

        return null;
    }

    private string? GetResourceNameOverride(ProductDefinition product, string? versionId, string? vertexId)
    {
        AppSettings.DocumentationBuilderOptions.NameOverrideEntry? overrideEntry =
            FindNameOverride(product, versionId, vertexId);

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying resource name override for {Product}.{Version}.{Vertex}: {ResourceName}",
                product, versionId, vertexId, overrideEntry.ResourceName);
            return overrideEntry.ResourceName;
        }

        return null;
    }

    private async Task<Resource> BuildResourceAsync(
        ProductDefinition product, string versionId, VertexResource versionVertex)
    {
        await _semaphore.WaitAsync();
        VertexDocument vertexDocument;
        try
        {
            vertexDocument = await _client.GetVertexAsync(product, versionId, versionVertex.Id!);
        }
        finally
        {
            _semaphore.Release();
        }

        return BuildResource(vertexDocument.Data, product, versionId);
    }

    private Resource BuildResource(VertexResource vertex, ProductDefinition product, string versionId)
    {
        _logger.LogTrace("Building resource for vertex with ID: {VertexId}", vertex.Id);
        return new()
        {
            Id = vertex.Id!,
            Name = GetModelNameOverride(product, versionId, vertex.Id!) ?? vertex.Attributes!.Name!,
            ResourceName = GetResourceNameOverride(product, versionId, vertex.Id!) 
                ?? vertex.Attributes!.Name!.ToPascalCase() + "Resource",
            Description = vertex.Attributes!.Description,
            Deprecated = vertex.Attributes!.Deprecated,
            CollectionOnly = vertex.Attributes!.CollectionOnly,
            Attributes = vertex.Relationships!.Attributes.Data
                .Where(attr => attr.Attributes.Name != "id")
                .Select(attr => BuildAttribute(attr.Attributes)),
            Relationships = vertex.Relationships.Relationships.Data.Select(rel => BuildRelationship(rel.Attributes)),
            CanInclude = vertex.Relationships.CanInclude.Data
                .Select(inc => BuildIncludable(inc.Attributes))
                .DistinctBy(i => i.Parameter),
            CanOrderBy = vertex.Relationships.CanOrder.Data
                .Select(ord => BuildOrderable(ord.Attributes))
                .DistinctBy(o => o.Parameter),
            CanQueryBy = vertex.Relationships.CanQuery.Data
                .Select(qry => BuildQueryable(qry.Attributes))
                .DistinctBy(q => q.Parameter),
            Children = vertex.Relationships.OutboundEdges.Data
                .Select(edge => BuildChild(edge, product, versionId, vertex.Id!)),
            Postable = vertex.Relationships.Permissions.Data.Attributes.CanCreate,
            Patchable = vertex.Relationships.Permissions.Data.Attributes.CanUpdate,
            Deletable = vertex.Relationships.Permissions.Data.Attributes.CanDestroy,
            GenerateClients = !IsClientGenerationExcluded(product, versionId, vertex.Id!),
            GenerateResource = !IsResourceGenerationExcluded(product, versionId, vertex.Id!)
        };
    }

    private ResourceAttribute BuildAttribute(Models.Attribute attribute)
    {
        _logger.LogTrace("Building attribute: {AttributeName}", attribute.Name);
        return new()
        {
            Name = attribute.Name,
            Type = attribute.TypeAnnotation.Name,
            Description = attribute.Description
        };
    }

    private ResourceRelationship BuildRelationship(Relationship relationship)
    {
        _logger.LogTrace("Building relationship: {RelationshipName}", relationship.Name);
        return new()
        {
            Name = relationship.Name,
            Type = relationship.GraphType,
            AssociationType = relationship.Association,
            Note = relationship.Note
        };
    }

    private ResourceIncludable BuildIncludable(UrlParameter parameter)
    {
        _logger.LogTrace("Building includable parameter: {ParameterName}", parameter.Parameter);
        return new()
        {
            Parameter = parameter.Parameter,
            Value = parameter.Value!,
            Description = parameter.Description,
            CanAssignOnCreate = parameter.CanAssignOnCreate ?? false,
            CanAssignOnUpdate = parameter.CanAssignOnUpdate ?? false
        };
    }

    private ResourceOrderable BuildOrderable(UrlParameter parameter)
    {
        _logger.LogTrace("Building orderable parameter: {ParameterName}", parameter.Parameter);
        return new()
        {
            Parameter = parameter.Parameter,
            Value = parameter.Value!,
            Type = parameter.Type,
            Description = parameter.Description
        };
    }

    private ResourceQueryable BuildQueryable(UrlParameter parameter)
    {
        _logger.LogTrace("Building queryable parameter: {ParameterName}", parameter.Parameter);
        return new()
        {
            Name = parameter.Name,
            Parameter = parameter.Parameter,
            Type = parameter.Type,
            Description = parameter.Description,
            Example = parameter.Example
        };
    }

    private ResourceChild BuildChild(EdgeResource edge, ProductDefinition product, string versionId, string vertex)
    {
        _logger.LogTrace("Building associated resource: {EdgeName}", edge.Attributes.Name);

        string slug = new Uri(edge.Attributes.Path).Segments[^1];
        bool isCollection = GetCollectionOverride(product, versionId, vertex, edge.Attributes.Name) 
            ?? edge.Attributes.Name.IsPlural();
        IEnumerable<ResourceChildFilter> filters = edge.Attributes.Scopes
            .Select(s => new ResourceChildFilter { Name = s.Name, Description = s.ScopeHelp });

        string type = edge.Relationships.Head.Data.Id!;
        
        if ("services".Equals(product.ToString(), StringComparison.OrdinalIgnoreCase) 
            && vertex.Equals("organization", StringComparison.OrdinalIgnoreCase)
            && edge.Attributes.Name.Equals("plans", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogDebug("Plans");
        }

        AppSettings.DocumentationBuilderOptions.EdgeTypeOverrideEntry? overrideEntry = options.Value.EdgeTypeOverrides
            .FirstOrDefault(e => (e.Product is null || e.Product.Equals(product.ToString(), StringComparison.OrdinalIgnoreCase))
                && (e.Version is null || e.Version == versionId)
                && (e.Vertex is null || e.Vertex.Equals(vertex, StringComparison.OrdinalIgnoreCase))
                && e.Edge.Equals(edge.Attributes.Name, StringComparison.OrdinalIgnoreCase));

        if (overrideEntry is not null)
        {
            _logger.LogInformation(
                "Applying edge type override for {Product}.{Version}.{Vertex}.{Edge}: {Type}",
                product, versionId, vertex, edge.Attributes.Name, overrideEntry.Type);
            type = overrideEntry.Type;
        }

        return new()
        {
            Name = edge.Attributes.Name,
            Description = edge.Attributes.Details,
            Slug = slug,
            Filters = filters,
            IsCollection = isCollection,
            IsDeprecated = edge.Attributes.Deprecated,
            Type = type
        };
    }
}
