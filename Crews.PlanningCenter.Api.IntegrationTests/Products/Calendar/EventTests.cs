using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class EventTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Event_GetAsync_ReturnsEvent()
	{
		var result = await Org.Events.WithId(Fixture.EventId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
