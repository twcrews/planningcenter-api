using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record DeprecatedEdge
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }

   [JsonPropertyName("deprecated_in")]
  public DateOnly DeprecatedIn { get; init; }

  [JsonPropertyName("removed_in")]
  public DateOnly RemovedIn { get; init; }
}

public record DeprecatedEdgeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new DeprecatedEdge Attributes { get; init; }
}

public record DeprecatedEdgeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<DeprecatedEdgeResource> Data { get; init; }
}