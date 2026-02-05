using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public class UrlParameter
{
  [JsonPropertyName("can_assign_on_create")]
  public bool? CanAssignOnCreate { get; set; }

  [JsonPropertyName("can_assign_on_update")]
  public bool? CanAssignOnUpdate { get; set; }

  [JsonPropertyName("Minimum")]
  public int? Minimum { get; set; }

  [JsonPropertyName("Maximum")]
  public int? Maximum { get; set; }

  [JsonPropertyName("default")]
  public int? Default { get; set; }

  [JsonPropertyName("example")]
  public string? Example { get; set; }

  [JsonPropertyName("name")]
  public required string Name { get; set; }

  [JsonPropertyName("parameter")]
  public required string Parameter { get; set; }

  [JsonPropertyName("type")]
  public required string Type { get; set; }

  [JsonPropertyName("value")]
  public string? Value { get; set; }

  [JsonPropertyName("description")]
  public string? Description { get; set; }
}

public class UrlParameterResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new UrlParameter Attributes { get; set; }
}

public class UrlParameterCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<UrlParameterResource> Data { get; set; }
}

public class UrlParameterRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new UrlParameterResource Data { get; set; }
}