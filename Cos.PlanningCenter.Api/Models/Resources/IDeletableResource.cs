namespace Cos.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a deletable resource.
/// </summary>
public interface IDeletableResource
{
	/// <summary>
	/// Deletes a resource from the remote source.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task DeleteAsync();
}
