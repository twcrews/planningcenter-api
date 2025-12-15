using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Incoming;
using Crews.PlanningCenter.Api.DocParser.Services;
using Crews.PlanningCenter.Api.DocParser.Tests.Fixtures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.DocParser.Tests.Services;

public class PlanningCenterClientTests : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PlanningCenterClient> _mockLogger;
    private readonly MockHttpMessageHandler _mockHandler;
    private readonly PlanningCenterClient _client;

    public PlanningCenterClientTests()
    {
        _mockHandler = new MockHttpMessageHandler();
        _httpClient = new HttpClient(_mockHandler)
        {
            BaseAddress = new Uri("https://api.planningcenteronline.com/")
        };
        _mockLogger = Substitute.For<ILogger<PlanningCenterClient>>();
        _client = new PlanningCenterClient(_httpClient, _mockLogger, 
            Options.Create<AppSettings.PlanningCenterClientOptions>(new() { BaseAddress = "https://test.com" }));
    }

    [Fact(DisplayName = "GetGraphAsync sends request to correct endpoint")]
    public async Task GetGraphAsync_SendsRequestToCorrectEndpoint()
    {
        // Arrange
        ProductDefinition product = ProductDefinition.People;
        GraphDocument expectedDoc = TestDataBuilder.CreateGraphDocument(
            title: "People API",
            description: "Manage people");

        _mockHandler.SetupResponse(
            "people/v2/documentation",
            expectedDoc);

        // Act
        GraphDocument result = await _client.GetGraphAsync(product);

        // Assert
        Assert.Equal("People API", result.Data.Attributes.Title);
        Assert.Equal("Manage people", result.Data.Attributes.Description);
        Assert.Single(_mockHandler.RequestHistory);
        Assert.EndsWith("people/v2/documentation", _mockHandler.RequestHistory[0].RequestUri?.ToString());
    }

    [Fact(DisplayName = "GetGraphVersionAsync sends request to correct endpoint")]
    public async Task GetGraphVersionAsync_SendsRequestToCorrectEndpoint()
    {
        // Arrange
        ProductDefinition product = ProductDefinition.Calendar;
        string versionId = "2024-01-01";
        GraphVersionDocument expectedDoc = TestDataBuilder.CreateGraphVersionDocument(
            id: versionId,
            beta: false,
            details: "Stable version");

        _mockHandler.SetupResponse(
            $"calendar/v2/documentation/{versionId}",
            expectedDoc);

        // Act
        GraphVersionDocument result = await _client.GetGraphVersionAsync(product, versionId);

        // Assert
        Assert.Equal(versionId, result.Data.Id);
        Assert.False(result.Data.Attributes.Beta);
        Assert.Equal("Stable version", result.Data.Attributes.Details);
        Assert.Single(_mockHandler.RequestHistory);
        Assert.EndsWith($"calendar/v2/documentation/{versionId}",
            _mockHandler.RequestHistory[0].RequestUri?.ToString());
    }

    [Fact(DisplayName = "GetVertexAsync sends request to correct endpoint")]
    public async Task GetVertexAsync_SendsRequestToCorrectEndpoint()
    {
        // Arrange
        ProductDefinition product = ProductDefinition.Services;
        string versionId = "2024-01-01";
        string vertexId = "plan";
        VertexDocument expectedDoc = TestDataBuilder.CreateVertexDocument(
            id: vertexId,
            name: "Plan");

        _mockHandler.SetupResponse(
            $"services/v2/documentation/{versionId}/vertices/{vertexId}",
            expectedDoc);

        // Act
        VertexDocument result = await _client.GetVertexAsync(product, versionId, vertexId);

        // Assert
        Assert.Equal(vertexId, result.Data.Id);
        Assert.Equal("Plan", result.Data.Attributes?.Name);
        Assert.Single(_mockHandler.RequestHistory);
        Assert.EndsWith($"services/v2/documentation/{versionId}/vertices/{vertexId}",
            _mockHandler.RequestHistory[0].RequestUri?.ToString());
    }

    [Fact(DisplayName = "GetGraphAsync throws when response is null")]
    public async Task GetGraphAsync_ThrowsWhenResponseIsNull()
    {
        // Arrange
        ProductDefinition product = ProductDefinition.Giving;
        _mockHandler.SetupNullResponse("giving/v2/documentation");

        // Act & Assert
        await Assert.ThrowsAsync<JsonException>(async () =>
            await _client.GetGraphAsync(product));
    }

    [Fact(DisplayName = "GetGraphVersionAsync handles different products")]
    public async Task GetGraphVersionAsync_HandlesDifferentProducts()
    {
        // Arrange
        ProductDefinition[] products = [
            ProductDefinition.Calendar,
            ProductDefinition.CheckIns,
            ProductDefinition.Giving,
            ProductDefinition.Groups,
            ProductDefinition.People,
            ProductDefinition.Publishing,
            ProductDefinition.Services
        ];

        string versionId = "2024-01-01";

        foreach (ProductDefinition product in products)
        {
            GraphVersionDocument doc = TestDataBuilder.CreateGraphVersionDocument(id: versionId);
            _mockHandler.SetupResponse($"{product}/v2/documentation/{versionId}", doc);
        }

        // Act & Assert
        foreach (ProductDefinition product in products)
        {
            GraphVersionDocument result = await _client.GetGraphVersionAsync(product, versionId);
            Assert.NotNull(result);
            Assert.Equal(versionId, result.Data.Id);
        }

        Assert.Equal(products.Length, _mockHandler.RequestHistory.Count);
    }

    [Fact(DisplayName = "GetVertexAsync deserializes complex vertex document")]
    public async Task GetVertexAsync_DeserializesComplexVertexDocument()
    {
        // Arrange
        ProductDefinition product = ProductDefinition.People;
        string versionId = "2024-01-01";
        string vertexId = "person";

        AttributeResource[] attributes = [
            TestDataBuilder.CreateAttributeResource("first_name", "string"),
            TestDataBuilder.CreateAttributeResource("last_name", "string"),
            TestDataBuilder.CreateAttributeResource("age", "integer")
        ];

        RelationshipResource[] relationships = [
            TestDataBuilder.CreateRelationshipResource("emails", "Email", "has_many"),
            TestDataBuilder.CreateRelationshipResource("phones", "PhoneNumber", "has_many")
        ];

        UrlParameterResource[] canInclude = [
            TestDataBuilder.CreateUrlParameterResource("emails", "include", "string"),
            TestDataBuilder.CreateUrlParameterResource("phones", "include", "string")
        ];

        VertexDocument expectedDoc = TestDataBuilder.CreateVertexDocument(
            id: vertexId,
            name: "Person",
            attributes: attributes,
            relationships: relationships,
            canInclude: canInclude,
            canCreate: true,
            canUpdate: true,
            canDestroy: false);

        _mockHandler.SetupResponse(
            $"people/v2/documentation/{versionId}/vertices/{vertexId}",
            expectedDoc);

        // Act
        VertexDocument result = await _client.GetVertexAsync(product, versionId, vertexId);

        // Assert
        Assert.Equal(vertexId, result.Data.Id);
        Assert.Equal("Person", result.Data.Attributes?.Name);
        Assert.NotNull(result.Data.Relationships);
        Assert.Equal(3, result.Data.Relationships.Attributes.Data.Count());
        Assert.Equal(2, result.Data.Relationships.Relationships.Data.Count());
        Assert.Equal(2, result.Data.Relationships.CanInclude.Data.Count());
        Assert.True(result.Data.Relationships.Permissions.Data.Attributes.CanCreate);
        Assert.True(result.Data.Relationships.Permissions.Data.Attributes.CanUpdate);
        Assert.False(result.Data.Relationships.Permissions.Data.Attributes.CanDestroy);
    }

    public void Dispose() => GC.SuppressFinalize(this);

    /// <summary>
    /// Simple mock HTTP message handler for testing
    /// </summary>
    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, object> _responses = [];
        private readonly Dictionary<string, bool> _nullResponses = [];
        public List<HttpRequestMessage> RequestHistory { get; } = [];

        public void SetupResponse<T>(string path, T response)
        {
            _responses[path] = response!;
        }

        public void SetupNullResponse(string path)
        {
            _nullResponses[path] = true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestHistory.Add(request);

            string path = request.RequestUri?.PathAndQuery.TrimStart('/') ?? string.Empty;

            if (_nullResponses.ContainsKey(path))
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("null")
                };
            }

            if (_responses.TryGetValue(path, out object? response))
            {
                string json = JsonSerializer.Serialize(response);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
