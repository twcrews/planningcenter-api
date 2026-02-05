using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public class Graph
{
  [JsonPropertyName("title")]
  public string? Title { get; set; }

  [JsonPropertyName("description")]
  public string? Description { get; set; }
}

public class GraphDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new GraphResource Data { get; set; }
}

public class GraphResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Graph Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required new GraphRelationships Relationships { get; set; }
}

public class GraphRelationships
{
  [JsonPropertyName("versions")]
  public required VersionRelationship Versions { get; set; }
}