using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.CheckIns;

public class LocationEventPeriodTests(CheckInsFixture fixture) : CheckInsTestBase(fixture)
{
	[Fact]
	public async Task LocationEventPeriod_GetAsync_ReturnsLocationEventPeriod()
	{
		var result = await Org.Events.WithId(Fixture.EventId)
			.EventPeriods.WithId(Fixture.EventPeriodId)
			.LocationEventPeriods.WithId(Fixture.LocationEventPeriodId)
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
