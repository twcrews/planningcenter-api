using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.CheckIns;

public class LocationTests(CheckInsFixture fixture) : CheckInsTestBase(fixture)
{
	[Fact]
	public async Task Location_GetAsync_ReturnsLocation()
	{
		var result = await Org.Events.WithId(Fixture.EventId).Locations.WithId(Fixture.LocationId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
