namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Version
{
    public required string Id { get; set; }
    public bool Beta { get; set; }
    public string? Details { get; set; }
    public required IEnumerable<Resource> Resources { get; set; }
}
