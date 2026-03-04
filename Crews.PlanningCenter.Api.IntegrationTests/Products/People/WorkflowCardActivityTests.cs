using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowCardActivityTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowCardActivity_GetAsync_ReturnsWorkflowCardActivity()
	{
		var cardId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/workflows/{Fixture.WorkflowId}/cards");
		Skip.If(cardId is null, "No workflow card data available.");

		var activityId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/workflows/{Fixture.WorkflowId}/cards/{cardId}/activities");
		Skip.If(activityId is null, "No workflow card activity data available.");

		var result = await Org.Workflows.WithId(Fixture.WorkflowId).Cards
			.WithId(cardId!).Activities.WithId(activityId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
