namespace Crews.PlanningCenter.Api.DocParser.Configuration;

class AppSettings
{
    public string OutputDirectory { get; set; } = "output";
    public PlanningCenterClientOptions? PlanningCenterClient { get; set; }
    public DocumentationBuilderOptions? DocumentationBuilder { get; set; }

    public class PlanningCenterClientOptions
    {
        public string? BaseAddress { get; set; }
    }

    public class DocumentationBuilderOptions
    {
        public int ConcurrentConnections { get; set; }
    }
}
