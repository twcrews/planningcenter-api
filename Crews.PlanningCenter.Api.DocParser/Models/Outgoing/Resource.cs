namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Resource
{
    public required string Type { get; set; }
    public required string ApiSingularName { get; set; }
    public required string ApiPluralName { get; set; }
    public IEnumerable<RelatedResource> RelatedResources { get; set; } = [];
    public required IEnumerable<Property> Attributes { get; set; }
    public IEnumerable<Method> Methods { get; set; } = [];
}
