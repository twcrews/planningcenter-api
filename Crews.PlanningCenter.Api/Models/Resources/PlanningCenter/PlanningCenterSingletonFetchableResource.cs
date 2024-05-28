using System.Text.Json;
using Crews.PlanningCenter.Api.Services;
using JsonApiFramework;
using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <inheritdoc />
public abstract class PlanningCenterSingletonFetchableResource<T, TSelf, TContext>(Uri uri)
	: PlanningCenterFetchableResource<TSelf>(uri), ISingletonFetchableResource<T>
	where T : class
	where TSelf : PlanningCenterSingletonFetchableResource<T, TSelf, TContext>
	where TContext : PlanningCenterDocumentContext
{
	/// <inheritdoc />
	public async Task<T> GetAsync()
	{
		Document document = await GetDocumentAsync(new() { RequestUri = Uri });

		TContext context = (TContext)Activator.CreateInstance(typeof(TContext), document)!;
		return context.GetResource<T>();
	}

	/// <summary>
	/// Posts the specified resource to Planning Center.
	/// </summary>
	/// <param name="resource">The resource to post.</param>
	/// <returns>An instance of the resource representing its newly created server instance.</returns>
	protected async Task<T?> PostAsync(T resource)
	{
		Document document = await GetDocumentAsync(new()
		{
			RequestUri = Uri,
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(resource))
		});

		TContext context = (TContext)Activator.CreateInstance(typeof(TContext), document)!;
		return context.GetResource<T>();
	}

	/// <summary>
	/// Patches the specified resource to Planning Center.
	/// </summary>
	/// <param name="resource">The resource to patch.</param>
	/// <returns>An instance of the resource representing its newly modified server instance.</returns>
	protected async Task<T?> PatchAsync(T resource)
	{
		Document document = await GetDocumentAsync(new()
		{
			RequestUri = Uri,
			Method = HttpMethod.Patch,
			Content = new StringContent(JsonSerializer.Serialize(resource))
		});

		TContext context = (TContext)Activator.CreateInstance(typeof(TContext), document)!;
		return context.GetResource<T>();
	}

	/// <summary>
	/// Deletes the specified resource from Planning Center.
	/// </summary>
	/// <returns>An instance of the resource representing its newly modified server instance.</returns>
	public async Task DeleteAsync()
	{
		PlanningCenterApiClient client = new(new()
		{
			BaseAddress = new("https://api.planningcenteronline.com/")
		});

		await client.SendAsync(new()
		{
			RequestUri = Uri,
			Method = HttpMethod.Delete
		});
	}
}
