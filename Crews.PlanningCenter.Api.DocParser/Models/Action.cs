using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

class Action
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("path")]
    public required Uri Path { get; set; }

    [JsonPropertyName("can_run")]
    public JsonElement? CanRun { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("return_type")]
    public JsonElement? ReturnType { get; set; }

    [JsonPropertyName("example_body")]
    public JsonElement ExampleBody { get; set; }

    [JsonPropertyName("deprecated")]
    public bool Deprecated { get; set; }
}

class ActionRelationships
{
    [JsonPropertyName("tail")]
    public VertexRelationship? Tail { get; set; }

    [JsonPropertyName("rate_limits")]
    public JsonApiRelationship? RateLimits { get; set; }
}

class ActionResource : JsonApiResource
{
    [JsonPropertyName("attributes")]
    public new Action? Attributes { get; set; }

    [JsonPropertyName("relationships")]
    public new ActionRelationships? Relationships { get; set; }
}

class ActionRelationship : JsonApiRelationship
{
    [JsonPropertyName("data")]
    public new IEnumerable<ActionResource> Data { get; set; } = [];
}