using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record GraphVersion : Graph 
{
  [JsonPropertyName("beta")]
  public bool Beta { get; init; }

  [JsonPropertyName("details")]
  public string? Details { get; init; }
}

public record GraphVersionDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new GraphVersionResource Data { get; init; }
}

public record GraphVersionResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new GraphVersion Attributes { get; init; }

  [JsonPropertyName("relationships")]
  public required new GraphVersionRelationships Relationships { get; init; }
}

public record GraphVersionRelationships
{
  [JsonPropertyName("previous_version")]
  public required JsonApiRelationship PreviousVersion { get; init; }

  [JsonPropertyName("next_version")]
  public required JsonApiRelationship NextVersion { get; init; }

  [JsonPropertyName("changes")]
  public required VertexChangeCollectionRelationship Changes { get; init; }

  [JsonPropertyName("removed")]
  public required DeprecatedEdgeCollectionRelationship Removed { get; init; }

  [JsonPropertyName("vertices")]
  public required VertexCollectionRelationship Vertices { get; init; }

  [JsonPropertyName("edges")]
  public required EdgeCollectionRelationship Edges { get; init; }

  [JsonPropertyName("entry")]
  public required VertexRelationship Entry { get; init; }
}