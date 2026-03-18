using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowCardActivityTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowCardActivity_GetAsync_ReturnsWorkflowCardActivity()
	{
		// Planning Center sorts workflows by created_at descending, so the first created workflow is last in the list.
		var workflowId = await CollectionReadHelper.GetLastIdAsync(
			HttpClient, $"people/v2/workflows");

		var cardId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/workflows/{workflowId}/cards");

		var activityId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/workflows/{workflowId}/cards/{cardId}/activities");

		var result = await Org.Workflows.WithId(workflowId!).Cards
			.WithId(cardId!).Activities.WithId(activityId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
