using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Permissions
{
    [JsonPropertyName("can_create")]
    public bool CanCreate { get; init; }

    [JsonPropertyName("can_update")]
    public bool CanUpdate { get; init; }

    [JsonPropertyName("can_destroy")]
    public bool CanDestroy { get; init; }

    [JsonPropertyName("read")]
    public JsonElement? Read { get; init; }

    [JsonPropertyName("create")]
    public JsonElement? Create { get; init; }

    [JsonPropertyName("update")]
    public JsonElement? Update { get; init; }

    [JsonPropertyName("destroy")]
    public JsonElement? Destroy { get; init; }

    [JsonPropertyName("edges")]
    public IEnumerable<string> Edges { get; init; } = [];
}

public record PermissionsResource : JsonApiResource
{
    [JsonPropertyName("attributes")]
    public required new Permissions Attributes { get; init; }
}

public record PermissionsRelationship : JsonApiRelationship
{
    [JsonPropertyName("data")]
    public required new PermissionsResource Data { get; init; }
}