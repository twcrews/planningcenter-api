using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class ResourceTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Resource_GetAsync_ReturnsResource()
	{
		var resourceId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"groups/v2/groups/{Fixture.GroupId}/resources");
		Assert.NotNull(resourceId);

		var result = await Org.Groups.WithId(Fixture.GroupId!).Resources.WithId(resourceId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
