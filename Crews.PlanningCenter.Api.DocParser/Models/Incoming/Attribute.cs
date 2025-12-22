using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models.Incoming;

class Attribute
{
  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("type_annotation")]
  public required TypeAnnotation TypeAnnotation { get; set; }

  [JsonPropertyName("note")]
  public string? Note { get; set; }

  [JsonPropertyName("permission_level")]
  public required string PermissionLevel { get; set; }

  [JsonPropertyName("description")]
  public string? Description { get; set; }
}

class AttributeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Attribute Attributes { get; set; }
}

class AttributeRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new AttributeResource Data { get; set; }
}

class AttributeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<AttributeResource> Data { get; set; }
}
class TypeAnnotation
{
  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("example")]
  public JsonElement? Example { get; set; }
}