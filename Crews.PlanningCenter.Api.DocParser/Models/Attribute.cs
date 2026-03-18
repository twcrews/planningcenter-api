using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Attribute
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("type_annotation")]
  public required TypeAnnotation TypeAnnotation { get; init; }

  [JsonPropertyName("note")]
  public string? Note { get; init; }

  [JsonPropertyName("permission_level")]
  public required string PermissionLevel { get; init; }

  [JsonPropertyName("description")]
  public string? Description { get; init; }
}

public record AttributeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Attribute Attributes { get; init; }
}

public record AttributeRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new AttributeResource Data { get; init; }
}

public record AttributeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<AttributeResource> Data { get; init; }
}
public record TypeAnnotation
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("example")]
  public JsonElement? Example { get; init; }
}