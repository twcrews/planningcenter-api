using Crews.PlanningCenter.Api.Generators.Tests.TestData;

namespace Crews.PlanningCenter.Api.Generators.Tests;

public class PlanningCenterRootClientsGeneratorTests
{
    [Fact]
    public void ShouldGenerateRootClientClass()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        Assert.NotEmpty(result.GeneratedTrees);
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "namespace Crews.PlanningCenter.Api;",
            "public class PeopleClient(HttpClient httpClient)");
    }

    [Fact]
    public void ShouldGenerateLatestVersionProperty()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "/// <summary>",
            "/// Gets a client for the latest version (2024-12-01) of the People API.",
            "/// </summary>",
            "public Crews.PlanningCenter.Api.People.V2024_12_01.OrganizationClient Latest");
    }

    [Fact]
    public void ShouldGenerateVersionSpecificProperties()
    {
        // Arrange
        var version1 = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2024-01-01",
            Beta = false,
            Resources = [CreateMinimalResource()]
        };
        var version2 = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2024-12-01",
            Beta = false,
            Resources = [CreateMinimalResource()]
        };
        var json1 = System.Text.Json.JsonSerializer.Serialize(version1);
        var json2 = System.Text.Json.JsonSerializer.Serialize(version2);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-01-01.json", json1),
            ("People/2024-12-01.json", json2));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public Crews.PlanningCenter.Api.People.V2024_01_01.OrganizationClient V2024_01_01",
            "public Crews.PlanningCenter.Api.People.V2024_12_01.OrganizationClient V2024_12_01",
            "httpClient.DefaultRequestHeaders.SetPlanningCenterVersion(\"2024-01-01\");",
            "httpClient.DefaultRequestHeaders.SetPlanningCenterVersion(\"2024-12-01\");");
    }

    [Fact]
    public void ShouldSelectLatestVersionCorrectly()
    {
        // Arrange
        var version1 = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2023-06-15",
            Beta = false,
            Resources = [CreateMinimalResource()]
        };
        var version2 = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2024-12-01",
            Beta = false,
            Resources = [CreateMinimalResource()]
        };
        var version3 = new Crews.PlanningCenter.Api.Models.Version
        {
            Id = "2024-03-20",
            Beta = false,
            Resources = [CreateMinimalResource()]
        };
        var json1 = System.Text.Json.JsonSerializer.Serialize(version1);
        var json2 = System.Text.Json.JsonSerializer.Serialize(version2);
        var json3 = System.Text.Json.JsonSerializer.Serialize(version3);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2023-06-15.json", json1),
            ("People/2024-12-01.json", json2),
            ("People/2024-03-20.json", json3));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        // Latest should be 2024-12-01
        GeneratorTestHelper.AssertContains(source,
            "/// Gets a client for the latest version (2024-12-01) of the People API.");
    }

    [Fact]
    public void ShouldIncludeXmlDocumentation()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "/// <summary>",
            "/// Client for accessing the Planning Center People API.",
            "/// </summary>");
    }

    [Fact]
    public void ShouldIncludeAutoGeneratedComment()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

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
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

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
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "using System;",
            "using System.Net.Http;",
            "using System.Threading.Tasks;",
            "using Crews.Web.JsonApiClient;",
            "using Crews.PlanningCenter.Api.Extensions;");
    }

    [Fact]
    public void ShouldGenerateCorrectUriForVersionedEndpoints()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "new(httpClient.BaseAddress ?? new(\"https://api.planningcenteronline.com/\"), \"People/v2/\")");
    }

    [Fact]
    public void ShouldGenerateClientForDifferentProducts()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("Services/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "Services.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public class ServicesClient(HttpClient httpClient)",
            "/// Client for accessing the Planning Center Services API.",
            "public Crews.PlanningCenter.Api.Services.V2024_12_01.OrganizationClient Latest");
    }

    [Fact]
    public void ShouldHandleCheckInsProductName()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("CheckIns/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterRootClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "CheckIns.Client.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public class CheckInsClient(HttpClient httpClient)",
            "/// Client for accessing the Planning Center CheckIns API.",
            "public Crews.PlanningCenter.Api.CheckIns.V2024_12_01.OrganizationClient Latest");
    }

    private static Crews.PlanningCenter.Api.Models.Resource CreateMinimalResource()
    {
        return new Crews.PlanningCenter.Api.Models.Resource
        {
            Id = "person",
            Name = "person",
            ResourceName = "Person",
            GenerateResource = true,
            GenerateClients = true,
            Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute { Name = "name", Type = "string" }],
            Relationships = Array.Empty<Crews.PlanningCenter.Api.Models.ResourceRelationship>(),
            Children = Array.Empty<Crews.PlanningCenter.Api.Models.ResourceChild>(),
            CanInclude = Array.Empty<Crews.PlanningCenter.Api.Models.ResourceIncludable>(),
            CanOrderBy = Array.Empty<Crews.PlanningCenter.Api.Models.ResourceOrderable>(),
            CanQueryBy = Array.Empty<Crews.PlanningCenter.Api.Models.ResourceQueryable>()
        };
    }
}
