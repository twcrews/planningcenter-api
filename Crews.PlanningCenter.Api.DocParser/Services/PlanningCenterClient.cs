using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.Models;
using Crews.Web.JsonApiClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.DocParser.Services;

public class PlanningCenterClient : IPlanningCenterClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PlanningCenterClient> _logger;

    public PlanningCenterClient(
        HttpClient httpClient, 
        ILogger<PlanningCenterClient> logger,
        IOptions<AppSettings.PlanningCenterClientOptions> options)
    {
        _httpClient = httpClient;
        _logger = logger;
        if (options.Value.BaseAddress is not null) _httpClient.BaseAddress = new Uri(options.Value.BaseAddress);
    }

    public Task<GraphDocument> GetGraphAsync(ProductDefinition product)
    {
        _logger.LogDebug("Fetching Graph document for product {Product}", product);
        return GetDocumentAsync<GraphDocument>($"{product}/v2/documentation");
    }

    public Task<GraphVersionDocument> GetGraphVersionAsync(ProductDefinition product, string versionId)
    {
        _logger.LogDebug("Fetching GraphVersion document for product {Product}, version {VersionId}", 
            product, versionId);
        return GetDocumentAsync<GraphVersionDocument>($"{product}/v2/documentation/{versionId}");
    }

    public Task<VertexDocument> GetVertexAsync(ProductDefinition product, string versionId, string vertexId)
    {
        _logger.LogDebug("Fetching Vertex document for product {Product}, version {VersionId}, vertex {VertexId}", 
            product, versionId, vertexId);
        return GetDocumentAsync<VertexDocument>($"{product}/v2/documentation/{versionId}/vertices/{vertexId}");
    }

    private async Task<T> GetDocumentAsync<T>(string requestUri) where T : JsonApiDocument
    {
        _logger.LogTrace("Sending GET request to {RequestUri}", requestUri);
        HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        T document = await response.Content.ReadFromJsonAsync<T>()
          ?? throw new JsonException("Failed to deserialize response: deserialized content was null");
        return document;
    }
}
