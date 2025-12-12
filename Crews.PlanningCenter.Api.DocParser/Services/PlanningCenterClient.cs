using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Incoming;
using Crews.Web.JsonApiClient;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.DocParser.Services;

class PlanningCenterClient(
    HttpClient httpClient, 
    ILogger<PlanningCenterClient> logger, 
    JsonSerializerOptions? jsonSerializerOptions = null) 
    : IPlanningCenterClient
{
    public Task<GraphDocument> GetGraphAsync(ProductDefinition product)
    {
        logger.LogDebug("Fetching Graph document for product {Product}", product);
        return GetDocumentAsync<GraphDocument>($"{product}/v2/documentation");
    }

    public Task<GraphVersionDocument> GetGraphVersionAsync(ProductDefinition product, string versionId)
    {
        logger.LogDebug("Fetching GraphVersion document for product {Product}, version {VersionId}", 
            product, versionId);
        return GetDocumentAsync<GraphVersionDocument>($"{product}/v2/documentation/{versionId}");
    }

    public Task<VertexDocument> GetVertexAsync(ProductDefinition product, string versionId, string vertexId)
    {
        logger.LogDebug("Fetching Vertex document for product {Product}, version {VersionId}, vertex {VertexId}", 
            product, versionId, vertexId);
        return GetDocumentAsync<VertexDocument>($"{product}/v2/documentation/{versionId}/vertices/{vertexId}");
    }

    private async Task<T> GetDocumentAsync<T>(string requestUri) where T : JsonApiDocument
    {
        logger.LogTrace("Sending GET request to {RequestUri}", requestUri);
        HttpResponseMessage response = await httpClient.GetAsync(requestUri);

        T document = await response.Content.ReadFromJsonAsync<T>(jsonSerializerOptions)
          ?? throw new JsonException("Failed to deserialize response: deserialized content was null");
        return document;
    }
}
