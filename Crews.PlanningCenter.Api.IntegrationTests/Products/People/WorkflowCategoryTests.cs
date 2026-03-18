using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowCategoryTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowCategory_FullCrudLifecycle()
	{
		string? workflowCategoryId = null;

		try
		{
			var workflowCategories = new PaginatedWorkflowCategoryClient(
				HttpClient,
				new Uri(HttpClient.BaseAddress!, "people/v2/workflow_categories/"));

			// -- Create --
			var createResult = await workflowCategories.PostAsync(
				new WorkflowCategory { Name = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			workflowCategoryId = createResult.Data.Id;
			Assert.NotNull(workflowCategoryId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await workflowCategories.WithId(workflowCategoryId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(workflowCategoryId, readResult.Data.Id);

			// -- Update --
			var updateResult = await workflowCategories.WithId(workflowCategoryId).PatchAsync(
				new WorkflowCategory { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await workflowCategories.WithId(workflowCategoryId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await workflowCategories.WithId(workflowCategoryId).DeleteAsync();
			workflowCategoryId = null;
		}
		finally
		{
			if (workflowCategoryId is not null)
			{
				try
				{
					var workflowCategories = new PaginatedWorkflowCategoryClient(
						HttpClient,
						new Uri(HttpClient.BaseAddress!, "people/v2/workflow_categories/"));
					await workflowCategories.WithId(workflowCategoryId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
