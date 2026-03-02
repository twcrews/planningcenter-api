using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class CampusTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Campus_GetAsync_ReturnsCampus()
	{
		var result = await Org.Campuses.WithId(Fixture.CampusId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
