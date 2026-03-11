using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Converters;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.People;

/// <summary>
/// Wrapper for the BirthdayPeople resource.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record BirthdayPeopleResource : JsonApiResource<BirthdayPeople> { }

/// <summary>
/// Represents a collection of people with birthdays.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record BirthdayPeople
{
    /// <summary>
    /// The list of people with birthdays.
    /// </summary>
    [JsonPropertyName("people")]
    public IEnumerable<BirthdayPerson> People { get; init; } = [];
}

/// <summary>
/// Represents the details of a person's birth date.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record BirthdayPerson
{
    /// <summary>
    /// The unique identifier of the person.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(StringFromNumberConverter))]
    public required string Id { get; init; }

    /// <summary>
    /// The name of the person.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Indicates whether the person is a child.
    /// </summary>
    [JsonPropertyName("child")]
    public bool Child { get; init; }

    /// <summary>
    /// The birthdate of the person.
    /// </summary>
    [JsonPropertyName("birthdate")]
    public DateOnly Birthdate { get; init; }

    /// <summary>
    /// The URL of the person's avatar image.
    /// </summary>
    [JsonPropertyName("avatar")]
    public required string Avatar { get; init; }
}