namespace Crews.PlanningCenter.Api.Generators.Models;

public record PlanningCenterResource
{
	public required Type EntityClrType { get; init; }
	public IEnumerable<PlanningCenterResourceVertex> OutboundVertices { get; init; } = [];
}
