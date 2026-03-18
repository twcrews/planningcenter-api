using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Api;

public class OauthApplicationMauTests(ApiFixture fixture) : ApiTestBase(fixture)
{
	[Fact]
	public async Task OauthApplicationMau_GetAsync_ReturnsMau()
	{
		Assert.NotNull(Fixture.OauthApplicationId);

		var result = await Org.OauthApplications.WithId(Fixture.OauthApplicationId).Mau.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
