using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class CampusTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Campus_GetAsync_ReturnsCampus()
	{
		var campusId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "groups/v2/campuses");
		Assert.NotNull(campusId);

		var result = await Org.Campuses.WithId(campusId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
