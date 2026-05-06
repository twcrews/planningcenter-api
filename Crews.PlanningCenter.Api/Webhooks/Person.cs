using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Models;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Webhooks;

/// <summary>
/// Wrapper for the Person resource.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record PersonResource : JsonApiResource<Person> { }

/// <summary>
/// Represents a Person resource.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record Person
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Response wrapper for a Person resource.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public class PersonResponse : ResourceResponse<PersonResource> {}