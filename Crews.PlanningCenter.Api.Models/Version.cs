using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Crews.PlanningCenter.Api.Models;

[ExcludeFromCodeCoverage]
public class Version
{
    public string Id { get; set; } = null!;
    public bool Beta { get; set; }
    public string? Details { get; set; }
    public IEnumerable<Resource> Resources { get; set; } = [];
}
