namespace Crews.PlanningCenter.Api.DocParser.Configuration;

public class AppSettings
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
        public IEnumerable<string> ExcludedProducts { get; set; } = [];
        public IEnumerable<ExcludedVersionEntry> ExcludedVersions { get; set; } = [];
        public IEnumerable<ExcludedVertexEntry> ExcludedVertices { get; set; } = [];

        public class ExcludedVersionEntry
        {
            public string? Product { get; set; }
            public string Version { get; set; } = null!;
        }

        public class ExcludedVertexEntry
        {
            public string? Product { get; set; }
            public string? Version { get; set; }
            public string Vertex { get; set; } = null!;
        }
    }
}
