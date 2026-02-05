using Crews.Web.JsonApiClient;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public class Version
{
  [JsonPropertyName("beta")]
  public bool Beta { get; set; }

  [JsonPropertyName("details")]
  public string? Details { get; set; }
}

public class VersionResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Version Attributes { get; set; }

  [JsonPropertyName("relationships")]
  public required new VersionRelationships Relationships { get; set; }
}

public class VersionRelationships
{
  [JsonPropertyName("entry")]
  public required VertexRelationship Entry { get; set; }

  [JsonPropertyName("vertices")]
  public required VertexCollectionRelationship Vertices { get; set; }
}

public class VersionRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new IEnumerable<VersionResource> Data { get; set; }
}