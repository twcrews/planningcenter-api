namespace Crews.PlanningCenter.Api.Generators.Models;

public record PlanningCenterResource
{
	public required Type EntityClrType { get; init; }
	public IEnumerable<PlanningCenterResourceVertex> OutboundVertices { get; init; } = [];
	public bool CanCreate { get; init; }
	public bool CanUpdate { get; init; }
	public bool CanDestroy { get; init; }
}
