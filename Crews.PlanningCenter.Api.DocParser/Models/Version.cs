using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Version
{
  [JsonPropertyName("beta")]
  public bool Beta { get; init; }

  [JsonPropertyName("details")]
  public string? Details { get; init; }
}

public record VersionResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Version Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public required new VersionRelationships Relationships { get; init; }
}

public record VersionRelationships
{
  [JsonPropertyName("entry")]
  public required VertexRelationship Entry { get; init; }

  [JsonPropertyName("vertices")]
  public required VertexCollectionRelationship Vertices { get; init; }
}

public record VersionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<VersionResource> Data { get; init; }
}