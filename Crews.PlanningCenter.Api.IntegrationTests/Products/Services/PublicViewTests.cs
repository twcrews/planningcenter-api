using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PublicViewTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task PublicView_GetAsync_ReturnsPublicView()
	{
		var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId).PublicView.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
