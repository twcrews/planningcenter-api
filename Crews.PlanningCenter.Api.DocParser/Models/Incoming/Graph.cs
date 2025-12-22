using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models.Incoming;

class Graph
{
  [JsonPropertyName("title")]
  public string? Title { get; set; }

  [JsonPropertyName("description")]
  public string? Description { get; set; }
}

class GraphDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new GraphResource Data { get; set; }
}

class GraphResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Graph Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required new GraphRelationships Relationships { get; set; }
}

class GraphRelationships
{
  [JsonPropertyName("versions")]
  public required VersionRelationship Versions { get; set; }
}