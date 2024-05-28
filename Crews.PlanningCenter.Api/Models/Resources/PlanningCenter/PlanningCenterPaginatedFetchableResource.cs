using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.Services;
using JsonApiFramework;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <inheritdoc />
public abstract class PlanningCenterPaginatedFetchableResource<T, TSelf, TSingleton, TContext>(Uri uri)
	: PlanningCenterFetchableResource<TSelf>(uri), IPaginatedFetchableResource<T>
	where TSelf : PlanningCenterPaginatedFetchableResource<T, TSelf, TSingleton, TContext>
	where TSingleton : PlanningCenterSingletonFetchableResource<T, TSingleton, TContext>
	where TContext : PlanningCenterDocumentContext
	where T : class
{
	/// <inheritdoc />
	public async Task<PaginatedResourceCollection<T>> GetAllAsync()
	{
		PlanningCenterApiClient client = new(new()
		{
			BaseAddress = new("https://api.planningcenteronline.com/")
		});

		HttpResponseMessage response = await client.SendAsync(new()
		{
			RequestUri = Uri
		});
		Document document = JsonObject.Parse<Document>(await response.Content.ReadAsStringAsync());

		TContext context = (TContext)Activator.CreateInstance(typeof(TContext), document)!;
		PlanningCenterMetadata metadata = context.GetDocumentMeta().GetData<PlanningCenterMetadata>();
		return new()
		{
			NextPageOffset = metadata.Next?.Offset ?? default,
			PreviousPageOffset = metadata.Prev?.Offset ?? default,
			TotalCount = metadata.TotalCount ?? default,
			Resources = context.GetResourceCollection<T>()
		};
	}

	/// <inheritdoc />
	public Task<PaginatedResourceCollection<T>> GetAllAsync(int count)
	{
		AddParameters("per_page", $"{count}");
		return GetAllAsync();
	}

	/// <inheritdoc />
	public Task<PaginatedResourceCollection<T>> GetAllAsync(int count, int offset)
	{
		AddParameters("offset", $"{offset}");
		return GetAllAsync(count);
	}
	
	/// <summary>
	/// Retrieves a singleton fetchable resource with the given ID.
	/// </summary>
	/// <param name="id">The ID of the resource.</param>
	/// <returns>A singleton fetchable resource.</returns>
	public TSingleton WithID(string id) => (TSingleton)Activator.CreateInstance(typeof(TSingleton), Uri)!;

	/// <summary>
	/// Adds a parameter to the request for ordering the returned resources.
	/// </summary>
	/// <param name="orderer">The orderable attribute.</param>
	/// <typeparam name="TEnum">The enumerable associated with the orderable attributes.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected TSelf OrderBy<TEnum>(TEnum orderer)
		=> AddParameters("order", orderer.GetJsonApiName());

	/// <summary>
	/// Adds queries to the request.
	/// </summary>
	/// <param name="queries">A collection of query parameters.</param>
	/// <typeparam name="TEnum">The enumerable associated with the queryable attributes.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected TSelf Query<TEnum>(params KeyValuePair<TEnum, string>[] queries)
	{
		foreach (KeyValuePair<TEnum, string> query in queries)
		{
			AddParameters($"where[{query.Key.GetJsonApiName()}]", query.Value);
		}
		return (this as TSelf)!;
	}

	/// <summary>
	/// Adds filters to the request.
	/// </summary>
	/// <param name="filters">A collection of filterable attributes.</param>
	/// <typeparam name="TEnum">The enumerable associated with the filterable attributes.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected TSelf FilterBy<TEnum>(params TEnum[] filters)
		=> AddParameters("filter", filters.Select(f => f.GetJsonApiName()).ToArray());
}