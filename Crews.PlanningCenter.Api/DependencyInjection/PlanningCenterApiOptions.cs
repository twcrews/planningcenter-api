using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Represents options for configuring API client instances, especially in dependency injection scenarios.
/// </summary>
public class PlanningCenterApiOptions
{
	internal const string DefaultHttpClientName = "__defaultPlanningCenterApiHttpClient";
	internal const string DefaultPlanningCenterApiBaseAddress = "https://api.planningcenteronline.com";

	/// <summary>
	/// Name used for binding the instance to a configuration provider.
	/// </summary>
	public const string ConfigurationName = "PlanningCenterApi";

	/// <summary>
	/// The base address of the API. Defaults to <c>https://api.planningcenteronline.com</c>.
	/// </summary>
	public Uri ApiBaseAddress { get; set; } = new(DefaultPlanningCenterApiBaseAddress);

	/// <summary>
	/// An optional named <see cref="HttpClient"/> to use for Planning Center API requests.
	/// <a href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#named-clients">Learn more</a>.
	/// </summary>
	public string HttpClientName { get; set; } = DefaultHttpClientName;

	/// <summary>
	/// The User-Agent header value to send with API requests. Defaults to "Generic .NET Planning Center API Client".
	/// </summary>
	public string UserAgent { get; set; } = "Generic .NET Planning Center API Client";

	/// <summary>
	/// The personal access token used to authenticate with the API.
	/// <a href="https://developer.planning.center/docs/#/overview/authentication%23personal-access-token">Learn more</a>.
	/// </summary>
	public PlanningCenterPersonalAccessToken? PersonalAccessToken { get; set; }
}
