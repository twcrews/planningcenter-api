using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Vertex
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("description")]
  public string? Description { get; init; }

  [JsonPropertyName("example")]
  public string? Example { get; init; }

  [JsonPropertyName("path")]
  public string? Path { get; init; }

  [JsonPropertyName("collection_only")]
  public bool CollectionOnly { get; init; }

  [JsonPropertyName("deprecated")]
  public bool Deprecated { get; init; }
}

public record VertexDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new VertexResource Data { get; init; }
}

public record VertexResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public new Vertex? Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public new VertexRelationships? Relationships { get; init; }
}

public record VertexRelationships
{
  [JsonPropertyName("attributes")]
  public required AttributeCollectionRelationship Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public required RelationshipCollectionRelationship Relationships { get; init; }

  [JsonPropertyName("permissions")]
  public required PermissionsRelationship Permissions { get; init; }

  [JsonPropertyName("actions")]
  public required ActionRelationship Actions { get; init; }

  [JsonPropertyName("outbound_edges")]
  public required EdgeCollectionRelationship OutboundEdges { get; init; }

  [JsonPropertyName("inbound_edges")]
  public required EdgeCollectionRelationship InboundEdges { get; init; }

  [JsonPropertyName("can_include")]
  public required UrlParameterCollectionRelationship CanInclude { get; init; }

  [JsonPropertyName("can_order")]
  public required UrlParameterCollectionRelationship CanOrder { get; init; }

  [JsonPropertyName("can_query")]
  public required UrlParameterCollectionRelationship CanQuery { get; init; }

  [JsonPropertyName("per_page")]
  public required UrlParameterRelationship PerPage { get; init; }

  [JsonPropertyName("offset")]
  public required UrlParameterRelationship Offset { get; init; }

  [JsonPropertyName("rate_limits")]
  public required JsonApiRelationship RateLimits { get; init; }
}

public record VertexRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new VertexResource Data { get; init; }
}

public record VertexCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<VertexResource> Data { get; init; }
}