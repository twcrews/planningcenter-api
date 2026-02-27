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
        public IEnumerable<ExcludedVertexEntry> ExcludedVertices { get; set; } = [];
        public IEnumerable<EdgeTypeOverrideEntry> EdgeTypeOverrides { get; set; } = [];
        public IEnumerable<CollectionOverrideEntry> CollectionOverrides { get; set; } = [];
        public IEnumerable<NameOverrideEntry> NameOverrides { get; set; } = [];
        public IEnumerable<AttributeTypeOverrideEntry> AttributeTypeOverrides { get; set; } = [];

        public class ExcludedVertexEntry
        {
            public string? Product { get; set; }
            public string? Version { get; set; }
            public string Vertex { get; set; } = null!;
            public bool GenerateResource { get; set; }
            public bool GenerateClients { get; set; }
        }

        public class EdgeTypeOverrideEntry
        {
            public string? Product { get; set; }
            public string? Version { get; set; }
            public string? Vertex { get; set; }
            public required string Edge { get; set; }
            public required string Type { get; set; }
        }

        public class CollectionOverrideEntry
        {
            public string? Product { get; set; }
            public string? Version { get; set; }
            public string? Vertex { get; set; }
            public required string Edge { get; set; }
            public bool IsCollection { get; set; }
        }

        public class NameOverrideEntry
        {
            public string? Product { get; set; }
            public string? Version { get; set; }
            public string? Vertex { get; set; }
            public required string ModelName { get; set; }
            public required string ResourceName { get; set; }
        }

        public class AttributeTypeOverrideEntry
        {
            public string? Product { get; set; }
            public string? Version { get; set; }
            public string? Vertex { get; set; }
            public required string Attribute { get; set; }
            public required string Type { get; set; }
        }
    }
}
