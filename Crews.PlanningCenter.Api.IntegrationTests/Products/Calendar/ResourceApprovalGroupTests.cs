using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ResourceApprovalGroupTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ResourceApprovalGroup_GetAsync_ReturnsResourceApprovalGroup()
	{
		var groupId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/resource_approval_groups");
		var result = await Org.ResourceApprovalGroups.WithId(groupId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
