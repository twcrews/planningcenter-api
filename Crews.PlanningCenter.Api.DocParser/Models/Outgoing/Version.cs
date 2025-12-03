namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Version
{
    public required string Name { get; set; }
    public required IEnumerable<Resource> Resources { get; set; }
}
