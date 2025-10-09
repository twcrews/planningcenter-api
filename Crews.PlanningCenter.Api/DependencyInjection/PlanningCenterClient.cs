using Crews.PlanningCenter.Api.Clients;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Default implementation of <see cref="IPlanningCenterClient"/>.
/// </summary>
public class PlanningCenterClient : IPlanningCenterClient
{
	private readonly HttpClient _httpClient;

	/// <inheritdoc />
	public CalendarClient Calendar => new(_httpClient);

	/// <inheritdoc />
	public CheckInsClient CheckIns => new(_httpClient);

	/// <inheritdoc />
	public GivingClient Giving => new(_httpClient);

	/// <inheritdoc />
	public GroupsClient Groups => new(_httpClient);

	/// <inheritdoc />
	public PeopleClient People => new(_httpClient);

	/// <inheritdoc />
	public PublishingClient Publishing => new(_httpClient);

	/// <inheritdoc />
	public ServicesClient Services => new(_httpClient);

	/// <summary>
	/// Creates a new instance of the service.
	/// </summary>
	/// <param name="options">Configuration options for the service.</param>
	/// <param name="httpClientFactory">The <see cref="HttpClient"/> factory used for API instances.</param>
	/// <remarks>
	/// <para>
	/// <b>NOTE:</b> If <see cref="PlanningCenterClientOptions.HttpClientName"/> is set, the specified named client's
	/// <see cref="HttpClient.BaseAddress"/> property will be ignored, and will be replaced with the value of
	/// <see cref="PlanningCenterClientOptions.ApiBaseAddress"/> (which defaults to
	/// <c>https://api.planningcenteronline.com</c>).
	/// </para>
	/// <para>
	/// Authentication is handled by the <see cref="Authentication.PlanningCenterAuthenticationHandler"/> delegating handler,
	/// which automatically applies Personal Access Tokens (from configuration) or OAuth tokens (from the authenticated user).
	/// </para>
	/// </remarks>
	public PlanningCenterClient(IOptions<PlanningCenterClientOptions> options, IHttpClientFactory httpClientFactory)
	{
		HttpClient client = httpClientFactory.CreateClient(options.Value.HttpClientName);
		client.BaseAddress = options.Value.ApiBaseAddress;
		client.DefaultRequestHeaders.UserAgent.ParseAdd(options.Value.UserAgent);

		_httpClient = client;
	}
}
