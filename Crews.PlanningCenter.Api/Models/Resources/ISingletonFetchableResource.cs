namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a single fetchable remote resource.
/// </summary>
/// <typeparam name="TResource">The type of resource to be fetched.</typeparam>
public interface ISingletonFetchableResource<TResource>
{
	/// <summary>
	/// Retrieves the resource.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<JsonApiSingletonResponse<TResource>> GetAsync();
}
