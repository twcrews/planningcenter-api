using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class TeamTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Team_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.Teams.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task Team_GetAsync_ReturnsTeam()
	{
		var teamId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/teams");
		Assert.NotNull(teamId);

		var result = await Org.Teams.WithId(teamId).GetAsync();
		Assert.NotNull(result.Data);
		Assert.Equal(teamId, result.Data.Id);
	}
}
