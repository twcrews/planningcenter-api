using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ResourceSuggestionTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ResourceSuggestion_GetAsync_ReturnsResourceSuggestion()
	{
		var suggestionId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"calendar/v2/room_setups/{Fixture.RoomSetupId}/resource_suggestions");
		var result = await Org.RoomSetups.WithId(Fixture.RoomSetupId).ResourceSuggestions.WithId(suggestionId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
