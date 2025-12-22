using System.Collections.Generic;

namespace Crews.PlanningCenter.Api.Models;

public class Product
{
    public ProductDefinition Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public IEnumerable<Version> Versions { get; set; } = [];
}
