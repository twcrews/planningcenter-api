using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PersonTeamPositionAssignmentTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task PersonTeamPositionAssignment_GetCollectionAsync_ReturnsCollection()
	{
		var teamId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/teams");
		Assert.NotNull(teamId);

		var result = await Org.Teams.WithId(teamId).PersonTeamPositionAssignments.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
