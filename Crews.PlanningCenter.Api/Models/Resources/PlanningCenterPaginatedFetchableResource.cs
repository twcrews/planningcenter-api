﻿using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using JsonApiFramework;
using JsonApiFramework.JsonApi;
using System.Reflection;

namespace Crews.PlanningCenter.Api.Models.Resources;

/// <inheritdoc />
public abstract class PlanningCenterPaginatedFetchableResource<TResource, TSelf, TSingleton, TContext>(
	Uri uri, HttpClient client)
	: PlanningCenterFetchableResource<TSelf>(uri, client), IPaginatedFetchableResource<TResource>
	where TSelf : PlanningCenterPaginatedFetchableResource<TResource, TSelf, TSingleton, TContext>
	where TSingleton : PlanningCenterSingletonFetchableResource<TResource, TSingleton, TContext>
	where TContext : PlanningCenterDocumentContext
	where TResource : class
{
	/// <inheritdoc />
	public async Task<JsonApiCollectionResponse<TResource>> GetAllAsync()
	{
		HttpResponseMessage response = await Client.SendAsync(new() { RequestUri = Uri });
		Document? document = await GetDocumentAsync(response);

		if (document == null) return new() { RawResponse = response };

		TContext context = (TContext)Activator.CreateInstance(
			typeof(TContext),
			BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
			default,
			[document],
			default)!;
		JsonApiMetadata? metadata = context.GetDocumentMeta()?.GetData<JsonApiMetadata>();
		return new()
		{
			Data = context.GetResourceCollection<TResource>(),
			Metadata = metadata,
			RawResponse = response,
			JsonApiDocument = document
		};
	}

	/// <inheritdoc />
	public Task<JsonApiCollectionResponse<TResource>> GetAllAsync(int count)
	{
		AddParameters("per_page", $"{count}");
		return GetAllAsync();
	}

	/// <inheritdoc />
	public Task<JsonApiCollectionResponse<TResource>> GetAllAsync(int count, int offset)
	{
		AddParameters("offset", $"{offset}");
		return GetAllAsync(count);
	}

	/// <summary>
	/// Retrieves a singleton fetchable resource with the given ID.
	/// </summary>
	/// <param name="id">The ID of the resource.</param>
	/// <returns>A singleton fetchable resource.</returns>
	public TSingleton WithID(string id)
		=> (TSingleton)Activator.CreateInstance(
			typeof(TSingleton),
			BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
			default,
			[Uri.SafelyAppendPath(id), Client],
			default)!;

	/// <summary>
	/// Adds a parameter to the request for ordering the returned resources.
	/// </summary>
	/// <param name="orderer">The orderable attribute.</param>
	/// <param name="order">The sorting method for the <see cref="IOrderable{TSelf, TEnum}"/> items.</param>
	/// <typeparam name="TEnum">The enumerable associated with the orderable attributes.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected TSelf OrderBy<TEnum>(TEnum orderer, Order order)
		=> AddParameters("order", (order == Order.Descending ? "-" : "") + orderer.GetJsonApiName());

	/// <summary>
	/// Adds queries to the request.
	/// </summary>
	/// <param name="queries">A collection of query parameters.</param>
	/// <typeparam name="TEnum">The enumerable associated with the queryable attributes.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected TSelf Query<TEnum>(params (TEnum, string)[] queries)
	{
		foreach ((TEnum, string) query in queries)
		{
			AddParameters($"where[{query.Item1.GetJsonApiName()}]", query.Item2);
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