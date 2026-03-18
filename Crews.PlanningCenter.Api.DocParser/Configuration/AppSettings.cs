namespace Crews.PlanningCenter.Api.DocParser.Configuration;

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
