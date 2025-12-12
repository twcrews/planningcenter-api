using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models.Incoming;

class DeprecatedEdge
{
  [JsonPropertyName("name")]
  public required string Name { get; set; }

   [JsonPropertyName("deprecated_in")]
  public DateOnly DeprecatedIn { get; set; }

  [JsonPropertyName("removed_in")]
  public DateOnly RemovedIn { get; set; }
}

class DeprecatedEdgeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new DeprecatedEdge Attributes { get; set; }
}

class DeprecatedEdgeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<DeprecatedEdgeResource> Data { get; set; }
}