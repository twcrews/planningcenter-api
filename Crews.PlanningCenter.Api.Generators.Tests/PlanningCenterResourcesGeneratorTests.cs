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
            "public record PersonResource : JsonApiResource<Person>");
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
            "public record Person",
            "public string? FirstName { get; init; }",
            "public string? LastName { get; init; }",
            "public DateOnly? Birthdate { get; init; }",
            "public DateTime? CreatedAt { get; init; }",
            "public int? Age { get; init; }",
            "public bool? Active { get; init; }",
            "public JsonObject? Metadata { get; init; }");
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
            "public record EmailRelationships",
            "public JsonApiRelationship? Person { get; init; }");
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
                    Id = "person",
                    Name = "person",
                    ResourceName = "PersonResource",
                    GenerateResource = true,
                    GenerateClients = false,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute { Name = "name", Type = "string" }],
                    Relationships = [],
                    Children = [],
                    CanInclude = [],
                    CanOrderBy = [],
                    CanQueryBy = []
                },
                new Crews.PlanningCenter.Api.Models.Resource
                {
                    Id = "email",
                    Name = "email",
                    ResourceName = "EmailResource",
                    GenerateResource = false,
                    GenerateClients = false,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute { Name = "address", Type = "string" }],
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
            "public record PersonResource",
            "public record Person");

        GeneratorTestHelper.AssertDoesNotContain(source,
            "public record EmailResource",
            "public record Email {");
    }

    [Fact]
    public void ShouldHandleDefaultDescriptionWhenNull()
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
                    Id = "person",
                    Name = "person",
                    ResourceName = "Person",
                    Description = null, // No description
                    GenerateResource = true,
                    GenerateClients = false,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute { Name = "name", Type = "string", Description = null }],
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
            "/// Planning Center does not provide a description for this resource.",
            "/// Planning Center does not provide a description for this attribute.");
    }
}
