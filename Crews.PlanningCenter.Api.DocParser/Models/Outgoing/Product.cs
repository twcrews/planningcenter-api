namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Product
{
    public required string Name { get; set; }
    public required IEnumerable<Version> Versions { get; set; }
}
