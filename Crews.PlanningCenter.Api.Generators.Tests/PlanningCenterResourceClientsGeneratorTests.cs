using Crews.PlanningCenter.Api.Generators.Tests.TestData;

namespace Crews.PlanningCenter.Api.Generators.Tests;

public partial class PlanningCenterResourceClientsGeneratorTests
{
    [Fact]
    public void ShouldGenerateResourceClientClass()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        Assert.NotEmpty(result.GeneratedTrees);
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "namespace Crews.PlanningCenter.Api.People.V2024_12_01;",
            "public class PersonClient(HttpClient httpClient, Uri uri)",
            ": SingletonResourceClient<Person, PersonResource, PersonResponse>(httpClient, uri)");
    }

    [Fact]
    public void ShouldGeneratePaginatedClientClass()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public class PaginatedPersonClient(HttpClient httpClient, Uri uri)",
            ": PaginatedResourceClient<Person, PersonResource, PersonCollectionResponse, PersonResponse>(httpClient, uri)");
    }

    [Fact]
    public void ShouldGenerateResponseClasses()
    {
        // Arrange
        var version = SampleVersionData.GetMinimalVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2024-12-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public class PersonResponse : ResourceResponse<PersonResource> { }",
            "public class PersonCollectionResponse : ResourceResponse<IEnumerable<PersonResource>> { }");
    }

    [Fact]
    public void ShouldGenerateGetMethod()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public new Task<PersonResponse> GetAsync(CancellationToken cancellationToken = default)");
    }

    [Fact]
    public void ShouldGeneratePostMethodWhenResourceIsPostable()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public new Task<PersonResponse> PostAsync(Person resource, CancellationToken cancellationToken = default)");
    }

    [Fact]
    public void ShouldNotGeneratePostMethodWhenResourceIsNotPostable()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        // Email resource is postable, Report is not
        Assert.Contains("public new Task<EmailResponse> PostAsync(Email resource", source);
        Assert.DoesNotContain("public new Task<ReportResponse> PostAsync(Report resource", source);
    }

    [Fact]
    public void ShouldGeneratePatchMethodWhenResourceIsPatchable()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public new Task<PersonResponse> PatchAsync(Person resource, CancellationToken cancellationToken = default)");
    }

    [Fact]
    public void ShouldNotGeneratePatchMethodWhenResourceIsNotPatchable()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        // Person is patchable, Email is not
        Assert.Contains("public new Task<PersonResponse> PatchAsync(Person resource", source);
        Assert.DoesNotContain("public new Task<EmailResponse> PatchAsync(Email resource", source);
    }

    [Fact]
    public void ShouldGenerateDeleteMethodWhenResourceIsDeletable()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public new Task DeleteAsync(CancellationToken cancellationToken = default)");
    }

    [Fact]
    public void ShouldNotGenerateDeleteMethodWhenResourceIsNotDeletable()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        // Person and Email are deletable, Report is not
        Assert.Contains("public new Task DeleteAsync(CancellationToken cancellationToken = default) => base.DeleteAsync(cancellationToken);", source);

        // Count occurrences - should appear for Person and Email but not Report
        Assert.NotNull(source);
        int deleteAsyncCount = MyRegex().Count(source);
        Assert.Equal(2, deleteAsyncCount); // Only Person and Email
    }

    [Fact]
    public void ShouldGenerateChildNavigationProperties()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public PaginatedEmailClient Emails => new(HttpClient, new(Uri, \"emails/\"));");
    }

    [Fact]
    public void ShouldGenerateIncludeMethods()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public PersonClient IncludeEmails() => (PersonClient)SetQueryParameter(\"include\", \"emails\");");
    }

    [Fact]
    public void ShouldGenerateOrderByMethods()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public PersonClient OrderByFirstName() => (PersonClient)SetQueryParameter(\"order\", \"first_name\");",
            "public PersonClient OrderByLastName() => (PersonClient)SetQueryParameter(\"order\", \"last_name\");");
    }

    [Fact]
    public void ShouldGenerateQueryByMethods()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public PersonClient WhereFirstName(string value) => (PersonClient)SetQueryParameter(\"where[first_name]\", value);");
    }

    [Fact]
    public void ShouldIncludeExampleInQueryByMethodDocumentation()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "Example: WhereFirstName(\"John\")");
    }

    [Fact]
    public void ShouldGenerateWithIdMethodForNonCollectionOnlyResources()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        // Person is not collection-only, so should have WithId
        GeneratorTestHelper.AssertContains(source,
            "public PersonClient WithId(string id) => new (HttpClient, new(Uri, $\"{id}/\"));");
    }

    [Fact]
    public void ShouldNotGenerateWithIdMethodForCollectionOnlyResources()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");
        Assert.NotNull(source);

        // Report is collection-only, should not have WithId
        int withIdInReportClient = source.IndexOf("public class PaginatedReportClient");
        int nextClientStart = source.IndexOf("public class ", withIdInReportClient + 1);
        if (nextClientStart == -1) nextClientStart = source.Length;

        string reportClientSection = source.Substring(withIdInReportClient, nextClientStart - withIdInReportClient);
        Assert.DoesNotContain("WithId", reportClientSection);
    }

    [Fact]
    public void ShouldIncludeDeprecatedCommentForDeprecatedResources()
    {
        // Arrange
        var version = SampleVersionData.GetSampleVersion();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "/// DEPRECATED: Client for interacting with the report resource.");
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
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "/// <summary>",
            "/// Client for interacting with the person resource.",
            "/// Fetches the <see cref=\"Person\"/> resource asynchronously.",
            "/// </summary>");
    }

    [Fact]
    public void ShouldOnlyGenerateClientsWithGenerateClientsFlagSet()
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
                    GenerateResource = true,
                    GenerateClients = true,
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
                    ResourceName = "Email",
                    GenerateResource = true,
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
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "public class PersonClient",
            "public class PaginatedPersonClient");

        GeneratorTestHelper.AssertDoesNotContain(source,
            "public class EmailClient",
            "public class PaginatedEmailClient");
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
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Clients.g.cs");

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
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Clients.g.cs");

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
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2024-12-01.Clients.g.cs");

        GeneratorTestHelper.AssertContains(source,
            "using System;",
            "using System.Collections.Generic;",
            "using System.Net.Http;",
            "using System.Net.Http.Json;",
            "using System.Text.Json;",
            "using System.Text.Json.Nodes;",
            "using System.Text.Json.Serialization;",
            "using Crews.PlanningCenter.Api.Models;",
            "using Crews.Web.JsonApiClient;");
    }

    [System.Text.RegularExpressions.GeneratedRegex("public new Task DeleteAsync")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}
