using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Services;

/// <summary>
/// Represents one section of an arrangement.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record ArrangementSection
{
    /// <summary>
    /// The label for the section.
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// The lyrics of the section.
    /// </summary>
    public string? Lyrics { get; init; }

    /// <summary>
    /// Where the section breaks.
    /// </summary>
    public JsonElement BreaksAt { get; init; }
}
