using System.Text.Json;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowShareTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowShare_FullCrudLifecycle()
	{
		string? shareId = null;

		try
		{
			var workflowShares = Org.Workflows.WithId(Fixture.WorkflowId).Shares;

			// -- Create --
			var createResult = await workflowShares.PostAsync(
				new JsonApiDocument<WorkflowShareResource>
				{
					Data = new()
					{
						Attributes = new WorkflowShare { Permission = "view" },
						Relationships = new()
						{
							Person = new()
							{
								Data = JsonSerializer.SerializeToElement<JsonApiResourceIdentifier>(
									new() { Type = "Person", Id = Fixture.PersonId })
							}
						}
					}
				});
			Assert.NotNull(createResult.Data);
			shareId = createResult.Data.Id;
			Assert.NotNull(shareId);

			// -- Read --
			var readResult = await workflowShares.WithId(shareId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(shareId, readResult.Data.Id);

			// -- Update --
			var updateResult = await workflowShares.WithId(shareId).PatchAsync(
				new WorkflowShare { Permission = "manage_cards" });
			Assert.NotNull(updateResult.Data);

			// -- Delete --
			await workflowShares.WithId(shareId).DeleteAsync();
			shareId = null;
		}
		finally
		{
			if (shareId is not null)
			{
				try
				{
					await Org.Workflows.WithId(Fixture.WorkflowId).Shares
						.WithId(shareId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
