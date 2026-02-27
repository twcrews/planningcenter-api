using System.Text.Json.Nodes;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Helper for reading collection endpoints directly via HttpClient.
/// Workaround for PaginatedResourceClient.GetAsync being protected.
/// </summary>
public static class CollectionReadHelper
{
	/// <summary>
	/// Fetches the first resource ID from a collection endpoint.
	/// Returns null if the collection is empty.
	/// </summary>
	/// <param name="httpClient">The configured HttpClient instance.</param>
	/// <param name="collectionPath">Relative path to the collection endpoint (e.g., "check-ins/v2/events").</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	public static async Task<string?> GetFirstIdAsync(
		HttpClient httpClient,
		string collectionPath,
		CancellationToken cancellationToken = default)
	{
		var response = await httpClient.GetAsync($"{collectionPath}?per_page=1", cancellationToken);
		response.EnsureSuccessStatusCode();

		var json = await response.Content.ReadAsStringAsync(cancellationToken);
		var doc = JsonNode.Parse(json);
		var data = doc?["data"]?.AsArray();

		return data?.FirstOrDefault()?["id"]?.GetValue<string>();
	}
}
