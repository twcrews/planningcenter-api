namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a patchable resource.
/// </summary>
/// <typeparam name="TResource">The resource type.</typeparam>
public interface IPatchableResource<TResource>
{
	/// <summary>
	/// Patches a resource to the remote source.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<TResource?> PatchAsync(TResource content);
}
