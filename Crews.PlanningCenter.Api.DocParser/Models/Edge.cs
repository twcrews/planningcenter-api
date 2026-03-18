using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Edge
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("details")]
  public string? Details { get; init; }

  [JsonPropertyName("path")]
  public required string Path { get; init; }

  [JsonPropertyName("filters")]
  public IEnumerable<string> Filters { get; init; } = [];

  [JsonPropertyName("scopes")]
  public IEnumerable<Scope> Scopes { get; init; } = [];

  [JsonPropertyName("deprecated")]
  public bool Deprecated { get; init; }
}

public record EdgeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Edge Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public required new EdgeRelationships Relationships { get; init; }
}

public record EdgeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<EdgeResource> Data { get; init; }
}

public record EdgeRelationships
{
  [JsonPropertyName("head")]
  public required VertexRelationship Head { get; init; }

  [JsonPropertyName("tail")]
  public required VertexRelationship Tail { get; init; }

  [JsonPropertyName("rate_limits")]
  public required JsonApiRelationship RateLimits { get; init; }
}

public record Scope
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }
  [JsonPropertyName("scope_help")]
  public string? ScopeHelp { get; init; }
}