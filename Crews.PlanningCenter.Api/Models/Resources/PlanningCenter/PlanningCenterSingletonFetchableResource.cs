namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <inheritdoc />
public abstract class PlanningCenterSingletonFetchableResource<T>(Uri uri)
    : PlanningCenterFetchableResource(uri), ISingletonFetchableResource<T>
{
	/// <inheritdoc />
	public abstract Task<T> GetAsync();
}
