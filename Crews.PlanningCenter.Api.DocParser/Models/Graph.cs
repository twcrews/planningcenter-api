using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Graph
{
  [JsonPropertyName("title")]
  public string? Title { get; init; }

  [JsonPropertyName("description")]
  public string? Description { get; init; }
}

public record GraphDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new GraphResource Data { get; init; }
}

public record GraphResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Graph Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public required new GraphRelationships Relationships { get; init; }
}

public record GraphRelationships
{
  [JsonPropertyName("versions")]
  public required VersionRelationship Versions { get; init; }
}