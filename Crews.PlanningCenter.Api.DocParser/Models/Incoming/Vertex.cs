using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models.Incoming;

class Vertex
{
  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("description")]
  public string? Description { get; set; }

  [JsonPropertyName("example")]
  public string? Example { get; set; }

  [JsonPropertyName("path")]
  public string? Path { get; set; }

  [JsonPropertyName("collection_only")]
  public bool CollectionOnly { get; set; }

  [JsonPropertyName("deprecated")]
  public bool Deprecated { get; set; }
}

class VertexDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new VertexResource Data { get; set; }
}

class VertexResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public new Vertex? Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public new VertexRelationships? Relationships { get; set; }
}

class VertexRelationships
{
  [JsonPropertyName("attributes")]
  public required AttributeCollectionRelationship Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required RelationshipCollectionRelationship Relationships { get; set; }

  [JsonPropertyName("permissions")]
  public required PermissionsRelationship Permissions { get; set; }

  [JsonPropertyName("actions")]
  public required JsonApiRelationship Actions { get; set; }

  [JsonPropertyName("outbound_edges")]
  public required EdgeCollectionRelationship OutboundEdges { get; set; }

  [JsonPropertyName("inbound_edges")]
  public required EdgeCollectionRelationship InboundEdges { get; set; }

  [JsonPropertyName("can_include")]
  public required UrlParameterCollectionRelationship CanInclude { get; set; }

  [JsonPropertyName("can_order")]
  public required UrlParameterCollectionRelationship CanOrder { get; set; }

  [JsonPropertyName("can_query")]
  public required UrlParameterCollectionRelationship CanQuery { get; set; }

  [JsonPropertyName("per_page")]
  public required UrlParameterRelationship PerPage { get; set; }

  [JsonPropertyName("offset")]
  public required UrlParameterRelationship Offset { get; set; }

  [JsonPropertyName("rate_limits")]
  public required JsonApiRelationship RateLimits { get; set; }
}

class VertexRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new VertexResource Data { get; set; }
}

class VertexCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<VertexResource> Data { get; set; }
}