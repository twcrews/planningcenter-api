using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.CheckIns;

public class EventTimeTests(CheckInsFixture fixture) : CheckInsTestBase(fixture)
{
	[Fact]
	public async Task EventTime_GetAsync_ReturnsEventTime()
	{
		var result = await Org.EventTimes.WithId(Fixture.EventTimeId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
