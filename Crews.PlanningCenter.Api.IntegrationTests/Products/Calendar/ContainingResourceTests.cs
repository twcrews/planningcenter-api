using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ContainingResourceTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ContainingResource_GetAsync_ReturnsContainingResource()
	{
		var result = await Org.RoomSetups.WithId(Fixture.RoomSetupId).ContainingResource.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
