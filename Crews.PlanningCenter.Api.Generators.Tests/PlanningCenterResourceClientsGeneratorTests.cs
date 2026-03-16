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
            "public PersonClient IncludeEmails() => (PersonClient)AddQueryParameter(\"include\", \"emails\");");
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
            "public PersonClient OrderByFirstName() => (PersonClient)ReplaceQueryParameter(\"order\", \"first_name\");",
            "public PersonClient OrderByLastName() => (PersonClient)ReplaceQueryParameter(\"order\", \"last_name\");");
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
            "public PersonClient WhereFirstName(string value) => (PersonClient)ReplaceQueryParameter(\"where[first_name]\", value);");
    }

    [Fact]
    public void ShouldFormatDateTimeQueryParameterWithRoundTripFormat()
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
            "public PersonClient WhereCreatedAt(System.DateTime value) => (PersonClient)ReplaceQueryParameter(\"where[created_at]\", value.ToString(\"o\"));");
    }

    [Fact]
    public void ShouldFormatDateOnlyQueryParameterWithDateFormat()
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
            "public PersonClient WhereBirthdate(System.DateOnly value) => (PersonClient)ReplaceQueryParameter(\"where[birthdate]\", value.ToString(\"yyyy-MM-dd\"));");
    }

    [Fact]
    public void ShouldCallToStringForNonStringQueryParameter()
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
            "public PersonClient WhereAge(int value) => (PersonClient)ReplaceQueryParameter(\"where[age]\", value.ToString());");
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

        string reportClientSection = source[withIdInReportClient..nextClientStart];
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
            "[Obsolete(\"This resource is deprecated and may be removed in a future version.\")]");
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
            "/// Client for interacting with the Person resource.",
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
                    JsonName = "person",
                    AttributesClrType = "Person",
                    ResourceClrType = "PersonResource",
                    ShouldGenerateResource = true,
                    ShouldGenerateClients = true,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute { JsonName = "name", ClrType = "string" }],
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
                    ShouldGenerateResource = true,
                    ShouldGenerateClients = false,
                    Attributes = [new Crews.PlanningCenter.Api.Models.ResourceAttribute { JsonName = "address", ClrType = "string" }],
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

    [Fact]
    public void ShouldIncludeObsoleteAttributeForDeprecatedChildNavigation()
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

        // Verify the obsolete attribute is adjacent to the deprecated child (Notes), not the non-deprecated one (Emails)
        int notesIndex = source.IndexOf("public PaginatedNoteClient Notes =>");
        int obsoleteBeforeNotes = source.LastIndexOf("[Obsolete(\"This endpoint is deprecated", notesIndex);
        Assert.True(obsoleteBeforeNotes >= 0 && notesIndex - obsoleteBeforeNotes < 200);

        int emailsIndex = source.IndexOf("public PaginatedEmailClient Emails =>");
        int obsoleteBeforeEmails = source.LastIndexOf("[Obsolete(\"This endpoint is deprecated", emailsIndex);
        Assert.True(obsoleteBeforeEmails < 0 || obsoleteBeforeEmails < emailsIndex - 200);
    }

    [Fact]
    public void ShouldGenerateActionMethod()
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
            "public Task Promote(CancellationToken cancellationToken = default) => base.PostAsync(new(Uri, \"promote\"), cancellationToken);");
    }

    [Fact]
    public void ShouldIncludeObsoleteAttributeForDeprecatedAction()
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

        // Verify the obsolete attribute is adjacent to the deprecated action (Archive), not the non-deprecated one (Promote)
        int archiveIndex = source.IndexOf("public Task Archive(");
        int obsoleteBeforeArchive = source.LastIndexOf("[Obsolete(\"This action is deprecated", archiveIndex);
        Assert.True(obsoleteBeforeArchive >= 0 && archiveIndex - obsoleteBeforeArchive < 200);
    }

    [Fact]
    public void ShouldIncludeAdditionalDetailsInActionXmlDoc()
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
            "/// <br/>Rate limits apply.");
    }

    [Fact]
    public void ShouldGenerateResponseClassesForDottedAttributeType()
    {
        // Arrange: AttributesClrType contains a dot ("Address.Street") — when the
        // generator calls ToPascalCase() on it, the '.' branch is exercised (the dot
        // is preserved in output and the following character is capitalised).
        var version = SampleVersionData.GetVersionWithDottedAttributeType();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert: response class names reflect the dot-preserved PascalCase value
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");
        GeneratorTestHelper.AssertContains(source,
            "public class Address.StreetResponse : ResourceResponse<Address.StreetResource> { }",
            "public class Address.StreetCollectionResponse : ResourceResponse<IEnumerable<Address.StreetResource>> { }");
    }

    [Fact]
    public void ShouldGenerateResponseClassesForDigitToLetterAttributeType()
    {
        // Arrange: AttributesClrType has a letter immediately following a digit
        // ("Track1Title") — when the generator calls ToPascalCase() on it, the
        // digit-to-letter branch is exercised (the letter after the digit is capitalised).
        var version = SampleVersionData.GetVersionWithDigitToLetterAttributeType();
        var json = System.Text.Json.JsonSerializer.Serialize(version);
        var (compilation, additionalFiles) = GeneratorTestHelper.CreateCompilation(
            "namespace Test { }",
            ("People/2025-01-01.json", json));

        // Act
        var result = GeneratorTestHelper.RunGenerator(
            "PlanningCenterResourceClientsGenerator",
            compilation,
            additionalFiles);

        // Assert: response class names reflect the capitalised-after-digit PascalCase value
        var source = GeneratorTestHelper.GetGeneratedSource(result, "People.2025-01-01.Clients.g.cs");
        GeneratorTestHelper.AssertContains(source,
            "public class Track1TitleResponse : ResourceResponse<Track1TitleResource> { }",
            "public class Track1TitleCollectionResponse : ResourceResponse<IEnumerable<Track1TitleResource>> { }");
    }

    [System.Text.RegularExpressions.GeneratedRegex("public new Task DeleteAsync")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}
