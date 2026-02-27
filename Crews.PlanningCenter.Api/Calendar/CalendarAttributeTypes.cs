using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Calendar;

/// <summary>
/// Defines an undocumented type for images in Calendar resources.
/// </summary>
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
/// Defines an undocumented container type for image URLs used in Calendar resources.
/// </summary>
public record ImageUrl
{
    /// <summary>
    /// The URL of the image.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("url")]
    public Uri? Url { get; init; }
}