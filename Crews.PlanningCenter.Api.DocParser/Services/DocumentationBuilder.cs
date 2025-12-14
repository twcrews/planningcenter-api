using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Incoming;
using Crews.PlanningCenter.Api.DocParser.Models.Outgoing;
using Microsoft.Extensions.Logging;

namespace Crews.PlanningCenter.Api.DocParser.Services;

class DocumentationBuilder(IPlanningCenterClient client, ILogger<DocumentationBuilder> logger) : IDocumentationBuilder
{
    private readonly SemaphoreSlim _semaphore = new(10, 10);

    public async Task<IEnumerable<Product>> BuildAllProductsAsync()
    {
        logger.LogDebug("Building documentation for all products");

        Task<Product>[] productTasks = [.. ProductDefinition.All.Select(BuildProductAsync)];

        return await Task.WhenAll(productTasks);
    }

    public async Task<Product> BuildProductAsync(ProductDefinition product)
    {
        logger.LogDebug("Building documentation for product: {ProductName}", product);

        await _semaphore.WaitAsync();
        GraphDocument graphDocument;
        try
        {
            graphDocument = await client.GetGraphAsync(product);
        }
        finally
        {
            _semaphore.Release();
        }

        Task<Models.Outgoing.Version>[] versionTasks = 
        [
            .. graphDocument.Data.Relationships.Versions.Data.Select(version => BuildVersionAsync(product, version))
        ];

        Models.Outgoing.Version[] versions = await Task.WhenAll(versionTasks);

        return new()
        {
            Name = product,
            Title = graphDocument.Data.Attributes.Title!,
            Description = graphDocument.Data.Attributes.Description,
            Versions = versions
        };
    }

    private async Task<Models.Outgoing.Version> BuildVersionAsync(ProductDefinition product, VersionResource version)
    {
        logger.LogTrace("Building version with ID: {VersionId}", version.Id);

        await _semaphore.WaitAsync();
        GraphVersionDocument versionDocument;
        try
        {
            versionDocument = await client.GetGraphVersionAsync(product, version.Id!);
        }
        finally
        {
            _semaphore.Release();
        }

        Task<Resource>[] resourceTasks =
        [
            .. versionDocument.Data.Relationships.Vertices.Data.Select(
                versionVertex => BuildResourceAsync(product, version.Id!, versionVertex))
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
            vertexDocument = await client.GetVertexAsync(product, versionId, versionVertex.Id!);
        }
        finally
        {
            _semaphore.Release();
        }

        return BuildResource(vertexDocument.Data);
    }

    private Resource BuildResource(VertexResource vertex)
    {
        logger.LogTrace("Building resource for vertex with ID: {VertexId}", vertex.Id);
        return new()
        {
            Id = vertex.Id!,
            Name = vertex.Attributes!.Name!,
            Description = vertex.Attributes.Description,
            Deprecated = vertex.Attributes.Deprecated,
            CollectionOnly = vertex.Attributes.CollectionOnly,
            Attributes = vertex.Relationships!.Attributes.Data.Select(attr => BuildAttribute(attr.Attributes)),
            Relationships = vertex.Relationships.Relationships.Data.Select(rel => BuildRelationship(rel.Attributes)),
            CanInclude = vertex.Relationships.CanInclude.Data.Select(inc => BuildIncludable(inc.Attributes)),
            CanOrderBy = vertex.Relationships.CanOrder.Data.Select(ord => BuildOrderable(ord.Attributes)),
            CanQueryBy = vertex.Relationships.CanQuery.Data.Select(qry => BuildQueryable(qry.Attributes))
        };
    }

    private ResourceAttribute BuildAttribute(Models.Incoming.Attribute attribute)
    {
        logger.LogTrace("Building attribute: {AttributeName}", attribute.Name);
        return new()
        {
            Name = attribute.Name,
            Type = attribute.TypeAnnotation.Name,
            Description = attribute.Description
        };
    }

    private ResourceRelationship BuildRelationship(Relationship relationship)
    {
        logger.LogTrace("Building relationship: {RelationshipName}", relationship.Name);
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
        logger.LogTrace("Building includable parameter: {ParameterName}", parameter.Parameter);
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
        logger.LogTrace("Building orderable parameter: {ParameterName}", parameter.Parameter);
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
        logger.LogTrace("Building queryable parameter: {ParameterName}", parameter.Parameter);
        return new()
        {
            Name = parameter.Name,
            Parameter = parameter.Parameter,
            Type = parameter.Type,
            Description = parameter.Description,
            Example = parameter.Example
        };
    }
}
