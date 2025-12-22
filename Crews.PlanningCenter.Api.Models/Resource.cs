using System.Collections.Generic;

namespace Crews.PlanningCenter.Api.Models;

public class Resource
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Deprecated { get; set; }
    public bool CollectionOnly { get; set; }
    public IEnumerable<ResourceAttribute> Attributes { get; set; } = [];
    public IEnumerable<ResourceRelationship> Relationships { get; set; } = [];
    public IEnumerable<ResourceIncludable> CanInclude { get; set; } = [];
    public IEnumerable<ResourceOrderable> CanOrderBy { get; set; } = [];
    public IEnumerable<ResourceQueryable> CanQueryBy { get; set; } = [];
}

public class ResourceAttribute
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
}

public class ResourceRelationship
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string AssociationType { get; set; } = null!;
    public string? Note { get; set; }
}

public class ResourceIncludable
{
    public string Parameter { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? Description { get; set; }
    public bool CanAssignOnCreate { get; set; }
    public bool CanAssignOnUpdate { get; set; }
}

public class ResourceOrderable
{
    public string Parameter { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
}

public class ResourceQueryable
{
    public string Name { get; set; } = null!;
    public string Parameter { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
    public string? Example { get; set; }
}