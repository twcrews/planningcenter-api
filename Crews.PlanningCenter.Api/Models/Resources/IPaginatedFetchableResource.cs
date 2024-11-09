namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a paginated collection of fetchable resources.
/// </summary>
/// <typeparam name="T">The type of resource to be fetched.</typeparam>
public interface IPaginatedFetchableResource<T>
{
	/// <summary>
	/// Fetches the paginated collection with an undefined page size and offset.
	/// </summary>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<PaginatedResourceCollection<T>> GetAllAsync();

	/// <summary>
	/// Fetches the paginated collection with the given page size and an undefined page offset.
	/// </summary>
	/// <param name="count">
	/// The requested size of the page. The remote provider may enforce an upper limit to this.
	/// </param>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<PaginatedResourceCollection<T>> GetAllAsync(int count);

	/// <summary>
	/// Fetches the paginated collection with the given page size and offset.
	/// </summary>
	/// <param name="count">
	/// The requested size of the page. The remote provider may enforce an upper limit to this.
	/// </param>
	/// <param name="offset">
	/// The requested offset index of the page relative to the total number of available resources.
	/// </param>
	/// <returns>The task object representing the asynchronous operation.</returns>
	Task<PaginatedResourceCollection<T>> GetAllAsync(int count, int offset);
}
