using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Models;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Publishing;

/// <summary>
/// Wrapper for the Person resource.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record PersonResource : JsonApiResource<Person> { }

/// <summary>
/// Represents a Person.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record Person
{
    /// <summary>
    /// Planning Center does not provide a description for this attribute.
    /// </summary>
    [JsonPropertyName("acknowledged_editor")]
    public bool? AcknowledgedEditor { get; init; }

    /// <summary>
    /// Planning Center does not provide a description for this attribute.
    /// </summary>
    [JsonPropertyName("permissions")]
    public string? Permissions { get; init; }
}

/// <summary>
/// Response wrapper for a Person resource.
/// </summary>
public class PersonResponse : ResourceResponse<PersonResource> {}