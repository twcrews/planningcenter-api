using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record UrlParameter
{
  [JsonPropertyName("can_assign_on_create")]
  public bool? CanAssignOnCreate { get; init; }

  [JsonPropertyName("can_assign_on_update")]
  public bool? CanAssignOnUpdate { get; init; }

  [JsonPropertyName("Minimum")]
  public int? Minimum { get; init; }

  [JsonPropertyName("Maximum")]
  public int? Maximum { get; init; }

  [JsonPropertyName("default")]
  public int? Default { get; init; }

  [JsonPropertyName("example")]
  public string? Example { get; init; }

  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("parameter")]
  public required string Parameter { get; init; }

  [JsonPropertyName("type")]
  public required string Type { get; init; }

  [JsonPropertyName("value")]
  public string? Value { get; init; }

  [JsonPropertyName("description")]
  public string? Description { get; init; }
}

public record UrlParameterResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new UrlParameter Attributes { get; init; }
}

public record UrlParameterCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<UrlParameterResource> Data { get; init; }
}

public record UrlParameterRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new UrlParameterResource Data { get; init; }
}