namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <inheritdoc />
public abstract class PlanningCenterPaginatedFetchableResource<T>(Uri uri)
	: PlanningCenterFetchableResource(uri), IPaginatedFetchableResource<T>
{
	/// <inheritdoc />
	public abstract Task<PaginatedResourceCollection<T>> GetAllAsync();

	/// <inheritdoc />
	public abstract Task<PaginatedResourceCollection<T>> GetAllAsync(int count);

	/// <inheritdoc />
	public abstract Task<PaginatedResourceCollection<T>> GetAllAsync(int count, int offset);
	
	/// <summary>
	/// Retrieves a singleton fetchable resource with the given ID.
	/// </summary>
	/// <param name="id">The ID of the resource.</param>
	/// <returns>A singleton fetchable resource.</returns>
	public abstract PlanningCenterSingletonFetchableResource<T> WithID(string id);
	
	/// <summary>
	/// Adds parameters representing a query expression to the request.
	/// </summary>
	/// <param name="buildQuery">An action used to define property values by which to query.</param>
	/// <returns>An instance of the request with the added items.</returns>
	public abstract PlanningCenterPaginatedFetchableResource<T> Query(Action<T> buildQuery);
	
	/// <summary>
	/// Adds a string to the request representing an attribute by which to order the response results. Should be wrapped
	/// by <c>IOrderable.OrderBy()</c> in the derived type.
	/// </summary>
	/// <param name="orderingAttribute">Attribute to order by.</param>
	/// <returns>An instance of the request with the added item.</returns>
	protected PlanningCenterPaginatedFetchableResource<T> OrderBy(string orderingAttribute)
		=> (AddParameters("order", orderingAttribute) as PlanningCenterPaginatedFetchableResource<T>)!;
	
	/// <summary>
	/// Adds an array of strings representing filterable attributes to the request. Should be wrapped by 
	/// <c>IFilterable.FilterBy()</c> in the derived type.
	/// </summary>
	/// <param name="filters">Attributes to be filtered by.</param>
	protected PlanningCenterPaginatedFetchableResource<T> FilterBy(params string[] filters) 
		=> (AddParameters("filter", filters) as PlanningCenterPaginatedFetchableResource<T>)!;
}