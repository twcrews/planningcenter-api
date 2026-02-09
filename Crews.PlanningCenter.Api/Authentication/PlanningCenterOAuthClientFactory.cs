namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Factory for creating instances of <see cref="PlanningCenterOAuthClient"/>.
/// </summary>
public static class PlanningCenterOAuthClientFactory
{
	/// <summary>
	/// Creates a new instance of <see cref="PlanningCenterOAuthClient"/> with the specified options.
	/// Uses a new <see cref="HttpClient"/> instance.
	/// </summary>
	/// <param name="options">The OAuth client options.</param>
	/// <returns>A new instance of <see cref="PlanningCenterOAuthClient"/>.</returns>
	/// <exception cref="ArgumentNullException">Thrown when options is null.</exception>
	public static PlanningCenterOAuthClient Create(
		PlanningCenterOAuthClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);

		var httpClient = new HttpClient();
		return new PlanningCenterOAuthClient(httpClient, options);
	}

	/// <summary>
	/// Creates a new instance of <see cref="PlanningCenterOAuthClient"/> with a custom <see cref="HttpClient"/>.
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for OAuth operations.</param>
	/// <param name="options">The OAuth client options.</param>
	/// <returns>A new instance of <see cref="PlanningCenterOAuthClient"/>.</returns>
	/// <exception cref="ArgumentNullException">Thrown when httpClient or options is null.</exception>
	public static PlanningCenterOAuthClient Create(
		HttpClient httpClient,
		PlanningCenterOAuthClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(httpClient);
		ArgumentNullException.ThrowIfNull(options);

		return new PlanningCenterOAuthClient(httpClient, options);
	}
}
