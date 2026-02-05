using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public class GraphVersion : Graph 
{
  [JsonPropertyName("beta")]
  public bool Beta { get; set; }

  [JsonPropertyName("details")]
  public string? Details { get; set; }
}

public class GraphVersionDocument : JsonApiDocument
{
  [JsonPropertyName("data")]
  public required new GraphVersionResource Data { get; set; }
}

public class GraphVersionResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new GraphVersion Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required new GraphVersionRelationships Relationships { get; set; }
}

public class GraphVersionRelationships
{
  [JsonPropertyName("previous_version")]
  public required JsonApiRelationship PreviousVersion { get; set; }

  [JsonPropertyName("next_version")]
  public required JsonApiRelationship NextVersion { get; set; }

  [JsonPropertyName("changes")]
  public required VertexChangeCollectionRelationship Changes { get; set; }

  [JsonPropertyName("removed")]
  public required DeprecatedEdgeCollectionRelationship Removed { get; set; }

  [JsonPropertyName("vertices")]
  public required VertexCollectionRelationship Vertices { get; set; }

  [JsonPropertyName("edges")]
  public required EdgeCollectionRelationship Edges { get; set; }

  [JsonPropertyName("entry")]
  public required VertexRelationship Entry { get; set; }
}