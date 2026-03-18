using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Common;

/// <summary>
/// Defines an undocumented type for images in Check-ins resources.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record Image : ImageUrl
{
    /// <summary>
    /// The image thumbnail.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("thumb")]
    public ImageUrl? Thumb { get; init; }
}

/// <summary>
/// Defines an undocumented container type for image URLs used in Check-ins resources.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record ImageUrl
{
    /// <summary>
    /// The URL of the image.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("url")]
    public Uri? Url { get; init; }
}