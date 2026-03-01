using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class EventTimeTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task EventTime_Collection_ReturnsSuccessfully()
	{
		var response = await HttpClient.GetAsync(
			$"calendar/v2/event_instances/{Fixture.EventInstanceId}/event_times?per_page=1");

		Assert.True(response.IsSuccessStatusCode);
	}
}
