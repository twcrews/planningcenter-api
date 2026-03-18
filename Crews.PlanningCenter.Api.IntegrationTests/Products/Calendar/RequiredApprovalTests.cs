using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class RequiredApprovalTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task RequiredApproval_GetAsync_ReturnsRequiredApproval()
	{
		var groupId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/resource_approval_groups");
		var approvalId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"calendar/v2/resource_approval_groups/{groupId}/required_approvals");
		var result = await Org.ResourceApprovalGroups.WithId(groupId!).RequiredApprovals.WithId(approvalId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
