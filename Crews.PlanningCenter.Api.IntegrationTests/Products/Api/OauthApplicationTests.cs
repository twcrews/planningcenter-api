using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Api;

public class OauthApplicationTests(ApiFixture fixture) : ApiTestBase(fixture)
{
	[Fact]
	public async Task OauthApplication_GetAsync_ReturnsOauthApplication()
	{
		Assert.NotNull(Fixture.OauthApplicationId);

		var result = await Org.OauthApplications.WithId(Fixture.OauthApplicationId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.OauthApplicationId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
