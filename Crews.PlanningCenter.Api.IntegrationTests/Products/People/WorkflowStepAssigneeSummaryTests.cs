using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowStepAssigneeSummaryTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowStepAssigneeSummary_GetAsync_ReturnsAssigneeSummary()
	{
		var stepId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/workflows/{Fixture.WorkflowId}/steps");

		var assigneeSummaryId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient,
			$"people/v2/workflows/{Fixture.WorkflowId}/steps/{stepId}/assignee_summaries");

		var result = await Org.Workflows.WithId(Fixture.WorkflowId).Steps
			.WithId(stepId!).AssigneeSummaries.WithId(assigneeSummaryId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
