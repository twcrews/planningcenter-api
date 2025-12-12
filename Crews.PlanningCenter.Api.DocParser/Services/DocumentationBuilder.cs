using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Incoming;
using Crews.PlanningCenter.Api.DocParser.Models.Outgoing;
using Microsoft.Extensions.Logging;

namespace Crews.PlanningCenter.Api.DocParser.Services;

class DocumentationBuilder(IPlanningCenterClient client, ILogger<DocumentationBuilder> logger) : IDocumentationBuilder
{
    public async Task<IEnumerable<Product>> BuildAllProductsAsync()
    {
        logger.LogDebug("Building documentation for all products");

        List<Product> products = [];
        foreach (ProductDefinition productDefinition in ProductDefinition.All)
        {
            Product product = await BuildProductAsync(productDefinition);
            products.Add(product);
        }
        return products;
    }

    public async Task<Product> BuildProductAsync(ProductDefinition product)
    {
        logger.LogDebug("Building documentation for product: {ProductName}", product);
        GraphDocument graphDocument = await client.GetGraphAsync(product);
        List<Models.Outgoing.Version> versions = [];

        foreach (VersionResource version in graphDocument.Data.Relationships.Versions.Data)
        {
            logger.LogTrace("Building version with ID: {VersionId}", version.Id);
            GraphVersionDocument versionDocument = await client.GetGraphVersionAsync(product, version.Id!);
            List<Resource> resources = [];

            foreach (VertexResource versionVertex in versionDocument.Data.Relationships.Vertices.Data)
            {
                VertexDocument vertexDocument = await client.GetVertexAsync(product, version.Id!, versionVertex.Id!);
                resources.Add(BuildResource(vertexDocument.Data));
            }
            versions.Add(new()
            {
                Id = versionDocument.Data.Id!,
                Beta = versionDocument.Data.Attributes.Beta,
                Details = versionDocument.Data.Attributes.Details,
                Resources = resources
            });
        }

        return new()
        {
            Name = product,
            Title = graphDocument.Data.Attributes.Title!,
            Description = graphDocument.Data.Attributes.Description,
            Versions = versions
        };
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
