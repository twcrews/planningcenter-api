using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record VertexChange { }

public record VertexChangeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new VertexChange Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public required new VertexChangeRelationships Relationships { get; init; }
}

public record VertexChangeRelationships
{
  [JsonPropertyName("changes")]
  public required ChangeRelationship Changes { get; init; }

  public record Change
  {
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }
  }

  // The Planning Center API documentation breaks the JSON:API specification here by omitting the 'type' property
  // from this resource linkage, therefore we cannot inherit from 'JsonApiResource' in this case.
  public record ChangeResource
  {
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("attributes")]
    public required Change Attributes { get; init; }
  }

  public record ChangeRelationship : JsonApiRelationship
  {
    [JsonPropertyName("data")]
    public required new IEnumerable<ChangeResource> Data { get; init; }
  }
}

public record VertexChangeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<VertexChangeResource> Data { get; init; }
}