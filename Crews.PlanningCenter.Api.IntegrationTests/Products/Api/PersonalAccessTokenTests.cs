using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Api;

public class PersonalAccessTokenTests(ApiFixture fixture) : ApiTestBase(fixture)
{
	[Fact]
	public async Task PersonalAccessToken_GetAsync_ReturnsPersonalAccessToken()
	{
		Assert.NotNull(Fixture.PersonalAccessTokenId);

		var result = await Org.PersonalAccessTokens.WithId(Fixture.PersonalAccessTokenId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.PersonalAccessTokenId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
