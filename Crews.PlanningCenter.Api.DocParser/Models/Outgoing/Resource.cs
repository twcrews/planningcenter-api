namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Resource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Deprecated { get; set; }
    public bool CollectionOnly { get; set; }
    public required IEnumerable<ResourceAttribute> Attributes { get; set; }
    public IEnumerable<ResourceRelationship> Relationships { get; set; } = [];
    public IEnumerable<ResourceIncludable> CanInclude { get; set; } = [];
    public IEnumerable<ResourceOrderable> CanOrderBy { get; set; } = [];
    public IEnumerable<ResourceQueryable> CanQueryBy { get; set; } = [];
}

class ResourceAttribute
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }
}

class ResourceRelationship
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string AssociationType { get; set; }
    public string? Note { get; set; }
}

class ResourceIncludable
{
    public required string Parameter { get; set; }
    public required string Value { get; set; }
    public string? Description { get; set; }
    public bool CanAssignOnCreate { get; set; }
    public bool CanAssignOnUpdate { get; set; }
}

class ResourceOrderable
{
    public required string Parameter { get; set; }
    public required string Value { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }
}

class ResourceQueryable
{
    public required string Name { get; set; }
    public required string Parameter { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }
    public string? Example { get; set; }
}