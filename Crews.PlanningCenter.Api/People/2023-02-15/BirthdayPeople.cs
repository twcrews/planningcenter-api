using System.Text.Json.Serialization;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.People.V2023_02_15;

/// <summary>
/// Wrapper for the BirthdayPeople resource.
/// </summary>
public record BirthdayPeopleResource : JsonApiResource<BirthdayPeople> { }

/// <summary>
/// Represents a collection of people with birthdays.
/// </summary>
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
public record BirthdayPerson
{
    /// <summary>
    /// The unique identifier of the person.
    /// </summary>
    [JsonPropertyName("id")]
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