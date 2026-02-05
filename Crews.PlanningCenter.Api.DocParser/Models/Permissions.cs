using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public class Permissions
{
    [JsonPropertyName("can_create")]
    public bool CanCreate { get; set; }

    [JsonPropertyName("can_update")]
    public bool CanUpdate { get; set; }

    [JsonPropertyName("can_destroy")]
    public bool CanDestroy { get; set; }

    [JsonPropertyName("read")]
    public JsonElement? Read { get; set; }

    [JsonPropertyName("create")]
    public JsonElement? Create { get; set; }

    [JsonPropertyName("update")]
    public JsonElement? Update { get; set; }

    [JsonPropertyName("destroy")]
    public JsonElement? Destroy { get; set; }

    [JsonPropertyName("edges")]
    public IEnumerable<string> Edges { get; set; } = [];
}

public class PermissionsResource : JsonApiResource
{
    [JsonPropertyName("attributes")]
    public required new Permissions Attributes { get; set; }
}

public class PermissionsRelationship : JsonApiRelationship
{
    [JsonPropertyName("data")]
    public required new PermissionsResource Data { get; set; }
}