using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public class VertexChange { }

public class VertexChangeResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new VertexChange Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required new VertexChangeRelationships Relationships { get; set; }
}

public class VertexChangeRelationships
{
  [JsonPropertyName("changes")]
  public required ChangeRelationship Changes { get; set; }

  public class Change
  {
    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
  }

  // The Planning Center API documentation breaks the JSON:API specification here by omitting the 'type' property
  // from this resource linkage, therefore we cannot inherit from 'JsonApiResource' in this case.
  public class ChangeResource
  {
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("attributes")]
    public required Change Attributes { get; set; }
  }

  public class ChangeRelationship : JsonApiRelationship
  {
    [JsonPropertyName("data")]
    public required new IEnumerable<ChangeResource> Data { get; set; }
  }
}

public class VertexChangeCollectionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<VertexChangeResource> Data { get; set; }
}