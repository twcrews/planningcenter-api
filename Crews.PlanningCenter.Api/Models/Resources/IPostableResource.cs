namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a postable resource.
/// </summary>
/// <typeparam name="TResource">The resource type.</typeparam>
public interface IPostableResource<TResource>
{
	/// <summary>
	/// Posts a resource to the remote source.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<TResource?> PostAsync(TResource content);
}
