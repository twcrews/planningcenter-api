namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Product
{
    public required ProductDefinition Name { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required IEnumerable<Version> Versions { get; set; }
}
