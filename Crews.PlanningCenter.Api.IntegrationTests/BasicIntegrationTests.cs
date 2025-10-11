using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

namespace Crews.PlanningCenter.Api.IntegrationTests;

/// <summary>
/// Basic integration tests to verify the Planning Center API client works end-to-end.
/// These tests make real API calls and require valid credentials.
/// </summary>
[Trait("Category", "Integration")]
public class BasicIntegrationTests : IClassFixture<PlanningCenterClientFixture>
{
	private readonly PlanningCenterClientFixture _fixture;

	public BasicIntegrationTests(PlanningCenterClientFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact(DisplayName = "Can access People API client")]
	public void PeopleClient_IsAccessible()
	{
		// Arrange & Act
		var peopleClient = _fixture.Client.People;

		// Assert
		Assert.NotNull(peopleClient);
		Assert.NotNull(peopleClient.LatestVersion);
	}

	[Fact(DisplayName = "Can fetch current user via 'me' endpoint")]
	public async Task GetCurrentUser_ReturnsData()
	{
		// Arrange
		if (!_fixture.HasCredentials())
		{
			// Skip test if credentials not configured
			return;
		}

		// Act
		var result = await _fixture.Client.People.LatestVersion
			.GetRelated<Resources.People.V2025_07_17.PersonResource>("me")
			.GetAsync();

		// Assert
		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		TestHelpers.AssertJsonApiDocument(result.JsonApiDocument);
	}

	[Fact(DisplayName = "Can fetch people collection with pagination")]
	public async Task GetPeople_WithPagination_ReturnsData()
	{
		// Arrange
		if (!_fixture.HasCredentials())
		{
			// Skip test if credentials not configured
			return;
		}

		// Act
		var result = await _fixture.Client.People.LatestVersion
			.People
			.GetAllAsync(count: 5);

		// Assert
		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		TestHelpers.AssertJsonApiDocument(result.JsonApiDocument);
		Assert.True(result.Data.Count() <= 5, $"Expected at most 5 results, got {result.Data.Count()}");
	}

	[Fact(DisplayName = "Can access Calendar API client")]
	public void CalendarClient_IsAccessible()
	{
		// Arrange & Act
		var calendarClient = _fixture.Client.Calendar;

		// Assert
		Assert.NotNull(calendarClient);
		Assert.NotNull(calendarClient.LatestVersion);
	}

	[Fact(DisplayName = "Can fetch calendar events")]
	public async Task GetEvents_ReturnsData()
	{
		// Arrange
		if (!_fixture.HasCredentials())
		{
			// Skip test if credentials not configured
			return;
		}

		// Act
		var result = await _fixture.Client.Calendar.LatestVersion
			.Events
			.GetAllAsync(count: 3);

		// Assert
		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		TestHelpers.AssertJsonApiDocument(result.JsonApiDocument);
	}

	[Fact(DisplayName = "Can access CheckIns API client")]
	public void CheckInsClient_IsAccessible()
	{
		// Arrange & Act
		var checkInsClient = _fixture.Client.CheckIns;

		// Assert
		Assert.NotNull(checkInsClient);
		Assert.NotNull(checkInsClient.LatestVersion);
	}

	[Fact(DisplayName = "Can access Giving API client")]
	public void GivingClient_IsAccessible()
	{
		// Arrange & Act
		var givingClient = _fixture.Client.Giving;

		// Assert
		Assert.NotNull(givingClient);
		Assert.NotNull(givingClient.LatestVersion);
	}

	[Fact(DisplayName = "Can access Groups API client")]
	public void GroupsClient_IsAccessible()
	{
		// Arrange & Act
		var groupsClient = _fixture.Client.Groups;

		// Assert
		Assert.NotNull(groupsClient);
		Assert.NotNull(groupsClient.LatestVersion);
	}

	[Fact(DisplayName = "Can access Publishing API client")]
	public void PublishingClient_IsAccessible()
	{
		// Arrange & Act
		var publishingClient = _fixture.Client.Publishing;

		// Assert
		Assert.NotNull(publishingClient);
		Assert.NotNull(publishingClient.LatestVersion);
	}

	[Fact(DisplayName = "Can access Services API client")]
	public void ServicesClient_IsAccessible()
	{
		// Arrange & Act
		var servicesClient = _fixture.Client.Services;

		// Assert
		Assert.NotNull(servicesClient);
		Assert.NotNull(servicesClient.LatestVersion);
	}
}
