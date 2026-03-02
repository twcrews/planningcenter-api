using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class RoomSetupTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task RoomSetup_GetAsync_ReturnsRoomSetup()
	{
		var result = await Org.RoomSetups.WithId(Fixture.RoomSetupId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
