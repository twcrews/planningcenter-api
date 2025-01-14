using Humanizer;

namespace Crews.PlanningCenter.Api.Generators.Models;

public record PlanningCenterResourceVertex
{
	public required PlanningCenterResource Resource { get; init; }
	public required string Name { get; init; }

	// FIXME: There doesn't seem to be a reliable way to determine whether a vertex represents a collection via the
	//   documentation API, so we need to check if the name of the vertex is plural.
	public bool IsCollection => Name == Name.Pluralize();
}
