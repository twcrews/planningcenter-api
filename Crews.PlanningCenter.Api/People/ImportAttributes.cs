using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace Crews.PlanningCenter.Api.People;

/// <summary>
/// Nested attributes for the People Import resource.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record ImportAttributes
{
    /// <summary>
    /// Planning Center does not provide a description for this attribute.
    /// </summary>
    public bool? DeleteEmpty { get; init; }

    /// <summary>
    /// Planning Center does not provide a description for this attribute.
    /// </summary>
    public JsonObject? Mappings { get; init; }

    /// <summary>
    /// Planning Center does not provide a description for this attribute.
    /// </summary>
    public JsonObject? Source { get; init; } 
}
