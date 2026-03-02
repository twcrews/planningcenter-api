using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.CheckIns;

public class PassTests(CheckInsFixture fixture) : CheckInsTestBase(fixture)
{
	[Fact]
	public async Task Pass_GetAsync_ReturnsPass()
	{
		var result = await Org.Passes.WithId(Fixture.PassId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
