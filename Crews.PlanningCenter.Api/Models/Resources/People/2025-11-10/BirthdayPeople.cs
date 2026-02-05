using System;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.People.V2025_11_10;

public class BirthdayPeople
{
    [JsonPropertyName("people")]
    public IEnumerable<BirthdayPerson> People { get; set; }
}

public class BirthdayPerson
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("child")]
    public bool Child { get; set; }
    [JsonPropertyName("birthdate")]
    public DateOnly Birthdate { get; set; }
    [JsonPropertyName("avatar")]
    public required string Avatar { get; set; }
}