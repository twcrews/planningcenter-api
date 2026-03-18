using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Workflow_FullCrudLifecycle()
	{
		string? workflowId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Workflows.PostAsync(
				new Workflow { Name = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			workflowId = createResult.Data.Id;
			Assert.NotNull(workflowId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Workflows.WithId(workflowId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(workflowId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Workflows.WithId(workflowId).PatchAsync(
				new Workflow { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Workflows.WithId(workflowId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Workflows.WithId(workflowId).DeleteAsync();
			workflowId = null;
		}
		finally
		{
			if (workflowId is not null)
			{
				try
				{
					await Org.Workflows.WithId(workflowId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
