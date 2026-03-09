using System.Text.Json;
using ApiVersion = Crews.PlanningCenter.Api.Models.Version;
using ApiResource = Crews.PlanningCenter.Api.Models.Resource;
using ApiResourceAttribute = Crews.PlanningCenter.Api.Models.ResourceAttribute;
using ApiResourceRelationship = Crews.PlanningCenter.Api.Models.ResourceRelationship;
using ApiResourceChild = Crews.PlanningCenter.Api.Models.ResourceChild;
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
                    JsonName = "person",
                    AttributesClrType = "Person",
                    ResourceClrType = "PersonResource",
                    Description = "A person in the system",
                    ShouldGenerateResource = true,
                    ShouldGenerateClients = true,
                    Attributes =
                    [
                        new ApiResourceAttribute
                        {
                            JsonName = "name",
                            ClrName = "Name",
                            ClrType = "string",
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
            JsonName = "person",
            AttributesClrType = "Person",
            ResourceClrType = "PersonResource",
            Description = "Represents a person in the Planning Center system.\nCan be used to track individuals.",
            Deprecated = false,
            CollectionOnly = false,
            ShouldGenerateResource = true,
            ShouldGenerateClients = true,
            Postable = true,
            Patchable = true,
            Deletable = true,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    JsonName = "first_name",
                    ClrName = "FirstName",
                    ClrType = "string",
                    Description = "The person's first name"
                },
                new ApiResourceAttribute
                {
                    JsonName = "last_name",
                    ClrName = "LastName",
                    ClrType = "string",
                    Description = "The person's last name"
                },
                new ApiResourceAttribute
                {
                    JsonName = "birthdate",
                    ClrName = "Birthdate",
                    ClrType = "System.DateOnly",
                    Description = "The person's birthdate"
                },
                new ApiResourceAttribute
                {
                    JsonName = "created_at",
                    ClrName = "CreatedAt",
                    ClrType = "System.DateTime",
                    Description = "When the person record was created"
                },
                new ApiResourceAttribute
                {
                    JsonName = "age",
                    ClrName = "Age",
                    ClrType = "int",
                    Description = "The person's age in years"
                },
                new ApiResourceAttribute
                {
                    JsonName = "active",
                    ClrName = "Active",
                    ClrType = "bool",
                    Description = "Whether the person is active"
                },
                new ApiResourceAttribute
                {
                    JsonName = "metadata",
                    ClrName = "Metadata",
                    ClrType = "System.Text.Json.Nodes.JsonObject",
                    Description = "Additional metadata"
                }
            ],
            Relationships = [],
            Children =
            [
                new ApiResourceChild
                {
                    JsonName = "emails",
                    ClrName = "Emails",
                    ClrAttributesType = "Email",
                    Description = "The person's email addresses",
                    Slug = "emails",
                    IsCollection = true,
                    Deprecated = false,
                    Filters = []
                }
            ],
            CanInclude =
            [
                new ApiResourceIncludable
                {
                    ClrMethodName = "IncludeEmails",
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
                    ClrMethodName = "OrderByFirstName",
                    Value = "first_name",
                    Description = "Order by first name"
                },
                new ApiResourceOrderable
                {
                    ClrMethodName = "OrderByLastName",
                    Value = "last_name",
                    Description = "Order by last name"
                }
            ],
            CanQueryBy =
            [
                new ApiResourceQueryable
                {
                    ClrMethodName = "WhereFirstName",
                    Parameter = "where[first_name]",
                    ClrType = "string",
                    Description = "Query by first name",
                }
            ]
        };
    }

    private static ApiResource CreateSampleResourceWithRelationships()
    {
        return new ApiResource
        {
            JsonName = "email",
            AttributesClrType = "Email",
            ResourceClrType = "EmailResource",
            Description = "An email address",
            Deprecated = false,
            CollectionOnly = false,
            ShouldGenerateResource = true,
            ShouldGenerateClients = true,
            Postable = true,
            Patchable = false,
            Deletable = true,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    JsonName = "address",
                    ClrName = "Address",
                    ClrType = "string",
                    Description = "The email address"
                },
                new ApiResourceAttribute
                {
                    JsonName = "primary",
                    ClrName = "Primary",
                    ClrType = "bool",
                    Description = "Whether this is the primary email"
                }
            ],
            Relationships =
            [
                new ApiResourceRelationship
                {
                    JsonName = "person",
                    ClrName = "Person",
                    ClrAttributesType = "Person",
                    ClrResourceType = "PersonResource",
                    IsCollection = false,
                    Description = "The person this email belongs to"
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
            JsonName = "report",
            AttributesClrType = "Report",
            ResourceClrType = "ReportResource",
            Description = "A report (collection only)",
            Deprecated = true,
            CollectionOnly = true,
            ShouldGenerateResource = true,
            ShouldGenerateClients = true,
            Postable = false,
            Patchable = false,
            Deletable = false,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    JsonName = "title",
                    ClrName = "Title",
                    ClrType = "string",
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
            JsonName = "person",
            AttributesClrType = "Person",
            ResourceClrType = "PersonResource",
            Description = "A person",
            ShouldGenerateResource = true,
            ShouldGenerateClients = true,
            Attributes =
            [
                new ApiResourceAttribute
                {
                    JsonName = "person",
                    ClrName = "Person",
                    ClrType = "string",
                    Description = "This attribute has the same name as the class"
                },
                new ApiResourceAttribute
                {
                    JsonName = "name",
                    ClrName = "Name",
                    ClrType = "string",
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
