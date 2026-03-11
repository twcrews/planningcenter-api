using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.DocParser.Configuration;

public record DocumentationTransforms
{
    public IEnumerable<ResourcePropertyOverrideEntry> ResourceOverrides { get; init; } = [];
    public IEnumerable<AttributePropertyOverrideEntry> AttributeOverrides { get; init; } = [];
    public IEnumerable<RelationshipPropertyOverrideEntry> RelationshipOverrides { get; init; } = [];
    public IEnumerable<ResourceChildPropertyOverrideEntry> ResourceChildOverrides { get; init; } = [];
    public IEnumerable<AdditionalResourceChildEntry> AdditionalResourceChildren { get; init; } = [];

    public record ResourcePropertyOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public required string Resource { get; init; }
        public string? JsonName { get; init; }
        public string? ResourceClrType { get; init; }
        public string? AttributesClrType { get; init; }
        public string? Description { get; init; }
        public bool? Deprecated { get; init; }
        public bool? CollectionOnly { get; init; }
        public bool? Postable { get; init; }
        public bool? Patchable { get; init; }
        public bool? Deletable { get; init; }
        public bool? ShouldGenerateResource { get; init; }
        public bool? ShouldGenerateClients { get; init; }
    }

    public record AttributePropertyOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Resource { get; init; }
        public required string Attribute { get; init; }
        public string? JsonName { get; init; }
        public string? ClrName { get; init; }
        public string? ClrType { get; init; }
        public string? Description { get; init; }
        public string? JsonConverter { get; init; }
    }

    public record RelationshipPropertyOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Resource { get; init; }
        public required string Relationship { get; init; }
        public string? JsonName { get; init; }
        public string? ClrName { get; init; }
        public string? AttributesClrType { get; init; }
        public string? ResourceClrType { get; init; }
        public string? IsCollection { get; init; }
        public string? Description { get; init; }
    }

    public record ResourceChildPropertyOverrideEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public string? Resource { get; init; }
        public required string Child { get; init; }
        public string? JsonName { get; init; }
        public string? ClrName { get; init; }
        public string? AttributesClrType { get; init; }
        public string? Description { get; init; }
        public string? Slug { get; init; }
        public bool? IsCollection { get; init; }
        public bool? Deprecated { get; init; }
    }

    public record AdditionalResourceChildEntry
    {
        public string? Product { get; init; }
        public string? Version { get; init; }
        public required string Resource { get; init; }
        public required ResourceChild Child { get; init; }
    }
}
