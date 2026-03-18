using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Crews.PlanningCenter.Api.Models;

[ExcludeFromCodeCoverage]
public class Product
{
    public ProductDefinition Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public IEnumerable<Version> Versions { get; set; } = [];
}
