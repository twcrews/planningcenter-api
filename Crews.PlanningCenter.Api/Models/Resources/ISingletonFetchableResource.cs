namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a single fetchable remote resource.
/// </summary>
/// <typeparam name="T">The type of resource to be fetched.</typeparam>
public interface ISingletonFetchableResource<T>
{
	/// <summary>
	/// Retrieves the resource.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<T?> GetAsync();
}
