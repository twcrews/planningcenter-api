using System.Text.Json.Serialization;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.People.V2025_07_17;

/// <summary>
/// Resource wrapper for OrganizationStatistics.
/// </summary>
public record OrganizationStatisticsResource : JsonApiResource
{
    /// <summary>
    /// Attributes for the OrganizationStatistics resource.
    /// </summary>
    [JsonPropertyName("attributes")]
    public required new OrganizationStatistics Attributes { get; init; }
}

/// <summary>
/// Attributes for the OrganizationStatistics resource.
/// </summary>
public record OrganizationStatistics
{
    /// <summary>
    /// Membership counts by type.
    /// </summary>
    [JsonPropertyName("membership")]
    public IEnumerable<MembershipCount> Membership { get; init; } = [];

    /// <summary>
    /// People counts by gender.
    /// </summary>
    [JsonPropertyName("gender")]
    public required GenderCounts Gender { get; init; }

    /// <summary>
    /// People counts by age range, each then divided by gender.
    /// </summary>
    [JsonPropertyName("age")]
    public required AgeCounts Age { get; init; }

    /// <summary>
    /// People counts by campus.
    /// </summary>
    [JsonPropertyName("campuses")]
    public IEnumerable<CampusCount> Campuses { get; init; } = [];

    /// <summary>
    /// Total number of people in the organization.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>
    /// Number of people created in the last 30 days.
    /// </summary>
    [JsonPropertyName("created_last_30_days")]
    public int CreatedLast30Days { get; init; }

    /// <summary>
    /// Planning Center does not provide a description for this attribute.
    /// </summary>
    [JsonPropertyName("elasticsearch")]
    public bool ElasticSearch { get; init; }

    /// <summary>
    /// Unique identifiers for each gender reused across the organization.
    /// </summary>
    [JsonPropertyName("gender_ids")]
    public required GenderIds GenderIds { get; init; }
}

/// <summary>
/// Represents membership count for a specific membership type.
/// </summary>
public record MembershipCount
{
    /// <summary>
    /// The unique identifier of the membership type.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The name of the membership type.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The count of people with this membership type.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; init; }
}

/// <summary>
/// Represents people counts by age ranges, further divided by gender.
/// </summary>
public record AgeCounts
{
    /// <summary>
    /// Age range 0-3 years.
    /// </summary>
    [JsonPropertyName("0-3")]
    public required GenderCounts Age0To3 { get; init; }

    /// <summary>
    /// Age range 4-11 years.
    /// </summary>
    [JsonPropertyName("4-11")]
    public required GenderCounts Age4To11 { get; init; }

    /// <summary>
    /// Age range 12-18 years.
    /// </summary>
    [JsonPropertyName("12-18")]
    public required GenderCounts Age12To18 { get; init; }

    /// <summary>
    /// Age range 19-25 years.
    /// </summary>
    [JsonPropertyName("19-25")]
    public required GenderCounts Age19To25 { get; init; }

    /// <summary>
    /// Age range 26-35 years.
    /// </summary>
    [JsonPropertyName("26-35")]
    public required GenderCounts Age26To35 { get; init; }

    /// <summary>
    /// Age range 36-50 years.
    /// </summary>
    [JsonPropertyName("36-50")]
    public required GenderCounts Age36To50 { get; init; }

    /// <summary>
    /// Age range 51-64 years.
    /// </summary>
    [JsonPropertyName("51-64")]
    public required GenderCounts Age51To64 { get; init; }

    /// <summary>
    /// Age range 65+ years.
    /// </summary>
    [JsonPropertyName("65+")]
    public required GenderCounts Age65Plus { get; init; }
}

/// <summary>
/// Represents people counts by gender.
/// </summary>
public record GenderCounts
{
    /// <summary>
    /// Count of male individuals.
    /// </summary>
    [JsonPropertyName("male")]
    public int Male { get; init; }

    /// <summary>
    /// Count of female individuals.
    /// </summary>
    [JsonPropertyName("female")]
    public int Female { get; init; }

    /// <summary>
    /// Count of individuals with unassigned gender.
    /// </summary>
    [JsonPropertyName("unassigned")]
    public int Unassigned { get; init; }
}

/// <summary>
/// Represents a campus with its associated people count.
/// </summary>
public record CampusCount
{
    /// <summary>
    /// The unique identifier of the campus.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The name of the campus.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The count of people associated with this campus.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; init; }
}

/// <summary>
/// Represents unique identifiers for each gender in the organization.
/// </summary>
public record GenderIds
{
    /// <summary>
    /// Unique identifier for male gender.
    /// </summary>
    [JsonPropertyName("male")]
    public required string Male { get; init; }

    /// <summary>
    /// Unique identifier for female gender.
    /// </summary>
    [JsonPropertyName("female")]
    public required string Female { get; init; }
}