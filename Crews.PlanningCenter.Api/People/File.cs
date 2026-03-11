using System.Diagnostics.CodeAnalysis;

namespace Crews.PlanningCenter.Api.People;

/// <summary>
/// Represents a file, such as a profile picture or document.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record File
{
    /// <summary>
    /// The URL where the file can be accessed.
    /// </summary>
    public Uri? Url { get; init; }
}
