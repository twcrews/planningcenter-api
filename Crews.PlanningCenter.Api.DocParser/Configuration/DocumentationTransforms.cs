namespace Crews.PlanningCenter.Api.DocParser.Configuration;

public record DocumentationTransforms
{
    public IEnumerable<ExcludedVertexEntry> ExcludedVertices { get; init; } = [];
    public IEnumerable<EdgeTypeOverrideEntry> EdgeTypeOverrides { get; init; } = [];
    public IEnumerable<CollectionOverrideEntry> CollectionOverrides { get; init; } = [];
    public IEnumerable<VertexNameOverrideEntry> VertexNameOverrides { get; init; } = [];
    public IEnumerable<AttributeTypeOverrideEntry> AttributeTypeOverrides { get; init; } = [];
    public IEnumerable<AttributeJsonConverterEntry> AttributeJsonConverters { get; init; } = [];
    public IEnumerable<RelationshipTypeOverrideEntry> RelationshipTypeOverrides { get; init; } = [];

    public record ExcludedVertexEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public required string Vertex { get; init; }
        public bool ShouldGenerateResource { get; init; }
        public bool ShouldGenerateClients { get; init; }
    }

    public record EdgeTypeOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Vertex { get; init; }
        public required string Edge { get; init; }
        public required string ClrType { get; init; }
    }

    public record CollectionOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Vertex { get; init; }
        public required string Edge { get; init; }
        public bool IsCollection { get; init; }
    }

    public record VertexNameOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public required string Vertex { get; init; }
        public required string ClrModelName { get; init; }
        public required string ClrResourceName { get; init; }
    }

    public record AttributeTypeOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Vertex { get; init; }
        public required string Attribute { get; init; }
        public required string ClrType { get; init; }
    }

    public record AttributeJsonConverterEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Vertex { get; init; }
        public required string Attribute { get; init; }
        public required string Converter { get; init; }
    }

    public record RelationshipTypeOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Vertex { get; init; }
        public required string Relationship { get; init; }
        public required string ClrType { get; init; }
    }
}
