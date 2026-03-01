using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class EventInstanceTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task EventInstance_GetAsync_ReturnsEventInstance()
	{
		var result = await Org.EventInstances.WithId(Fixture.EventInstanceId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
