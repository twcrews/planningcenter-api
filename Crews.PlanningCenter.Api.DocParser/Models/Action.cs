using Crews.Web.JsonApiClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser.Models;

public record Action
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("path")]
    public required Uri Path { get; init; }

    [JsonPropertyName("can_run")]
    public string? CanRun { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("details")]
    public string? Details { get; init; }

    [JsonPropertyName("return_type")]
    public JsonElement? ReturnType { get; init; }

    [JsonPropertyName("example_body")]
    public JsonElement ExampleBody { get; init; }

    [JsonPropertyName("deprecated")]
    public bool Deprecated { get; init; }
}

public record ActionRelationships
{
    [JsonPropertyName("tail")]
    public VertexRelationship? Tail { get; init; }

    [JsonPropertyName("rate_limits")]
    public JsonApiRelationship? RateLimits { get; init; }
}

public record ActionResource : JsonApiResource
{
    [JsonPropertyName("attributes")]
    public required new Action Attributes { get; init; }

    [JsonPropertyName("relationships")]
    public new ActionRelationships? Relationships { get; init; }
}

public record ActionRelationship : JsonApiRelationship
{
    [JsonPropertyName("data")]
    public new IEnumerable<ActionResource> Data { get; init; } = [];
}