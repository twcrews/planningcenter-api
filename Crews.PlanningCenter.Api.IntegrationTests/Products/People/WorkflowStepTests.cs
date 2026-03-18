using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowStepTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowStep_FullCrudLifecycle()
	{
		string? stepId = null;

		try
		{
			var workflowSteps = Org.Workflows.WithId(Fixture.WorkflowId).Steps;

			// -- Create --
			var createResult = await workflowSteps.PostAsync(
				new WorkflowStep { Name = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			stepId = createResult.Data.Id;
			Assert.NotNull(stepId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await workflowSteps.WithId(stepId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(stepId, readResult.Data.Id);

			// -- Update --
			var updateResult = await workflowSteps.WithId(stepId).PatchAsync(
				new WorkflowStep { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await workflowSteps.WithId(stepId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await workflowSteps.WithId(stepId).DeleteAsync();
			stepId = null;
		}
		finally
		{
			if (stepId is not null)
			{
				try
				{
					await Org.Workflows.WithId(Fixture.WorkflowId).Steps
						.WithId(stepId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
