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
    public IEnumerable<ResourceChild> Children { get; set; } = [];
    public IEnumerable<ResourceIncludable> CanInclude { get; set; } = [];
    public IEnumerable<ResourceOrderable> CanOrderBy { get; set; } = [];
    public IEnumerable<ResourceQueryable> CanQueryBy { get; set; } = [];
    public bool Postable { get; set; }
    public bool Patchable { get; set; }
    public bool Deletable { get; set; }
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

public class ResourceChild
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
    public string Slug { get; set; } = null!;
    public IEnumerable<ResourceChildFilter> Filters { get; set; } = [];
    public bool IsCollection { get; set; }
    public bool IsDeprecated { get; set; }
}

public class ResourceChildFilter
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
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