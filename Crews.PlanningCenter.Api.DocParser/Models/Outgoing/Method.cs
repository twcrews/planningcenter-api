namespace Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

class Method
{
    public required string Name { get; set; }
    public required string Signature { get; set; }
    public required string PathMutation { get; set; }
}
