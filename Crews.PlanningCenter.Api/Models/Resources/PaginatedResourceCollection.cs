﻿namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// A collection of resources along with their pagination metadata.
/// </summary>
/// <typeparam name="TResource">The represented resource type.</typeparam>
public class PaginatedResourceCollection<TResource>
{
	/// <summary>
	/// The total number of resources available from the remote source.
	/// </summary>
	public int TotalCount { get; init; }

	/// <summary>
	/// The offset index of the next page of resources.
	/// </summary>
	public int NextPageOffset { get; init; }

	/// <summary>
	/// The offset index of the previous page of resources.
	/// </summary>
	/// <value></value>
	public int PreviousPageOffset { get; init; }

	/// <summary>
	/// The collection of resources retrieved from the paginated collection.
	/// </summary>
	public required IEnumerable<TResource> Resources { get; init; }
}
