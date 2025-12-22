using System.Collections.Generic;

namespace Crews.PlanningCenter.Api.Models;

public class Version
{
    public string Id { get; set; } = null!;
    public bool Beta { get; set; }
    public string? Details { get; set; }
    public IEnumerable<Resource> Resources { get; set; } = [];
}
