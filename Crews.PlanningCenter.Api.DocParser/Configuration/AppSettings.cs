using System.Diagnostics.CodeAnalysis;

namespace Crews.PlanningCenter.Api.DocParser.Configuration;

[ExcludeFromCodeCoverage(Justification = "This type is a DTO with no logic to test.")]
public record AppSettings
{
    public string OutputDirectory { get; init; } = "output";
    public PlanningCenterClientOptions? PlanningCenterClient { get; init; }
    public DocumentationBuilderOptions? DocumentationBuilder { get; init; }

    public record PlanningCenterClientOptions
    {
        public string? BaseAddress { get; init; }
    }

    public record DocumentationBuilderOptions
    {
        public int ConcurrentConnections { get; init; }
    }
}
