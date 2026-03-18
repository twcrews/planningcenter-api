using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Crews.PlanningCenter.Api.Models;

[ExcludeFromCodeCoverage]
public class Resource
{
    public string JsonName { get; set; } = null!;
    public string ResourceClrType { get; set; } = null!;
    public string AttributesClrType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Deprecated { get; set; }
    public bool CollectionOnly { get; set; }
    public bool Postable { get; set; }
    public bool Patchable { get; set; }
    public bool Deletable { get; set; }
    public bool ShouldGenerateResource { get; set; }
    public bool ShouldGenerateClients { get; set; }
    public IEnumerable<ResourceAttribute> Attributes { get; set; } = [];
    public IEnumerable<ResourceRelationship> Relationships { get; set; } = [];
    public IEnumerable<ResourceAction> Actions { get; set; } = [];
    public IEnumerable<ResourceChild> Children { get; set; } = [];
    public IEnumerable<ResourceIncludable> CanInclude { get; set; } = [];
    public IEnumerable<ResourceOrderable> CanOrderBy { get; set; } = [];
    public IEnumerable<ResourceQueryable> CanQueryBy { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public class ResourceAttribute
{
    public string JsonName { get; set; } = null!;
    public string ClrName { get; set; } = null!;
    public string ClrType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? JsonConverter { get; set; }
}

[ExcludeFromCodeCoverage]
public class ResourceRelationship
{
    public string JsonName { get; set; } = null!;
    public string ClrName { get; set; } = null!;
    public string AttributesClrType { get; set; } = null!;
    public string ResourceClrType { get; set; } = null!;
    public bool IsCollection { get; set; }
    public string Description { get; set; } = null!;
}

[ExcludeFromCodeCoverage]
public class ResourceAction
{
    public string JsonName { get; set; } = null!;
    public string ClrMethodName { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string? Requirements { get; set; }
    public string Description { get; set; } = null!;
    public string? AdditionalDetails { get; set; }
    public bool Deprecated { get; set; }
}

[ExcludeFromCodeCoverage]
public class ResourceChild
{
    public string JsonName { get; set; } = null!;
    public string ClrName { get; set; } = null!;
    public string AttributesClrType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public bool IsCollection { get; set; }
    public bool Deprecated { get; set; }
    public IEnumerable<ResourceChildFilter> Filters { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public class ResourceChildFilter
{
    public string ClrMethodName { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Description { get; set; } = null!;
}

[ExcludeFromCodeCoverage]
public class ResourceIncludable
{
    public string ClrMethodName { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool CanAssignOnCreate { get; set; }
    public bool CanAssignOnUpdate { get; set; }
}

[ExcludeFromCodeCoverage]
public class ResourceOrderable
{
    public string ClrMethodName { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Description { get; set; } = null!;
}

[ExcludeFromCodeCoverage]
public class ResourceQueryable
{
    public string ClrMethodName { get; set; } = null!;
    public string ClrType { get; set; } = null!;
    public string Parameter { get; set; } = null!;
    public string Description { get; set; } = null!;
}