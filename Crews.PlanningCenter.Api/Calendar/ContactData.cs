using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Calendar;

/// <summary>
/// Represents the contact data for a Person resource.
/// </summary>
public record ContactData
{
    /// <summary>
    /// The contact's email addresses.
    /// </summary>
    [JsonPropertyName("email_addresses")]
    public IEnumerable<EmailAddress> EmailAddresses { get; init; } = [];

    /// <summary>
    /// The contact's physical addresses.
    /// </summary>
    [JsonPropertyName("addresses")]
    public IEnumerable<Address> Addresses { get; init; } = [];

    /// <summary>
    /// The contact's phone numbers.
    /// </summary>
    [JsonPropertyName("phone_numbers")]
    public IEnumerable<PhoneNumber> PhoneNumbers { get; init; } = [];
}

/// <summary>
/// Represents an email address for a Person resource's contact data.
/// </summary>
public record EmailAddress
{
    /// <summary>
    /// The email address.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>
    /// The location or context of the email address (e.g. "home", "work").
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>
    /// Whether this email address is the primary contact method for the person.
    /// </summary>
    [JsonPropertyName("primary")]
    public bool? Primary { get; init; }

    /// <summary>
    /// Whether this email address is blocked.
    /// </summary>
    [JsonPropertyName("blocked")]   
    public bool? Blocked { get; init; }
}

/// <summary>
/// Represents a physical address for a Person resource's contact data.
/// </summary>
public record Address
{
    /// <summary>
    /// The full street address.
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }

    /// <summary>
    /// The first line of the street address.
    /// </summary>
    [JsonPropertyName("street_line_1")]
    public string? StreetLine1 { get; init; }

    /// <summary>
    /// The second line of the street address (e.g. apartment or suite number).
    /// </summary>
    [JsonPropertyName("street_line_2")]
    public string? StreetLine2 { get; init; }

    /// <summary>
    /// The city of the address.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>
    /// The state or province of the address.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>
    /// The postal code of the address.
    /// </summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; init; }

    /// <summary>
    /// The ISO country code of the address (e.g. "US" for United States).
    /// </summary>
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; init; }

    /// <summary>
    /// The location or context of the address (e.g. "home", "work").
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>
    /// Whether this address is the primary contact method for the person.
    /// </summary>
    [JsonPropertyName("primary")]
    public bool? Primary { get; init; }
}

/// <summary>
/// Represents a phone number for a Person resource's contact data.
/// </summary>
public record PhoneNumber
{
    /// <summary>
    /// The phone number.
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; init; }

    /// <summary>
    /// The carrier for a mobile phone number.
    /// </summary>
    [JsonPropertyName("carrier")]
    public string? Carrier { get; init; }

    /// <summary>
    /// The location or context of the phone number (e.g. "home", "work", "mobile").
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>
    /// Whether this phone number is the primary contact method for the person.
    /// </summary>
    [JsonPropertyName("primary")]
    public bool? Primary { get; init; }
}