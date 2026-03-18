using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.CheckIns;

public class LocationEventTimeTests(CheckInsFixture fixture) : CheckInsTestBase(fixture)
{
	[Fact]
	public async Task LocationEventTime_GetAsync_ReturnsLocationEventTime()
	{
		var result = await Org.EventTimes.WithId(Fixture.EventTimeId)
			.LocationEventTimes.WithId(Fixture.LocationEventTimeId)
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
