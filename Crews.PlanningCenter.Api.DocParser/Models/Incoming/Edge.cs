using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models.Incoming;

class Edge
{
  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("details")]
  public string? Details { get; set; }

  [JsonPropertyName("path")]
  public required string Path { get; set; }

  [JsonPropertyName("filters")]
  public IEnumerable<string> Filters { get; set; } = [];

  [JsonPropertyName("scopes")]
  public IEnumerable<Scope> Scopes { get; set; } = [];

  [JsonPropertyName("deprecated")]
  public bool Deprecated { get; set; }
}

class EdgeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Edge Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required new EdgeRelationships Relationships { get; set; }
}

class EdgeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<EdgeResource> Data { get; set; }
}

class EdgeRelationships
{
  [JsonPropertyName("head")]
  public required VertexRelationship Head { get; set; }

  [JsonPropertyName("tail")]
  public required VertexRelationship Tail { get; set; }

  [JsonPropertyName("rate_limits")]
  public required JsonApiRelationship RateLimits { get; set; }
}

class Scope
{
  [JsonPropertyName("name")]
  public required string Name { get; set; }
  [JsonPropertyName("scope_help")]
  public string? ScopeHelp { get; set; }
}