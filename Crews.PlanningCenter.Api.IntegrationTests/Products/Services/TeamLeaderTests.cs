using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class TeamLeaderTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task TeamLeader_GetCollectionAsync_ReturnsCollection()
	{
		var teamId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/teams");
		Assert.NotNull(teamId);

		var result = await Org.Teams.WithId(teamId).TeamLeaders.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
