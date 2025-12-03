using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Incoming;
using Crews.Web.JsonApiClient;
using System.Net.Http.Json;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.DocParser;

class PlanningCenterClient
{
  private readonly HttpClient _httpClient;

  public PlanningCenterClient(HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
  }

  public Task<GraphDocument> GetGraphAsync(ProductDefinition product)
   => GetDocumentAsync<GraphDocument>($"{product}/v2/documentation");

  public Task<GraphVersionDocument> GetGraphVersionAsync(ProductDefinition product, string versionId)
    => GetDocumentAsync<GraphVersionDocument>($"{product}/v2/documentation/{versionId}");

  public Task<VertexDocument> GetVertexAsync(ProductDefinition product, string versionId, string vertexId)
    => GetDocumentAsync<VertexDocument>($"{product}/v2/documentation/{versionId}/vertices/{vertexId}");

  private async Task<T> GetDocumentAsync<T>(string requestUri) where T : JsonApiDocument
  {
    HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
    T document = await response.Content.ReadFromJsonAsync<T>()
      ?? throw new JsonException("Failed to deserialize response: deserialized content was null");
    return document;
  }
}
