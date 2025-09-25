using System.Net;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.DependencyInjection;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;
using Microsoft.Extensions.Options;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.DependencyInjection;

public class PlanningCenterApiServiceTests
{
	[Theory(DisplayName = "Constructor correctly propogates options to client instances.")]
	[InlineData(null, null)]
	[InlineData("https://test.com", null)]
	[InlineData(null, "testClient")]
	[InlineData("https://test.com", "testClient")]
	public void PlanningCenterApiService_CorrectlyParsesOptions(string? baseAddressArg, string? httpClientNameArg)
	{
		Uri baseAddress = new(baseAddressArg ?? PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress);
		string httpClientName = httpClientNameArg ?? PlanningCenterApiOptions.DefaultHttpClientName;
		PlanningCenterPersonalAccessToken personalAccessToken = new()
		{
			AppId = "abc",
			Secret = "def"
		};

		PlanningCenterApiOptions options = new()
		{
			ApiBaseAddress = baseAddress,
			PersonalAccessToken = personalAccessToken,
			HttpClientName = httpClientName
		};

		HttpClient client = new();
		IHttpClientFactory clientFactory = Substitute.For<IHttpClientFactory>();

		clientFactory.CreateClient(Arg.Any<string>()).Returns(client);

		PlanningCenterApiService _ = new(Options.Create(options), clientFactory);

		clientFactory.Received().CreateClient(httpClientName);
		Assert.Equal(baseAddress, client.BaseAddress);
		Assert.Equal(personalAccessToken, client.DefaultRequestHeaders.Authorization);
	}

	[Fact(DisplayName = "API client properties return correctly configured client classes.")]
	public async Task Properties_CorrectlyReturnApiClients()
	{
		MockHttpMessageHandler handler = new();
		HttpClient httpClient = new(handler);
		IHttpClientFactory clientFactory = Substitute.For<IHttpClientFactory>();
		clientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

		handler.Expect("http://test.com/calendar/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyCalendarOrganizationObject);
		handler.Expect("http://test.com/check-ins/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyCheckInsOrganizationObject);
		handler.Expect("http://test.com/giving/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyGivingOrganizationObject);
		handler.Expect("http://test.com/groups/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyGroupsOrganizationObject);
		handler.Expect("http://test.com/people/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyPeopleOrganizationObject);
		handler.Expect("http://test.com/publishing/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyPublishingOrganizationObject);
		handler.Expect("http://test.com/services/v2")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyServicesOrganizationObject);

		PlanningCenterPersonalAccessToken personalAccessToken = new()
		{
			AppId = "abc",
			Secret = "def"
		};

		PlanningCenterApiOptions options = new()
		{
			ApiBaseAddress = new("http://test.com"),
			PersonalAccessToken = personalAccessToken
		};

		PlanningCenterApiService subject = new(Options.Create(options), clientFactory);

		JsonApiSingletonResponse<PlanningCenter.Models.Calendar.V2022_07_07.Entities.Organization> calendarOrg = await subject.Calendar.LatestVersion.GetAsync();
		JsonApiSingletonResponse<PlanningCenter.Models.CheckIns.V2025_05_28.Entities.Organization> checkInsOrg = await subject.CheckIns.LatestVersion.GetAsync();
		JsonApiSingletonResponse<PlanningCenter.Models.Giving.V2019_10_18.Entities.Organization> givingOrg = await subject.Giving.LatestVersion.GetAsync();
		JsonApiSingletonResponse<PlanningCenter.Models.Groups.V2023_07_10.Entities.Organization> groupsOrg = await subject.Groups.LatestVersion.GetAsync();
		JsonApiSingletonResponse<PlanningCenter.Models.People.V2025_07_17.Entities.Organization> peopleOrg = await subject.People.LatestVersion.GetAsync();
		JsonApiSingletonResponse<PlanningCenter.Models.Publishing.V2024_03_25.Entities.Organization> publishingOrg = await subject.Publishing.LatestVersion.GetAsync();
		JsonApiSingletonResponse<PlanningCenter.Models.Services.V2018_11_01.Entities.Organization> servicesOrg = await subject.Services.LatestVersion.GetAsync();

		string calendarResponse = await calendarOrg.RawResponse.Content.ReadAsStringAsync();
		string checkInsResponse = await checkInsOrg.RawResponse.Content.ReadAsStringAsync();
		string givingResponse = await givingOrg.RawResponse.Content.ReadAsStringAsync();
		string groupsResponse = await groupsOrg.RawResponse.Content.ReadAsStringAsync();
		string peopleResponse = await peopleOrg.RawResponse.Content.ReadAsStringAsync();
		string publishingResponse = await publishingOrg.RawResponse.Content.ReadAsStringAsync();
		string servicesResponse = await servicesOrg.RawResponse.Content.ReadAsStringAsync();

		Assert.Equal(Serialized.DummyCalendarOrganizationObject, calendarResponse);
		Assert.Equal(Serialized.DummyCheckInsOrganizationObject, checkInsResponse);
		Assert.Equal(Serialized.DummyGivingOrganizationObject, givingResponse);
		Assert.Equal(Serialized.DummyGroupsOrganizationObject, groupsResponse);
		Assert.Equal(Serialized.DummyPeopleOrganizationObject, peopleResponse);
		Assert.Equal(Serialized.DummyPublishingOrganizationObject, publishingResponse);
		Assert.Equal(Serialized.DummyServicesOrganizationObject, servicesResponse);
	}

	private static string RemoveWhitespace(string input)
		=> new([.. input.ToCharArray().Where(c => !char.IsWhiteSpace(c))]);
}
