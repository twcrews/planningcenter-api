using System.Text.Json;
using ApiVersion = Crews.PlanningCenter.Api.Models.Version;
using ApiResource = Crews.PlanningCenter.Api.Models.Resource;
using ApiResourceAttribute = Crews.PlanningCenter.Api.Models.ResourceAttribute;
using ApiResourceRelationship = Crews.PlanningCenter.Api.Models.ResourceRelationship;
using ApiResourceChild = Crews.PlanningCenter.Api.Models.ResourceChild;
using ApiResourceChildFilter = Crews.PlanningCenter.Api.Models.ResourceChildFilter;
using ApiResourceIncludable = Crews.PlanningCenter.Api.Models.ResourceIncludable;
using ApiResourceOrderable = Crews.PlanningCenter.Api.Models.ResourceOrderable;
using ApiResourceQueryable = Crews.PlanningCenter.Api.Models.ResourceQueryable;

namespace Crews.PlanningCenter.Api.Generators.Tests.TestData;

/// <summary>
/// Provides sample version data for testing the source generators.
/// </summary>
public static class SampleVersionData
{
    public static string GetSampleVersionJson()
    {
        var version = new ApiVersion
        {
            Id = "2025-01-01",
            Beta = false,
            Details = "Sample version for testing",
            Resources =
            [
                CreateSampleResource(),
                CreateSampleResourceWithRelationships(),
                CreateCollectionOnlyResource()
            ]
        };

        return JsonSerializer.Serialize(version, new JsonSerializerOptions { WriteIndented = true });
    }

    public static ApiVersion GetSampleVersion()
    {
        return new ApiVersion
        {
            Id = "2025-01-01",
            Beta = false,
            Details = "Sample version for testing",
            Resources =
            [
                CreateSampleResource(),
                CreateSampleResourceWithRelationships(),
                CreateCollectionOnlyResource()
            ]
        };
    }

    public static ApiVersion GetMinimalVersion()
    {
        return new ApiVersion
        {
            Id = "2024-12-01",
            Beta = true,
            Details = "Minimal version",
            Resources =
            [
                new ApiResource
                {
                    Id = "person",
                    Name = "person",
                    ResourceName = "PersonResource",
                    Description = "A person in the system",
                    GenerateResource = true,
                    GenerateClients = true,
                    Attributes =
                    [
                        new ApiResourceAttribute
                        {
                            Name = "name",
                            Type = "string",
                            Description = "The person's name"
                        }
                    ],
                    Relationships = [],
                    Children = [],
                    CanInclude = [],
                    CanOrderBy = [],
                    CanQueryBy = []
                }
            ]
        };
    }

    private static ApiResource CreateSampleResource()
    {
        return new ApiResource
        {
            Id = "person",
            Name = "person",
            ResourceName = "Person",
            Description = "Represents a person in the Planning Center system.\nCan be used to track individuals.",
            Deprecated = false,
            CollectionOnly = false,
            GenerateResource = true,
            GenerateClients = true,
            Postable = true,
            Patchable = true,
            Deletable = true,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    Name = "first_name",
                    Type = "string",
                    Description = "The person's first name"
                },
                new ApiResourceAttribute
                {
                    Name = "last_name",
                    Type = "string",
                    Description = "The person's last name"
                },
                new ApiResourceAttribute
                {
                    Name = "birthdate",
                    Type = "date",
                    Description = "The person's birthdate"
                },
                new ApiResourceAttribute
                {
                    Name = "created_at",
                    Type = "date_time",
                    Description = "When the person record was created"
                },
                new ApiResourceAttribute
                {
                    Name = "age",
                    Type = "integer",
                    Description = "The person's age in years"
                },
                new ApiResourceAttribute
                {
                    Name = "active",
                    Type = "boolean",
                    Description = "Whether the person is active"
                },
                new ApiResourceAttribute
                {
                    Name = "metadata",
                    Type = "json",
                    Description = "Additional metadata"
                }
            ],
            Relationships = [],
            Children =
            [
                new ApiResourceChild
                {
                    Name = "emails",
                    Type = "email",
                    Description = "The person's email addresses",
                    Slug = "emails",
                    IsCollection = true,
                    IsDeprecated = false,
                    Filters = []
                }
            ],
            CanInclude =
            [
                new ApiResourceIncludable
                {
                    Parameter = "include",
                    Value = "emails",
                    Description = "Include the person's email addresses",
                    CanAssignOnCreate = true,
                    CanAssignOnUpdate = false
                }
            ],
            CanOrderBy =
            [
                new ApiResourceOrderable
                {
                    Parameter = "order",
                    Value = "first_name",
                    Type = "string",
                    Description = "Order by first name"
                },
                new ApiResourceOrderable
                {
                    Parameter = "order",
                    Value = "last_name",
                    Type = "string",
                    Description = "Order by last name"
                }
            ],
            CanQueryBy =
            [
                new ApiResourceQueryable
                {
                    Name = "first_name",
                    Parameter = "where[first_name]",
                    Type = "string",
                    Description = "Query by first name",
                    Example = "where[first_name]=John"
                }
            ]
        };
    }

    private static ApiResource CreateSampleResourceWithRelationships()
    {
        return new ApiResource
        {
            Id = "email",
            Name = "email",
            ResourceName = "Email",
            Description = "An email address",
            Deprecated = false,
            CollectionOnly = false,
            GenerateResource = true,
            GenerateClients = true,
            Postable = true,
            Patchable = false,
            Deletable = true,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    Name = "address",
                    Type = "string",
                    Description = "The email address"
                },
                new ApiResourceAttribute
                {
                    Name = "primary",
                    Type = "boolean",
                    Description = "Whether this is the primary email"
                }
            ],
            Relationships =
            [
                new ApiResourceRelationship
                {
                    Name = "person",
                    Type = "Person",
                    AssociationType = "belongs_to",
                    Note = "The person this email belongs to"
                }
            ],
            Children = [],
            CanInclude = [],
            CanOrderBy = [],
            CanQueryBy = []
        };
    }

    private static ApiResource CreateCollectionOnlyResource()
    {
        return new ApiResource
        {
            Id = "report",
            Name = "report",
            ResourceName = "Report",
            Description = "A report (collection only)",
            Deprecated = true,
            CollectionOnly = true,
            GenerateResource = true,
            GenerateClients = true,
            Postable = false,
            Patchable = false,
            Deletable = false,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    Name = "title",
                    Type = "string",
                    Description = "Report title"
                }
            ],
            Relationships = [],
            Children = [],
            CanInclude = [],
            CanOrderBy = [],
            CanQueryBy = []
        };
    }

    public static ApiResource CreateResourceWithNameConflict()
    {
        // Creates a resource where an attribute has the same name as the class
        return new ApiResource
        {
            Id = "person",
            Name = "person",
            ResourceName = "Person",
            Description = "A person",
            GenerateResource = true,
            GenerateClients = true,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    Name = "person",
                    Type = "string",
                    Description = "This attribute has the same name as the class"
                },
                new ApiResourceAttribute
                {
                    Name = "name",
                    Type = "string",
                    Description = "The name"
                }
            ],
            Relationships = [],
            Children = [],
            CanInclude = [],
            CanOrderBy = [],
            CanQueryBy = []
        };
    }
}
