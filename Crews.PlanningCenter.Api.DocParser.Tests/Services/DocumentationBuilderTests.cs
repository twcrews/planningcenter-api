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
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create<DocumentationTransforms>(new()));
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

        Resource? personResource = version.Resources.FirstOrDefault(r => r.JsonName == "person");
        Assert.NotNull(personResource);
        Assert.Equal("Person", personResource.AttributesClrType);

        Resource? emailResource = version.Resources.FirstOrDefault(r => r.JsonName == "email");
        Assert.NotNull(emailResource);
        Assert.Equal("Email", emailResource.AttributesClrType);
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
        Assert.Contains(resource.Attributes, a => a.JsonName == "first_name" && a.ClrType == "string");
        Assert.Contains(resource.Attributes, a => a.JsonName == "last_name" && a.ClrType == "string");
        Assert.Contains(resource.Attributes, a => a.JsonName == "age" && a.ClrType == "int");
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
        Assert.Contains(resource.Relationships, r => r.JsonName == "emails" && r.AttributesClrType == "Email");
        Assert.Contains(resource.Relationships, r => r.JsonName == "organization" && r.AttributesClrType == "Organization");
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
            "search", "where[name_or_email]", "string", "search_name_or_email", "Search by name or email");

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
        Assert.Equal("where[name_or_email]", resource.CanQueryBy.First().Parameter);
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

    [Fact(DisplayName = "BuildProductAsync appends additional outbound edges as children")]
    public async Task BuildProductAsync_AppliesToAdditionalOutboundEdges()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        EdgeResource additionalEdge = TestDataBuilder.CreateEdgeResource(
            "custom_senders",
            "https://api.planningcenteronline.com/people/v2/custom_senders",
            "custom_sender",
            "CustomSender");

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Product = "people",
                    Resource = "person",
                    Child = new()
                    {
                        JsonName = additionalEdge.Attributes.Name,
                        Slug = new Uri(additionalEdge.Attributes.Path).Segments[^1],
                        AttributesClrType = additionalEdge.Relationships.Head.Data.Attributes!.Name
                    }
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Single(resource.Children);
        Assert.Contains(resource.Children, c => c.JsonName == "custom_senders");
    }

    [Fact(DisplayName = "BuildProductAsync appends additional outbound edges alongside existing edges")]
    public async Task BuildProductAsync_AdditionalOutboundEdges_ConcatenatesWithExistingEdges()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        EdgeResource existingEdge = TestDataBuilder.CreateEdgeResource(
            "emails",
            "https://api.planningcenteronline.com/people/v2/emails",
            "email",
            "Email");
        EdgeResource additionalEdge = TestDataBuilder.CreateEdgeResource(
            "custom_senders",
            "https://api.planningcenteronline.com/people/v2/custom_senders",
            "custom_sender",
            "CustomSender");

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Product = "people",
                    Resource = "person",
                    Child = new()
                    {
                        JsonName = additionalEdge.Attributes.Name,
                        Slug = new Uri(additionalEdge.Attributes.Path).Segments[^1],
                        AttributesClrType = additionalEdge.Relationships.Head.Data.Attributes!.Name
                    }
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", outboundEdges: [existingEdge]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal(2, resource.Children.Count());
        Assert.Contains(resource.Children, c => c.JsonName == "emails");
        Assert.Contains(resource.Children, c => c.JsonName == "custom_senders");
    }

    [Fact(DisplayName = "BuildProductAsync converts dotted attribute name to PascalCase with dot preserved")]
    public async Task BuildProductAsync_MapsAttributeWithDottedName()
    {
        // Arrange: attribute name containing a dot (e.g. "address.street") —
        // exercises the '.' branch in ToPascalCase which keeps the dot and
        // capitalizes the character that follows it.
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        AttributeResource dottedAttr = TestDataBuilder.CreateAttributeResource(
            "address.street", "string", "Street portion of the address");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            attributes: [dottedAttr]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert: dot is preserved and character after dot is capitalised
        ResourceAttribute attr = product.Versions.First().Resources.First().Attributes.First();
        Assert.Equal("Address.Street", attr.ClrName);
    }

    [Fact(DisplayName = "BuildProductAsync capitalises letter following a digit in attribute name")]
    public async Task BuildProductAsync_MapsAttributeWithDigitToLetterTransition()
    {
        // Arrange: attribute name where a letter immediately follows a digit
        // (e.g. "track1title") — exercises the digit-to-letter branch in
        // ToPascalCase which capitalises the letter after a digit.
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        AttributeResource digitAttr = TestDataBuilder.CreateAttributeResource(
            "track1title", "string", "Title of track 1");

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01",
            vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person",
            name: "Person",
            attributes: [digitAttr]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert: the letter following the digit is capitalised
        ResourceAttribute attr = product.Versions.First().Resources.First().Attributes.First();
        Assert.Equal("Track1Title", attr.ClrName);
    }

    [Fact(DisplayName = "BuildProductAsync includes additional outbound edges when Product is null")]
    public async Task BuildProductAsync_AdditionalOutboundEdges_NullProductMatchesAll()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.Calendar;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource orgVertex = TestDataBuilder.CreateVertexResource("organization", "Organization");
        EdgeResource additionalEdge = TestDataBuilder.CreateEdgeResource(
            "extra_resources",
            "https://api.planningcenteronline.com/calendar/v2/extra_resources",
            "extra_resource",
            "ExtraResource");

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Product = null, // matches any product
                    Resource = "organization",
                    Child = new()
                    {
                        JsonName = additionalEdge.Attributes.Name,
                        Slug = new Uri(additionalEdge.Attributes.Path).Segments[^1],
                        AttributesClrType = additionalEdge.Relationships.Head.Data.Attributes!.Name
                    }
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [orgVertex]);
        VertexDocument orgDoc = TestDataBuilder.CreateVertexDocument(
            id: "organization", name: "Organization");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "organization").Returns(orgDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Contains(resource.Children, c => c.JsonName == "extra_resources");
    }

    [Fact(DisplayName = "BuildProductAsync excludes additional outbound edges for non-matching vertex")]
    public async Task BuildProductAsync_AdditionalOutboundEdges_IgnoresNonMatchingVertex()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        EdgeResource additionalEdge = TestDataBuilder.CreateEdgeResource(
            "custom_senders",
            "https://api.planningcenteronline.com/people/v2/custom_senders",
            "custom_sender",
            "CustomSender");

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Product = "people",
                    Resource = "organization", // does not match "person"
                    Child = new()
                    {
                        JsonName = additionalEdge.Attributes.Name,
                        Slug = new Uri(additionalEdge.Attributes.Path).Segments[^1],
                        AttributesClrType = additionalEdge.Relationships.Head.Data.Attributes!.Name
                    }
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Empty(resource.Children);
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

    [Fact(DisplayName = "BuildProductAsync applies resource property overrides")]
    public async Task BuildProductAsync_ResourceOverride_AppliesOverriddenProperties()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        DocumentationTransforms transforms = new()
        {
            ResourceOverrides =
            [
                new()
                {
                    Product = "people",
                    Version = "2024-01-01",
                    Resource = "person",
                    JsonName = "person_overridden",
                    AttributesClrType = "CustomAttributes",
                    ResourceClrType = "CustomResource",
                    Description = "Custom description",
                    Deprecated = true,
                    CollectionOnly = true,
                    Postable = false,
                    Patchable = false,
                    Deletable = false,
                    ShouldGenerateClients = false,
                    ShouldGenerateResource = false
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", canCreate: true, canUpdate: true, canDestroy: true);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal("person_overridden", resource.JsonName);
        Assert.Equal("CustomAttributes", resource.AttributesClrType);
        Assert.Equal("CustomResource", resource.ResourceClrType);
        Assert.Equal("Custom description", resource.Description);
        Assert.True(resource.Deprecated);
        Assert.True(resource.CollectionOnly);
        Assert.False(resource.Postable);
        Assert.False(resource.Patchable);
        Assert.False(resource.Deletable);
        Assert.False(resource.ShouldGenerateClients);
        Assert.False(resource.ShouldGenerateResource);
    }

    [Fact(DisplayName = "BuildProductAsync skips resource override when product does not match")]
    public async Task BuildProductAsync_ResourceOverride_SkipsWhenProductDoesNotMatch()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        DocumentationTransforms transforms = new()
        {
            ResourceOverrides =
            [
                new()
                {
                    Product = "calendar", // does not match "People"
                    Resource = "person",
                    JsonName = "person_overridden"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal("person", resource.JsonName);
    }

    [Fact(DisplayName = "BuildProductAsync skips resource override when version does not match")]
    public async Task BuildProductAsync_ResourceOverride_SkipsWhenVersionDoesNotMatch()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        DocumentationTransforms transforms = new()
        {
            ResourceOverrides =
            [
                new()
                {
                    Resource = "person",
                    Version = "2025-01-01", // does not match "2024-01-01"
                    JsonName = "person_overridden"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal("person", resource.JsonName);
    }

    [Fact(DisplayName = "BuildProductAsync applies resource override when product is null")]
    public async Task BuildProductAsync_ResourceOverride_NullProductMatchesAllProducts()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.Calendar;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource orgVertex = TestDataBuilder.CreateVertexResource("organization", "Organization");

        DocumentationTransforms transforms = new()
        {
            ResourceOverrides =
            [
                new()
                {
                    Product = null, // matches any product
                    Resource = "organization",
                    JsonName = "org_overridden"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [orgVertex]);
        VertexDocument orgDoc = TestDataBuilder.CreateVertexDocument(id: "organization", name: "Organization");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "organization").Returns(orgDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal("org_overridden", resource.JsonName);
    }

    [Fact(DisplayName = "BuildProductAsync applies resource override when version is null")]
    public async Task BuildProductAsync_ResourceOverride_NullVersionMatchesAllVersions()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        DocumentationTransforms transforms = new()
        {
            ResourceOverrides =
            [
                new()
                {
                    Resource = "person",
                    Version = null, // matches any version
                    JsonName = "person_overridden"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Resource resource = product.Versions.First().Resources.First();
        Assert.Equal("person_overridden", resource.JsonName);
    }

    [Fact(DisplayName = "BuildProductAsync applies attribute property overrides")]
    public async Task BuildProductAsync_AttributeOverride_AppliesOverriddenProperties()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        AttributeResource statusAttr = TestDataBuilder.CreateAttributeResource("status", "string", "Status");

        DocumentationTransforms transforms = new()
        {
            AttributeOverrides =
            [
                new()
                {
                    Product = "people",
                    Version = "2024-01-01",
                    Resource = "person",
                    Attribute = "status",
                    JsonName = "status_code",
                    ClrName = "StatusCode",
                    ClrType = "int",
                    Description = "Custom status description",
                    JsonConverter = "StatusConverter"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", attributes: [statusAttr]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceAttribute attr = product.Versions.First().Resources.First().Attributes.First();
        Assert.Equal("status_code", attr.JsonName);
        Assert.Equal("StatusCode", attr.ClrName);
        Assert.Equal("int", attr.ClrType);
        Assert.Equal("Custom status description", attr.Description);
        Assert.Equal("StatusConverter", attr.JsonConverter);
    }

    [Fact(DisplayName = "BuildProductAsync applies attribute override when resource is null")]
    public async Task BuildProductAsync_AttributeOverride_NullResourceMatchesAllResources()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        AttributeResource statusAttr = TestDataBuilder.CreateAttributeResource("status", "string");

        DocumentationTransforms transforms = new()
        {
            AttributeOverrides =
            [
                new()
                {
                    Attribute = "status",
                    Resource = null, // matches any resource
                    ClrType = "CustomStatusEnum"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", attributes: [statusAttr]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceAttribute attr = product.Versions.First().Resources.First().Attributes.First();
        Assert.Equal("CustomStatusEnum", attr.ClrType);
    }

    [Fact(DisplayName = "BuildProductAsync skips attribute override when resource does not match")]
    public async Task BuildProductAsync_AttributeOverride_SkipsWhenResourceDoesNotMatch()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        AttributeResource statusAttr = TestDataBuilder.CreateAttributeResource("status", "string");

        DocumentationTransforms transforms = new()
        {
            AttributeOverrides =
            [
                new()
                {
                    Attribute = "status",
                    Resource = "organization", // does not match "person"
                    ClrType = "CustomStatusEnum"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", attributes: [statusAttr]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceAttribute attr = product.Versions.First().Resources.First().Attributes.First();
        Assert.Equal("string", attr.ClrType);
    }

    [Fact(DisplayName = "BuildProductAsync applies relationship property overrides")]
    public async Task BuildProductAsync_RelationshipOverride_AppliesOverriddenProperties()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        RelationshipResource emailsRel = TestDataBuilder.CreateRelationshipResource(
            "emails", "Email", "belongs_to"); // not to_many, so IsCollection would be false without override

        DocumentationTransforms transforms = new()
        {
            RelationshipOverrides =
            [
                new()
                {
                    Product = "people",
                    Version = "2024-01-01",
                    Resource = "person",
                    Relationship = "emails",
                    JsonName = "email_addresses",
                    ClrName = "EmailAddresses",
                    AttributesClrType = "EmailAddress",
                    ResourceClrType = "EmailAddressResource",
                    IsCollection = "true", // force collection even though association is not to_many
                    Description = "Custom description"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", relationships: [emailsRel]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceRelationship rel = product.Versions.First().Resources.First().Relationships.First();
        Assert.Equal("email_addresses", rel.JsonName);
        Assert.Equal("EmailAddresses", rel.ClrName);
        Assert.Equal("EmailAddress", rel.AttributesClrType);
        Assert.Equal("EmailAddressResource", rel.ResourceClrType);
        Assert.True(rel.IsCollection);
        Assert.Equal("Custom description", rel.Description);
    }

    [Fact(DisplayName = "BuildProductAsync applies relationship override when resource is null")]
    public async Task BuildProductAsync_RelationshipOverride_NullResourceMatchesAllResources()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        RelationshipResource orgRel = TestDataBuilder.CreateRelationshipResource(
            "organization", "Organization", "belongs_to");

        DocumentationTransforms transforms = new()
        {
            RelationshipOverrides =
            [
                new()
                {
                    Relationship = "organization",
                    Resource = null, // matches any resource
                    AttributesClrType = "Org"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", relationships: [orgRel]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceRelationship rel = product.Versions.First().Resources.First().Relationships.First();
        Assert.Equal("Org", rel.AttributesClrType);
    }

    [Fact(DisplayName = "BuildProductAsync skips relationship override when resource does not match")]
    public async Task BuildProductAsync_RelationshipOverride_SkipsWhenResourceDoesNotMatch()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        RelationshipResource orgRel = TestDataBuilder.CreateRelationshipResource(
            "organization", "Organization", "belongs_to");

        DocumentationTransforms transforms = new()
        {
            RelationshipOverrides =
            [
                new()
                {
                    Relationship = "organization",
                    Resource = "email", // does not match "person"
                    AttributesClrType = "Org"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", relationships: [orgRel]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceRelationship rel = product.Versions.First().Resources.First().Relationships.First();
        Assert.Equal("Organization", rel.AttributesClrType);
    }

    [Fact(DisplayName = "BuildProductAsync applies resource child property overrides")]
    public async Task BuildProductAsync_ChildOverride_AppliesOverriddenProperties()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        EdgeResource emailsEdge = TestDataBuilder.CreateEdgeResource(
            "emails",
            "https://api.planningcenteronline.com/people/v2/emails",
            "email",
            "Email");

        DocumentationTransforms transforms = new()
        {
            ResourceChildOverrides =
            [
                new()
                {
                    Product = "people",
                    Resource = "person",
                    Child = "emails",
                    JsonName = "email_addresses",
                    ClrName = "EmailAddresses",
                    AttributesClrType = "EmailAddress",
                    Description = "Custom child description",
                    Slug = "email_addresses",
                    IsCollection = false,
                    Deprecated = true
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", outboundEdges: [emailsEdge]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceChild child = product.Versions.First().Resources.First().Children.First();
        Assert.Equal("email_addresses", child.JsonName);
        Assert.Equal("EmailAddresses", child.ClrName);
        Assert.Equal("EmailAddress", child.AttributesClrType);
        Assert.Equal("Custom child description", child.Description);
        Assert.Equal("email_addresses", child.Slug);
        Assert.False(child.IsCollection);
        Assert.True(child.Deprecated);
    }

    [Fact(DisplayName = "BuildProductAsync applies child override when resource is null")]
    public async Task BuildProductAsync_ChildOverride_NullResourceMatchesAllResources()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        EdgeResource notesEdge = TestDataBuilder.CreateEdgeResource(
            "notes",
            "https://api.planningcenteronline.com/people/v2/notes",
            "note",
            "Note");

        DocumentationTransforms transforms = new()
        {
            ResourceChildOverrides =
            [
                new()
                {
                    Child = "notes",
                    Resource = null, // matches any resource
                    ClrName = "NoteItems"
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", outboundEdges: [notesEdge]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceChild child = product.Versions.First().Resources.First().Children.First();
        Assert.Equal("NoteItems", child.ClrName);
    }

    [Fact(DisplayName = "BuildProductAsync builds child filters from edge scopes")]
    public async Task BuildProductAsync_BuildsChildFiltersFromEdgeScopes()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        EdgeResource emailsEdge = TestDataBuilder.CreateEdgeResource(
            "emails",
            "https://api.planningcenteronline.com/people/v2/emails",
            scopes:
            [
                new() { Name = "active", ScopeHelp = "Filter by active emails." },
                new() { Name = "primary", ScopeHelp = null }
            ]);

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", outboundEdges: [emailsEdge]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        ResourceChild child = product.Versions.First().Resources.First().Children.First();
        ResourceChildFilter[] filters = [.. child.Filters];
        Assert.Equal(2, filters.Length);
        Assert.Equal("FilterByActive", filters[0].ClrMethodName);
        Assert.Equal("active", filters[0].Value);
        Assert.Equal("Filter by active emails.", filters[0].Description);
        Assert.Equal("FilterByPrimary", filters[1].ClrMethodName);
        Assert.Equal("primary", filters[1].Value);
        Assert.Equal("Filter results by the `Primary` scope.", filters[1].Description);
    }

    [Fact(DisplayName = "BuildProductAsync builds action properties")]
    public async Task BuildProductAsync_BuildsActionProperties()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        ActionResource promoteAction = TestDataBuilder.CreateActionResource(
            name: "promote",
            canRun: "admin",
            description: "Promotes the person.",
            details: "Additional details.",
            deprecated: false);

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", actions: [promoteAction]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        ResourceAction action = product.Versions.First().Resources.First().Actions.First();
        Assert.Equal("promote", action.JsonName);
        Assert.Equal("PromoteAsync", action.ClrMethodName);
        Assert.Equal("promote", action.Path);
        Assert.Equal("admin", action.Requirements);
        Assert.Equal("Promotes the person.", action.Description);
        Assert.Equal("Additional details.", action.AdditionalDetails);
        Assert.False(action.Deprecated);
    }

    [Fact(DisplayName = "BuildProductAsync uses default action description when missing")]
    public async Task BuildProductAsync_BuildsAction_UsesDefaultDescription_WhenMissing()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");
        ActionResource promoteAction = TestDataBuilder.CreateActionResource(
            name: "promote",
            description: null);

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(
            id: "person", name: "Person", actions: [promoteAction]);

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await _builder.BuildProductAsync(productDef);

        // Assert
        ResourceAction action = product.Versions.First().Resources.First().Actions.First();
        Assert.Equal("Planning Center does not provide a description for this action.", action.Description);
    }

    [Fact(DisplayName = "BuildProductAsync includes additional resource children that match product/version/resource")]
    public async Task BuildProductAsync_AdditionalResourceChildren_IncludesMatchingChildren()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        ResourceChild extraChild = new()
        {
            JsonName = "form_submissions",
            ClrName = "FormSubmissions",
            AttributesClrType = "FormSubmission",
            Description = "Form submissions for this person.",
            Slug = "form_submissions",
            IsCollection = true,
            Deprecated = false
        };

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Product = "people",
                    Version = "2024-01-01",
                    Resource = "person",
                    Child = extraChild
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        ResourceChild child = product.Versions.First().Resources.First().Children.Single();
        Assert.Equal("form_submissions", child.JsonName);
        Assert.Equal("FormSubmissions", child.ClrName);
    }

    [Fact(DisplayName = "BuildProductAsync includes additional children when product is null")]
    public async Task BuildProductAsync_AdditionalResourceChildren_NullProductMatchesAllProducts()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.Calendar;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource orgVertex = TestDataBuilder.CreateVertexResource("organization", "Organization");

        ResourceChild extraChild = new()
        {
            JsonName = "tags",
            ClrName = "Tags",
            AttributesClrType = "Tag",
            Description = "Tags.",
            Slug = "tags",
            IsCollection = true
        };

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Product = null, // matches any product
                    Resource = "organization",
                    Child = extraChild
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [orgVertex]);
        VertexDocument orgDoc = TestDataBuilder.CreateVertexDocument(id: "organization", name: "Organization");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "organization").Returns(orgDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Assert.Single(product.Versions.First().Resources.First().Children);
    }

    [Fact(DisplayName = "BuildProductAsync includes additional children when version is null")]
    public async Task BuildProductAsync_AdditionalResourceChildren_NullVersionMatchesAllVersions()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        ResourceChild extraChild = new()
        {
            JsonName = "notes",
            ClrName = "Notes",
            AttributesClrType = "Note",
            Description = "Notes.",
            Slug = "notes",
            IsCollection = true
        };

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Version = null, // matches any version
                    Resource = "person",
                    Child = extraChild
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Assert.Single(product.Versions.First().Resources.First().Children);
    }

    [Fact(DisplayName = "BuildProductAsync skips additional children when resource does not match")]
    public async Task BuildProductAsync_AdditionalResourceChildren_SkipsNonMatchingResource()
    {
        // Arrange
        ProductDefinition productDef = ProductDefinition.People;
        VersionResource versionRes = TestDataBuilder.CreateVersionResource("2024-01-01");
        VertexResource personVertex = TestDataBuilder.CreateVertexResource("person", "Person");

        ResourceChild extraChild = new()
        {
            JsonName = "notes",
            ClrName = "Notes",
            AttributesClrType = "Note",
            Description = "Notes.",
            Slug = "notes",
            IsCollection = true
        };

        DocumentationTransforms transforms = new()
        {
            AdditionalResourceChildren =
            [
                new()
                {
                    Resource = "organization", // does not match "person"
                    Child = extraChild
                }
            ]
        };

        DocumentationBuilder builder = new(_mockLogger, _mockClient,
            Options.Create<AppSettings.DocumentationBuilderOptions>(new() { ConcurrentConnections = 10 }),
            Options.Create(transforms));

        GraphDocument graphDoc = TestDataBuilder.CreateGraphDocument(versions: [versionRes]);
        GraphVersionDocument versionDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: "2024-01-01", vertices: [personVertex]);
        VertexDocument personDoc = TestDataBuilder.CreateVertexDocument(id: "person", name: "Person");

        _mockClient.GetGraphAsync(productDef).Returns(graphDoc);
        _mockClient.GetGraphVersionAsync(productDef, "2024-01-01").Returns(versionDoc);
        _mockClient.GetVertexAsync(productDef, "2024-01-01", "person").Returns(personDoc);

        // Act
        Product product = await builder.BuildProductAsync(productDef);

        // Assert
        Assert.Empty(product.Versions.First().Resources.First().Children);
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
            }),
            Options.Create<DocumentationTransforms>(new()));

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
