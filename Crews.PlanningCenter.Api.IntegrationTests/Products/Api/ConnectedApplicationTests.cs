using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Api;

public class ConnectedApplicationTests(ApiFixture fixture) : ApiTestBase(fixture)
{
	[Fact]
	public async Task ConnectedApplication_GetAsync_ReturnsConnectedApplication()
	{
		Assert.NotNull(Fixture.ConnectedApplicationId);

		var result = await Org.ConnectedApplications.WithId(Fixture.ConnectedApplicationId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.ConnectedApplicationId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
