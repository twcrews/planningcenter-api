using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models.Incoming;

internal class Permissions
{
  [JsonPropertyName("can_create")]
  public bool CanCreate { get; set; }

  [JsonPropertyName("can_update")]
  public bool CanUpdate { get; set; }

  [JsonPropertyName("can_destroy")]
  public bool CanDestroy { get; set; }

  [JsonPropertyName("read")]
  public JsonElement? Read { get; set; }
}

class PermissionsResource : JsonApiResource
{
  [JsonPropertyName("attributes")]
  public required new Permissions Attributes { get; set; }
}

class PermissionsRelationship : JsonApiRelationship
{
  [JsonPropertyName("data")]
  public required new PermissionsResource Data { get; set; }
}