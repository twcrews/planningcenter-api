using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.CheckIns;

public class IntegrationLinkTests(CheckInsFixture fixture) : CheckInsTestBase(fixture)
{
	[Fact]
	public async Task IntegrationLink_GetAsync_ReturnsIntegrationLink()
	{
		var result = await Org.IntegrationLinks.WithId(Fixture.IntegrationLinkId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
