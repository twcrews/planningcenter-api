namespace Cos.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a postable resource.
/// </summary>
/// <typeparam name="T">The resource type.</typeparam>
public interface IPostableResource<T>
{
	/// <summary>
	/// Posts a resource to the remote source.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<T?> PostAsync(T content);
}
