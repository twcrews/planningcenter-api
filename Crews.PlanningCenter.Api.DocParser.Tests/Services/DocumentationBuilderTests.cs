using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Services;
using Crews.PlanningCenter.Api.DocParser.Tests.Fixtures;
using Crews.PlanningCenter.Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Crews.PlanningCenter.Api.DocParser.Tests.Services;

public class DocumentationBuilderTests
{
    private readonly IPlanningCenterClient _mockClient;
    private readonly ILogger<DocumentationBuilder> _mockLogger;
    private readonly DocumentationBuilder _builder;

    public DocumentationBuilderTests()
    {
        _mockClient = Substitute.For<IPlanningCenterClient>();
        _mockLogger = Substitute.For<ILogger<DocumentationBuilder>>();
        _builder = new DocumentationBuilder(_mockLogger, _mockClient, 
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }));
    }

    [Fact(DisplayName = "BuildAllProductsAsync builds all products concurrently")]
    public async Task BuildAllProductsAsync_BuildsAllProducts()
    {
        // Arrange
        foreach (ProductDefinition product in ProductDefinition.All)
        {
            GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(
                title: product.ToString(),
                description: $"Description for {product}");

            _mockClient.GetGraphAsync(product)
                .Returns(Task.FromResult(graphDoc));
        }

        // Act
        IEnumerable<Product> products = await _builder.BuildAllProductsAsync();

        // Assert
        Assert.Equal(ProductDefinition.All.Count(), products.Count());
        foreach (ProductDefinition productDef in ProductDefinition.All)
        {
            await _mockClient.Received(1).GetGraphAsync(productDef);
        }
    }

    [Fact(DisplayName = "BuildProductAsync returns product with correct metadata")]
    public async Task BuildProductAsync_ReturnsProductWithMetadata()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(
            title: "People API",
            description: "Manage people and contacts");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Assert.Equal(productDef.ToString(), product.Name);
        Assert.Equal("People API", product.Title);
        Assert.Equal("Manage people and contacts", product.Description);
    }

    [Fact(DisplayName = "BuildProductAsync processes all versions")]
    public async Task BuildProductAsync_ProcessesAllVersions()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.Calendar;
        VersionResource version1 = TestDataBuilder.CreateVersionResource("2024-01-01");
        VersionResource version2 = TestDataBuilder.CreateVersionResource("2024-06-01");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(
            versions: [version1, version2]);

        GraphVersionDocument versionDoc1 = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            beta: false,
            vertices: []);

        GraphVersionDocument versionDoc2 = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-06-01",
            beta: true,
            vertices: []);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc1);
        _mockClient.GetGraphVersionAsync(productDef, "2024-06-01").Returns(versionDoc2);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Assert.Equal(2, product.Versions.Count());
        Assert.Contains(product.Versions, v => v.Id == "2024-01-01" && !v.Beta);
        Assert.Contains(product.Versions, v => v.Id == "2024-06-01" && v.Beta);
    }

    [Fact(DisplayName = "BuildProductAsync processes all resources in a version")]
    public async Task BuildProductAsync_ProcessesAllResources()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        VertexResource emailVertex = TestDataBuilder.CreateVertexResource("email", "Email");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex, emailVertex]);

        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            canCreate: true,
            canUpdate: true,
            canDestroy: false);

        VertexDocument emailDoc = TestDataBuilder.CreateVertexDocument(
            id: "email",
            name: "Email",
            canCreate: true,
            canUpdate: false,
            canDestroy: true);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "email").Returns(emailDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Api.Models.Version version = product.Versions.First();
        Assert.Equal(2, version.Resources.Count());

        Resource? personResource = version.Resources.FirstOrDefault(r => r.Id == "person");
        Assert.NotNull(personResource);
        Assert.Equal("Person", personResource.Name);

        Resource? emailResource = version.Resources.FirstOrDefault(r => r.Id == "email");
        Assert.NotNull(emailResource);
        Assert.Equal("Email", emailResource.Name);
    }

    [Fact(DisplayName = "BuildProductAsync maps resource attributes correctly")]
    public async Task BuildProductAsync_MapsResourceAttributes()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        AttributeResource firstNameAttr = TestDataBuilder.CreateAttributeResource(
            "first_name", "string", "Person's first name");
        AttributeResource lastNameAttr = TestDataBuilder.CreateAttributeResource(
            "last_name", "string", "Person's last name");
        AttributeResource ageAttr = TestDataBuilder.CreateAttributeResource(
            "age", "integer", "Person's age");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);

        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            attributes: [firstNameAttr, lastNameAttr, ageAttr]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal(3, resource.Attributes.Count());
        Assert.Contains(resource.Attributes, a => a.Name == "first_name" && a.Type == "string");
        Assert.Contains(resource.Attributes, a => a.Name == "last_name" && a.Type == "string");
        Assert.Contains(resource.Attributes, a => a.Name == "age" && a.Type == "integer");
    }

    [Fact(DisplayName = "BuildProductAsync maps resource relationships correctly")]
    public async Task BuildProductAsync_MapsResourceRelationships()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        RelationshipResource emailsRel = TestDataBuilder.CreateRelationshipResource(
            "emails", "Email", "has_many");
        RelationshipResource organizationRel = TestDataBuilder.CreateRelationshipResource(
            "organization", "Organization", "belongs_to");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);

        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            relationships: [emailsRel, organizationRel]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal(2, resource.Relationships.Count());
        Assert.Contains(resource.Relationships, r => r.Name == "emails" && r.Type == "Email");
        Assert.Contains(resource.Relationships, r => r.Name == "organization" && r.Type == "Organization");
    }

    [Fact(DisplayName = "BuildProductAsync maps includable parameters correctly")]
    public async Task BuildProductAsync_MapsIncludableParameters()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        UrlParameterResource emailsParam = TestDataBuilder.CreateUrlParameterResource(
            "emails", "include", "string", "emails", "Include email addresses");
        UrlParameterResource phonesParam = TestDataBuilder.CreateUrlParameterResource(
            "phone_numbers", "include", "string", "phone_numbers", "Include phone numbers");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);

        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            canInclude: [emailsParam, phonesParam]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal(2, resource.CanInclude.Count());
        Assert.Contains(resource.CanInclude, i => i.Value == "emails");
        Assert.Contains(resource.CanInclude, i => i.Value == "phone_numbers");
    }

    [Fact(DisplayName = "BuildProductAsync maps orderable parameters correctly")]
    public async Task BuildProductAsync_MapsOrderableParameters()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        UrlParameterResource firstNameParam = TestDataBuilder.CreateUrlParameterResource(
            "first_name", "order", "string", "first_name", "Order by first name");
        UrlParameterResource lastNameParam = TestDataBuilder.CreateUrlParameterResource(
            "last_name", "order", "string", "last_name", "Order by last name");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);

        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            canOrder: [firstNameParam, lastNameParam]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal(2, resource.CanOrderBy.Count());
        Assert.Contains(resource.CanOrderBy, o => o.Value == "first_name");
        Assert.Contains(resource.CanOrderBy, o => o.Value == "last_name");
    }

    [Fact(DisplayName = "BuildProductAsync maps queryable parameters correctly")]
    public async Task BuildProductAsync_MapsQueryableParameters()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        UrlParameterResource searchParam = TestDataBuilder.CreateUrlParameterResource(
            "search", "where", "string", "search_name_or_email", "Search by name or email");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);

        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            canQuery: [searchParam]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Single(resource.CanQueryBy);
        Assert.Equal("search", resource.CanQueryBy.First().Name);
    }

    [Fact(DisplayName = "BuildProductAsync handles resources with deprecated flag")]
    public async Task BuildProductAsync_HandlesDeprecatedResources()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.Services;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource planVertex = TestDataBuilder.CreateVertexResource("plan", "Plan");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [planVertex]);

        VertexDocument planDoc = TestDataBuilder.CreateVertexDocument(
            id: "plan",
            name: "Plan",
            deprecated: true);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "plan").Returns(planDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.True(resource.Deprecated);
    }

    [Fact(DisplayName = "BuildProductAsync handles resources with collection only flag")]
    public async Task BuildProductAsync_HandlesCollectionOnlyResources()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.CheckIns;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource eventVertex = TestDataBuilder.CreateVertexResource("event", "Event");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [eventVertex]);

        VertexDocument eventDoc = TestDataBuilder.CreateVertexDocument(
            id: "event",
            name: "Event",
            collectionOnly: true);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "event").Returns(eventDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.True(resource.CollectionOnly);
    }

    [Fact(DisplayName = "BuildProductAsync respects concurrent call limit")]
    public async Task BuildProductAsync_RespectsConcurrentCallLimit()
    {
        // Arrange - Create a product with many versions to test concurrent limiting
        ProductDefinition productDef = ProductDefinition.People;
        List<VersionResource> versions = [];
        for (int i = 1; i <= 15; i++)
        {
            versions.Add(TestDataBuilder.CreateVersionResource($"2024-{i:D2}-01"));
        }

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [.. versions]);
        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);

        foreach (VersionResource version in versions)
        {
            GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
                id: version.Id!,
                vertices: []);
            _mockClient.GetGraphVersionAsync(productDef, version.Id!).Returns(versionDoc);
        }

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert - Verify all versions were processed
        Assert.Equal(15, product.Versions.Count());

        // Verify all API calls were made (semaphore should have allowed them all through, just limiting concurrency)
        foreach (VersionResource version in versions)
        {
            await _mockClient.Received(1).GetGraphVersionAsync(productDef, version.Id!);
        }
    }

    [Fact(DisplayName = "ConcurrentConnections configuration limits parallel API calls")]
    public async Task ConcurrentConnectionsConfiguration_LimitsParallelApiCalls()
    {
        // Arrange - Configure with a specific concurrent connection limit
        int maxConcurrentRequests = 3;
        int currentConcurrentCalls = 0;
        int maxObservedConcurrentCalls = 0;
        object lockObject = new();

        IPlanningCenterClient mockClient = Substitute.For<IPlanningCenterClient>();
        ILogger<DocumentationBuilder> mockLogger = Substitute.For<ILogger<DocumentationBuilder>>();

        DocumentationBuilder builder = new(mockLogger, mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new()
            {
                ConcurrentConnections = maxConcurrentRequests
            }));

        // Create a product with multiple versions to trigger concurrent calls
        ProductDefinition productDef = ProductDefinition.Services;
        List<VersionResource> versions = [];
        for (int i = 1; i <= 10; i++)
        {
            versions.Add(TestDataBuilder.CreateVersionResource($"2024-{i:D2}-01"));
        }

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [.. versions]);

        // Set up mock to track concurrent calls
        mockClient.GetGraphAsync(productDef).Returns(callInfo =>
        {
            return Task.Run(async () =>
            {
                lock (lockObject)
                {
                    currentConcurrentCalls++;
                    maxObservedConcurrentCalls = Math.Max(maxObservedConcurrentCalls, currentConcurrentCalls);
                }

                await Task.Delay(50); // Simulate network delay

                lock (lockObject)
                {
                    currentConcurrentCalls--;
                }

                return graphDoc;
            });
        });

        foreach (VersionResource version in versions)
        {
            GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
                id: version.Id!,
                vertices: []);

            mockClient.GetGraphVersionAsync(productDef, version.Id!).Returns(callInfo =>
            {
                return Task.Run(async () =>
                {
                    lock (lockObject)
                    {
                        currentConcurrentCalls++;
                        maxObservedConcurrentCalls = Math.Max(maxObservedConcurrentCalls, currentConcurrentCalls);
                    }

                    await Task.Delay(50); // Simulate network delay

                    lock (lockObject)
                    {
                        currentConcurrentCalls--;
                    }

                    return versionDoc;
                });
            });
        }

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Assert.Equal(10, product.Versions.Count());

        // Verify that concurrent calls never exceeded the configured limit
        Assert.True(maxObservedConcurrentCalls <= maxConcurrentRequests,
            $"Expected max concurrent calls to be <= {maxConcurrentRequests}, but observed {maxObservedConcurrentCalls}");

        // Verify that concurrent calls actually happened (should be > 1 with 10 versions)
        Assert.True(maxObservedConcurrentCalls > 1,
            $"Expected concurrent execution, but max concurrent calls was {maxObservedConcurrentCalls}");
    }
}
