using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class LiveTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Live_GetAsync_ReturnsLive()
	{
		var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
			.Plans.WithId(Fixture.PlanId).Live.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
