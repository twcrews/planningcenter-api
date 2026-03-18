using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowCardTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowCard_FullCrudLifecycle()
	{
		string? cardId = null;

		try
		{
			var workflowCards = Org.Workflows.WithId(Fixture.WorkflowId).Cards;

			// -- Create --
			var createResult = await workflowCards.PostAsync(
				new JsonApiDocument<WorkflowCardResource>
				{
					Data = new()
					{
						Attributes = new WorkflowCard { StickyAssignment = false },
						Relationships = new()
						{
							Person = new()
							{
								Data = new() { Type = "Person", Id = Fixture.PersonId }
							}
						}
					}
				});
			Assert.NotNull(createResult.Data);
			cardId = createResult.Data.Id;
			Assert.NotNull(cardId);

			// -- Read --
			var readResult = await workflowCards.WithId(cardId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(cardId, readResult.Data.Id);

			// -- Update --
			var updateResult = await workflowCards.WithId(cardId).PatchAsync(
				new WorkflowCard { StickyAssignment = true });
			Assert.NotNull(updateResult.Data);

			// -- Delete --
			await workflowCards.WithId(cardId).DeleteAsync();
			cardId = null;
		}
		finally
		{
			if (cardId is not null)
			{
				try
				{
					await Org.Workflows.WithId(Fixture.WorkflowId).Cards
						.WithId(cardId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
