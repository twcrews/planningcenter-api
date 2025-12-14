using Crews.PlanningCenter.Api.DocParser.Models.Incoming;

namespace Crews.PlanningCenter.Api.DocParser.Tests.Fixtures;

/// <summary>
/// Builder class for creating test data for Planning Center API documentation models
/// </summary>
internal static class TestDataBuilder
{
    public static GraphDocument CreateGraphDocument(
        string title = "Test Product",
        string? description = "Test Description",
        params VersionResource[] versions)
    {
        return new GraphDocument
        {
            Data = new GraphResource
            {
                Id = "test-product",
                Type = "graph",
                Attributes = new Graph
                {
                    Title = title,
                    Description = description
                },
                Relationships = new GraphRelationships
                {
                    Versions = new VersionRelationship
                    {
                        Data = versions
                    }
                }
            }
        };
    }

    public static VersionResource CreateVersionResource(string id = "2024-01-01")
    {
        return new VersionResource
        {
            Id = id,
            Type = "version",
            Attributes = new Models.Incoming.Version
            {
                Beta = false,
                Details = "Test version details"
            },
            Relationships = new VersionRelationships
            {
                Entry = new VertexRelationship
                {
                    Data = new VertexResource
                    {
                        Id = "organization",
                        Type = "vertex",
                        Attributes = new Vertex
                        {
                            Name = "Organization"
                        }
                    }
                },
                Vertices = new VertexCollectionRelationship
                {
                    Data = []
                }
            }
        };
    }

    public static GraphVersionDocument CreateGraphVersionDocument(
        string id = "2024-01-01",
        bool beta = false,
        string? details = "Test details",
        params VertexResource[] vertices)
    {
        return new GraphVersionDocument
        {
            Data = new GraphVersionResource
            {
                Id = id,
                Type = "version",
                Attributes = new GraphVersion
                {
                    Title = "Test Version",
                    Beta = beta,
                    Details = details
                },
                Relationships = new GraphVersionRelationships
                {
                    PreviousVersion = new(),
                    NextVersion = new(),
                    Changes = new VertexChangeCollectionRelationship { Data = [] },
                    Removed = new DeprecatedEdgeCollectionRelationship { Data = [] },
                    Vertices = new VertexCollectionRelationship { Data = vertices },
                    Edges = new EdgeCollectionRelationship { Data = [] },
                    Entry = new VertexRelationship
                    {
                        Data = vertices.FirstOrDefault() ?? CreateVertexResource()
                    }
                }
            }
        };
    }

    public static VertexResource CreateVertexResource(string id = "person", string name = "Person")
    {
        return new VertexResource
        {
            Id = id,
            Type = "vertex",
            Attributes = new Vertex
            {
                Name = name,
                Description = $"Test {name} description",
                CollectionOnly = false,
                Deprecated = false
            }
        };
    }

    public static VertexDocument CreateVertexDocument(
        string id = "person",
        string name = "Person",
        AttributeResource[]? attributes = null,
        RelationshipResource[]? relationships = null,
        UrlParameterResource[]? canInclude = null,
        UrlParameterResource[]? canOrder = null,
        UrlParameterResource[]? canQuery = null,
        bool canCreate = false,
        bool canUpdate = false,
        bool canDestroy = false)
    {
        return new VertexDocument
        {
            Data = new VertexResource
            {
                Id = id,
                Type = "vertex",
                Attributes = new Vertex
                {
                    Name = name,
                    Description = $"Test {name} description",
                    CollectionOnly = false,
                    Deprecated = false
                },
                Relationships = new VertexRelationships
                {
                    Attributes = new AttributeCollectionRelationship
                    {
                        Data = attributes ?? []
                    },
                    Relationships = new RelationshipCollectionRelationship
                    {
                        Data = relationships ?? []
                    },
                    Permissions = new PermissionsRelationship
                    {
                        Data = new PermissionsResource
                        {
                            Id = "permissions",
                            Type = "permissions",
                            Attributes = new Permissions
                            {
                                CanCreate = canCreate,
                                CanUpdate = canUpdate,
                                CanDestroy = canDestroy
                            }
                        }
                    },
                    Actions = new(),
                    OutboundEdges = new EdgeCollectionRelationship { Data = [] },
                    InboundEdges = new EdgeCollectionRelationship { Data = [] },
                    CanInclude = new UrlParameterCollectionRelationship
                    {
                        Data = canInclude ?? []
                    },
                    CanOrder = new UrlParameterCollectionRelationship
                    {
                        Data = canOrder ?? []
                    },
                    CanQuery = new UrlParameterCollectionRelationship
                    {
                        Data = canQuery ?? []
                    },
                    PerPage = new UrlParameterRelationship
                    {
                        Data = CreateUrlParameterResource("per_page", "per_page", "integer")
                    },
                    Offset = new UrlParameterRelationship
                    {
                        Data = CreateUrlParameterResource("offset", "offset", "integer")
                    },
                    RateLimits = new()
                }
            }
        };
    }

    public static AttributeResource CreateAttributeResource(
        string name = "first_name",
        string typeName = "string",
        string? description = null)
    {
        return new AttributeResource
        {
            Id = name,
            Type = "attribute",
            Attributes = new Models.Incoming.Attribute
            {
                Name = name,
                TypeAnnotation = new TypeAnnotation
                {
                    Name = typeName
                },
                Description = description ?? $"The {name} attribute",
                PermissionLevel = "read"
            }
        };
    }

    public static RelationshipResource CreateRelationshipResource(
        string name = "emails",
        string graphType = "Email",
        string association = "has_many")
    {
        return new RelationshipResource
        {
            Id = name,
            Type = "relationship",
            Attributes = new Models.Incoming.Relationship
            {
                Name = name,
                GraphType = graphType,
                Association = association,
                AuthorizationLevel = "public",
                Note = $"Related {name}"
            }
        };
    }

    public static UrlParameterResource CreateUrlParameterResource(
        string name = "emails",
        string parameter = "include",
        string type = "string",
        string? value = null,
        string? description = null)
    {
        return new UrlParameterResource
        {
            Id = $"{parameter}_{name}",
            Type = "url_parameter",
            Attributes = new UrlParameter
            {
                Name = name,
                Parameter = parameter,
                Type = type,
                Value = value ?? name,
                Description = description ?? $"Include {name}",
                CanAssignOnCreate = false,
                CanAssignOnUpdate = false
            }
        };
    }
}
