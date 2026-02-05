using System.Text.Json.Serialization;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.People.V2019_10_10;

/// <summary>
/// Wrapper for the BirthdayPeople resource.
/// </summary>
public class BirthdayPeopleResource : JsonApiResource
{
    /// <summary>
    /// The attributes of the BirthdayPeople resource.
    /// </summary>
    [JsonPropertyName("attributes")]
    public required new BirthdayPeople Attributes { get; set; }
}

/// <summary>
/// Represents a collection of people with birthdays.
/// </summary>
public class BirthdayPeople
{
    /// <summary>
    /// The list of people with birthdays.
    /// </summary>
    [JsonPropertyName("people")]
    public IEnumerable<BirthdayPerson> People { get; set; } = [];
}

/// <summary>
/// Represents the details of a person's birth date.
/// </summary>
public class BirthdayPerson
{
    /// <summary>
    /// The unique identifier of the person.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The name of the person.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Indicates whether the person is a child.
    /// </summary>
    [JsonPropertyName("child")]
    public bool Child { get; set; }

    /// <summary>
    /// The birthdate of the person.
    /// </summary>
    [JsonPropertyName("birthdate")]
    public DateOnly Birthdate { get; set; }

    /// <summary>
    /// The URL of the person's avatar image.
    /// </summary>
    [JsonPropertyName("avatar")]
    public required string Avatar { get; set; }
}