using System.Reflection;
using System.Text;
using JsonApiFramework;
using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <inheritdoc />
public abstract class PlanningCenterSingletonFetchableResource<TResource, TSelf, TContext>(
	Uri uri, HttpClient client)
	: PlanningCenterFetchableResource<TSelf>(uri, client), ISingletonFetchableResource<TResource>
	where TResource : class
	where TSelf : PlanningCenterSingletonFetchableResource<TResource, TSelf, TContext>
	where TContext : PlanningCenterDocumentContext
{
	/// <inheritdoc />
	public async Task<TResource?> GetAsync()
		=> TryGetResource(await FetchDocumentAsync(new() { RequestUri = Uri }));

	/// <summary>
	/// Posts the specified resource to Planning Center.
	/// </summary>
	/// <param name="resource">The resource to post.</param>
	/// <returns>An instance of the resource representing its newly created server instance.</returns>
	protected async Task<TResource?> PostAsync(TResource resource)
		=> TryGetResource(await FetchDocumentAsync(new()
		{
			RequestUri = Uri,
			Method = HttpMethod.Post,
			Content = BuildDocumentContent(resource)
		}));

	/// <summary>
	/// Patches the specified resource to Planning Center.
	/// </summary>
	/// <param name="resource">The resource to patch.</param>
	/// <returns>An instance of the resource representing its newly modified server instance.</returns>
	protected async Task<TResource?> PatchAsync(TResource resource)
		=> TryGetResource(await FetchDocumentAsync(new()
		{
			RequestUri = Uri,
			Method = HttpMethod.Patch,
			Content = BuildDocumentContent(resource)
		}));

	/// <summary>
	/// Deletes the specified resource from Planning Center.
	/// </summary>
	/// <returns>An instance of the resource representing its newly modified server instance.</returns>
	public Task DeleteAsync() => Client.SendAsync(new()
	{
		RequestUri = Uri,
		Method = HttpMethod.Delete
	});

	/// <summary>
	/// Creates a <see cref="StringContent"/> instance containing the JSON representation of a 
	/// <typeparamref name="TResource"/> instance. 
	/// </summary>
	/// <param name="resource">The <typeparamref name="TResource"/> instance to parse.</param>
	/// <returns>A <see cref="StringContent"/> instance.</returns>
	protected static StringContent BuildDocumentContent(TResource resource)
	{
		using TContext context = (TContext)Activator.CreateInstance(
			typeof(TContext),
			BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
			default,
			[],
			default)!;
		return new(
			context.NewDocument().Resource(resource).ResourceEnd().WriteDocument().ToJson(), 
			Encoding.UTF8, 
			"application/json");
	}

	private static TResource? TryGetResource(Document? document)
	{
		if (document == null) return null;
		TContext context = (TContext)Activator.CreateInstance(
			typeof(TContext), 
			BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
			default,
			[document],
			default)!;
		return context.GetResource<TResource>();
	}
}
