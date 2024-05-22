using System.Collections.Immutable;

namespace Cos.PlanningCenter.Api.Models.Resources;

/// <summary>
/// A collection of resources along with their pagination metadata.
/// </summary>
public class PaginatedResourceCollection<T>
{
	/// <summary>
	/// The total number of resources available from the remote source.
	/// </summary>
	public int TotalCount { get; init; }

	/// <summary>
	/// The offset index from which the resource collection was retrieved, relative to the total number of available
	/// resources.
	/// </summary>
	public int Offset { get; init; }

	/// <summary>
	/// The collection of resources retrieved from the paginated collection.
	/// </summary>
	public required ImmutableList<T> Resources { get; init; }
}
