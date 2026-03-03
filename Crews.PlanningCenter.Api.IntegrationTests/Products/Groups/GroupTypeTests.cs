using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class GroupTypeTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task GroupType_GetAsync_ReturnsGroupType()
	{
		var groupTypeId = await CollectionReadHelper.GetLastIdAsync(
			HttpClient, "groups/v2/group_types");
		Assert.NotNull(groupTypeId);

		var result = await Org.GroupTypes.WithId(groupTypeId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
