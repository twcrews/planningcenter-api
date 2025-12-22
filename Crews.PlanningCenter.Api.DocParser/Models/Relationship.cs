using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

class Relationship
{
  [JsonPropertyName("association")]
  public required string Association { get; set; }

  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("authorization_level")]
  public required string AuthorizationLevel { get; set; }

  [JsonPropertyName("graph_type")]
  public required string GraphType { get; set; }

  [JsonPropertyName("polymorphic")]
  public JsonElement? Polymorphic { get; set; }

  [JsonPropertyName("note")]
  public string? Note { get; set; }
}

class RelationshipResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Relationship Attributes { get; set; }
}

class RelationshipCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<RelationshipResource> Data { get; set; }
}

class RelationshipRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new RelationshipResource Data { get; set; }
}
