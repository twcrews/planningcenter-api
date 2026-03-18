using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class GroupApplicationTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task GroupApplication_GetAsync_ReturnsGroupApplication()
	{
		var applicationId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "groups/v2/group_applications");
		Assert.NotNull(applicationId);

		var result = await Org.GroupApplications.WithId(applicationId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
