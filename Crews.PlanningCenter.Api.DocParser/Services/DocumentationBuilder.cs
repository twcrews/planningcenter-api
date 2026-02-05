using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Extensions;
using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.Models;
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
            .Where(p => !options.Value.ExcludedProducts.Contains(p.ToString()))
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
            .Where(version => !options.Value.ExcludedVersions.Any(e => product.ToString().Equals(
                e.Product, StringComparison.OrdinalIgnoreCase) && e.Version == version.Id)
                && !options.Value.ExcludedVersions.Any(e => e.Product == null && e.Version == version.Id))
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
            .Where(versionVertex => !options.Value.ExcludedVertices.Any(e => product.ToString().Equals(e.Product, StringComparison.OrdinalIgnoreCase)
                    && e.Version == version.Id && e.Vertex.Equals(versionVertex.Id, StringComparison.OrdinalIgnoreCase))
                && !options.Value.ExcludedVertices.Any(e => product.ToString().Equals(e.Product, StringComparison.OrdinalIgnoreCase)
                    && e.Version == null && e.Vertex.Equals(versionVertex.Id, StringComparison.OrdinalIgnoreCase))
                && !options.Value.ExcludedVertices.Any(e => e.Product == null 
                    && e.Version == version.Id && e.Vertex.Equals(versionVertex.Id, StringComparison.OrdinalIgnoreCase))
                && !options.Value.ExcludedVertices.Any(e => e.Product == null 
                    && e.Version == null && e.Vertex.Equals(versionVertex.Id, StringComparison.OrdinalIgnoreCase)))
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

        return BuildResource(vertexDocument.Data);
    }

    private Resource BuildResource(VertexResource vertex)
    {
        _logger.LogTrace("Building resource for vertex with ID: {VertexId}", vertex.Id);
        return new()
        {
            Id = vertex.Id!,
            Name = vertex.Attributes!.Name!,
            Description = vertex.Attributes.Description,
            Deprecated = vertex.Attributes.Deprecated,
            CollectionOnly = vertex.Attributes.CollectionOnly,
            Attributes = vertex.Relationships!.Attributes.Data
                .Where(attr => attr.Attributes.Name != "id")
                .Select(attr => BuildAttribute(attr.Attributes)),
            Relationships = vertex.Relationships.Relationships.Data.Select(rel => BuildRelationship(rel.Attributes)),
            CanInclude = vertex.Relationships.CanInclude.Data.Select(inc => BuildIncludable(inc.Attributes)),
            CanOrderBy = vertex.Relationships.CanOrder.Data.Select(ord => BuildOrderable(ord.Attributes)),
            CanQueryBy = vertex.Relationships.CanQuery.Data.Select(qry => BuildQueryable(qry.Attributes)),
            Children = vertex.Relationships.OutboundEdges.Data.Select(BuildChild),
            Postable = vertex.Relationships.Permissions.Data.Attributes.CanCreate,
            Patchable = vertex.Relationships.Permissions.Data.Attributes.CanUpdate,
            Deletable = vertex.Relationships.Permissions.Data.Attributes.CanDestroy
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

    private ResourceChild BuildChild(EdgeResource edge)
    {
        _logger.LogTrace("Building associated resource: {EdgeName}", edge.Attributes.Name);

        string slug = new Uri(edge.Attributes.Path).Segments[^1];
        bool isCollection = edge.Attributes.Name.IsPlural();
        IEnumerable<ResourceChildFilter> filters = edge.Attributes.Scopes
            .Select(s => new ResourceChildFilter { Name = s.Name, Description = s.ScopeHelp });

        return new()
        {
            Name = edge.Attributes.Name,
            Description = edge.Attributes.Details,
            Slug = slug,
            Filters = filters,
            IsCollection = isCollection,
            IsDeprecated = edge.Attributes.Deprecated,
            Type = edge.Relationships.Head.Data.Id!
        };
    }
}
