using Crews.PlanningCenter.Api.Clients;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Default implementation of <see cref="IPlanningCenterApiService"/>.
/// </summary>
public class PlanningCenterApiService : IPlanningCenterApiService
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
	/// <b>NOTE:</b> If <see cref="PlanningCenterApiOptions.HttpClientName"/> is set, the specified named client's 
	/// <see cref="HttpClient.BaseAddress"/> property will be ignored, and will be replaced with the value of
	/// <see cref="PlanningCenterApiOptions.ApiBaseAddress"/> (which defaults to 
	/// <c>https://api.planningcenteronline.com</c>).
	/// </remarks>
	public PlanningCenterApiService(IOptions<PlanningCenterApiOptions> options, IHttpClientFactory httpClientFactory)
	{
		HttpClient client = httpClientFactory.CreateClient(options.Value.HttpClientName);
		client.BaseAddress = options.Value.ApiBaseAddress;
		client.DefaultRequestHeaders.Authorization = options.Value.PersonalAccessToken;
		_httpClient = client;
	}
}
