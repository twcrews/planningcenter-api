namespace Cos.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a patchable resource.
/// </summary>
/// <typeparam name="T">The resource type.</typeparam>
public interface IPatchableResource<T>
{
	/// <summary>
	/// Patches a resource to the remote source.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<T?> PatchAsync(T content);
}
