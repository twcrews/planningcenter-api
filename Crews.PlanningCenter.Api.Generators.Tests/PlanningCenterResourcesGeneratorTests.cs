using Crews.PlanningCenter.Api.Generators.Tests.TestData;

namespace Crews.PlanningCenter.Api.Generators.Tests;

public class PlanningCenterResourcesGeneratorTests
{
    [Fact]
    public void ShouldGenerateResourceClass()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        Assert.NotEmpty(result.GeneratedTrees);
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "namespace Crews.PlanningCenter.Api.People.V2024_12_01;",
            "public partial record PersonResource : JsonApiResource<Person>");
    }

    [Fact]
    public void ShouldGenerateResourceAttributesClass()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public partial record Person",
            "public string? FirstName { get; init; }",
            "public string? LastName { get; init; }",
            "public System.DateOnly? Birthdate { get; init; }",
            "public System.DateTime? CreatedAt { get; init; }",
            "public int? Age { get; init; }",
            "public bool? Active { get; init; }",
            "public System.Text.Json.Nodes.JsonObject? Metadata { get; init; }");
    }

    [Fact]
    public void ShouldGenerateResourceRelationshipsClass()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public partial record EmailRelationships",
            "public JsonApiRelationship<PersonResource>? Person { get; init; }");
    }

    [Fact]
    public void ShouldNotGenerateRelationshipsClassWhenNoRelationships()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Resources.g.cs");

        Assert.NotNull(source);
        Assert.DoesNotContain("Relationships", source);
    }

    [Fact]
    public void ShouldIncludeXmlDocumentation()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "/// <summary>",
            "/// Represents a person in the Planning Center system.",
            "/// The person's first name",
            "/// </summary>");
    }

    [Fact]
    public void ShouldConvertNewLinesToBreakTagsInDocumentation()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "/// Represents a person in the Planning Center system.<br/>Can be used to track individuals.");
    }

    [Fact]
    public void ShouldHandleAttributeNameConflictWithClassName()
    {
        // Arrange
        var resource = SampleVersionData.CreateResourceWithNameConflict();
        var version = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2025-01-01",
            Beta = false,
            Resources = [resource]
        };
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public string? PersonAttribute { get; init; }",
            "NOTE: The name of this property has been modified because the type shares its original name.");
    }

    [Fact]
    public void ShouldGenerateAutoGeneratedComment()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source, "// <auto-generated />");
    }

    [Fact]
    public void ShouldIncludeNullableDirective()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source, "#nullable enable");
    }

    [Fact]
    public void ShouldIncludeRequiredUsings()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "using System;",
            "using System.Collections.Generic;",
            "using System.Text.Json;",
            "using System.Text.Json.Nodes;",
            "using System.Text.Json.Serialization;",
            "using Crews.Web.JsonApiClient;");
    }

    [Fact]
    public void ShouldGenerateJsonPropertyNameAttributes()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "[JsonPropertyName(\"first_name\")]",
            "[JsonPropertyName(\"last_name\")]");
    }

    [Fact]
    public void ShouldGenerateCollectionRelationshipPropertyWhenIsCollectionIsTrue()
    {
        // Arrange
        var version = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2025-01-01",
            Beta = false,
            Resources =
            [
                new Crews.PlanningCenter.Api.Models.Resource
                {
                    JsonName = "person",
                    AttributesClrType = "Person",
                    ResourceClrType = "PersonResource",
                    Description = "A person",
                    ShouldGenerateResource = true,
                    ShouldGenerateClients = false,
                    Attributes = [],
                    Relationships =
                    [
                        new Crews.PlanningCenter.Api.Models.ResourceRelationship
                        {
                            JsonName = "emails",
                            ClrName = "Emails",
                            AttributesClrType = "Email",
                            ResourceClrType = "EmailResource",
                            IsCollection = true,
                            Description = "The person's email addresses"
                        }
                    ],
                    Children = [],
                    CanInclude = [],
                    CanOrderBy = [],
                    CanQueryBy = []
                }
            ]
        };
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public JsonApiCollectionRelationship<EmailResource>? Emails { get; init; }");
        GeneratorTestHelper.AssertDoesNotContain(source,
            "public JsonApiRelationship<EmailResource>? Emails { get; init; }");
    }

    [Fact]
    public void ShouldGenerateJsonConverterAttributeWhenPresent()
    {
        // Arrange
        var version = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2025-01-01",
            Beta = false,
            Resources =
            [
                new Crews.PlanningCenter.Api.Models.Resource
                {
                    JsonName = "person",
                    AttributesClrType = "Person",
                    ResourceClrType = "PersonResource",
                    Description = "A person",
                    ShouldGenerateResource = true,
                    ShouldGenerateClients = false,
                    Attributes =
                    [
                        new Crews.PlanningCenter.Api.Models.ResourceAttribute
                        {
                            JsonName = "status",
                            ClrName = "Status",
                            ClrType = "string",
                            Description = "The person's status",
                            JsonConverter = "StatusJsonConverter"
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
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source, "[JsonConverter(typeof(StatusJsonConverter))]");
    }

    [Fact]
    public void ShouldNotGenerateJsonConverterAttributeWhenAbsent()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertDoesNotContain(source, "[JsonConverter(typeof(");
    }

    [Fact]
    public void ShouldOnlyGenerateResourcesWithGenerateResourceFlagSet()
    {
        // Arrange
        var version = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2025-01-01",
            Beta = false,
            Resources =
            [
                new Crews.PlanningCenter.Api.Models.Resource
                {
                    JsonName = "person",
                    AttributesClrType = "Person",
                    ResourceClrType = "PersonResource",
                    Description = "A person resource",
                    ShouldGenerateResource = true,
                    ShouldGenerateClients = false,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute
                    {
                        JsonName = "name",
                        ClrName = "Name",
                        ClrType = "string",
                        Description = "The person's name"
                    }],
                    Relationships = [],
                    Children = [],
                    CanInclude = [],
                    CanOrderBy = [],
                    CanQueryBy = []
                },
                new Crews.PlanningCenter.Api.Models.Resource
                {
                    JsonName = "email",
                    AttributesClrType = "Email",
                    ResourceClrType = "EmailResource",
                    Description = "An email resource",
                    ShouldGenerateResource = false,
                    ShouldGenerateClients = false,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute
                    {
                        JsonName = "address",
                        ClrName = "Address",
                        ClrType = "string",
                        Description = "The email address"
                    }],
                    Relationships = [],
                    Children = [],
                    CanInclude = [],
                    CanOrderBy = [],
                    CanQueryBy = []
                }
            ]
        };
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourcesGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Resources.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public partial record PersonResource",
            "public partial record Person");

        GeneratorTestHelper.AssertDoesNotContain(source,
            "public partial record EmailResource",
            "public partial record Email {");
    }
}
