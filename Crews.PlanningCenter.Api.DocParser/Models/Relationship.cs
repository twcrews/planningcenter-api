using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Relationship
{
  [JsonPropertyName("association")]
  public required string Association { get; init; }

  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("authorization_level")]
  public required string AuthorizationLevel { get; init; }

  [JsonPropertyName("graph_type")]
  public required string GraphType { get; init; }

  [JsonPropertyName("polymorphic")]
  public JsonElement? Polymorphic { get; init; }

  [JsonPropertyName("note")]
  public string? Note { get; init; }
}

public record RelationshipResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Relationship Attributes { get; init; }
}

public record RelationshipCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<RelationshipResource> Data { get; init; }
}

public record RelationshipRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new RelationshipResource Data { get; init; }
}
