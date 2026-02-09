using System.Net.Http.Headers;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Extension methods for configuring <see cref="HttpClient"/> with Planning Center authentication.
/// </summary>
public static class HttpClientAuthenticationExtensions
{
	/// <summary>
	/// Adds Planning Center Personal Access Token authentication to the HTTP client.
	/// </summary>
	/// <param name="client">The HTTP client to configure.</param>
	/// <param name="token">The Personal Access Token for authentication.</param>
	/// <returns>The configured HTTP client for method chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
	public static HttpClient AddPlanningCenterAuth(
		this HttpClient client,
		PlanningCenterPersonalAccessToken token)
	{
		ArgumentNullException.ThrowIfNull(client);

		client.DefaultRequestHeaders.Authorization = token;
		return client;
	}

	/// <summary>
	/// Adds Planning Center OAuth bearer token authentication to the HTTP client.
	/// </summary>
	/// <param name="client">The HTTP client to configure.</param>
	/// <param name="bearerToken">The OAuth bearer token for authentication.</param>
	/// <returns>The configured HTTP client for method chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
	/// <exception cref="ArgumentException">Thrown when bearerToken is null or whitespace.</exception>
	public static HttpClient AddPlanningCenterAuth(
		this HttpClient client,
		string bearerToken)
	{
		ArgumentNullException.ThrowIfNull(client);
		ArgumentException.ThrowIfNullOrWhiteSpace(bearerToken);

		client.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", bearerToken);
		return client;
	}

	/// <summary>
	/// Configures the HTTP client with Planning Center API defaults (base URL and JSON:API accept header).
	/// </summary>
	/// <param name="client">The HTTP client to configure.</param>
	/// <param name="baseUrl">The base URL for the Planning Center API. Defaults to "https://api.planningcenteronline.com".</param>
	/// <returns>The configured HTTP client for method chaining.</returns>
	/// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
	/// <exception cref="ArgumentException">Thrown when baseUrl is null or whitespace.</exception>
	public static HttpClient ConfigureForPlanningCenter(
		this HttpClient client,
		string baseUrl = "https://api.planningcenteronline.com")
	{
		ArgumentNullException.ThrowIfNull(client);
		ArgumentException.ThrowIfNullOrWhiteSpace(baseUrl);

		client.BaseAddress = new Uri(baseUrl);
		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
		return client;
	}
}
