using Crews.PlanningCenter.Api.People.V2026_10_05;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowCardNoteTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowCardNote_GetAndPost()
	{
		string? cardId = null;
		var workflowCards = Org.Workflows.WithId(Fixture.WorkflowId).Cards;

		try
		{
			// The API can't delete notes, so they're only removed along with a card this test owns.
			var cardResult = await workflowCards.PostAsync(
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
			Assert.NotNull(cardResult.Data);
			cardId = cardResult.Data.Id;
			Assert.NotNull(cardId);

			// -- Create note --
			var cardNotes = workflowCards.WithId(cardId).Notes;
			var createResult = await cardNotes.PostAsync(
				new WorkflowCardNote { Note = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			var noteId = createResult.Data.Id;
			Assert.NotNull(noteId);

			// -- Read --
			var readResult = await cardNotes.WithId(noteId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(noteId, readResult.Data.Id);
		}
		finally
		{
			if (cardId is not null)
			{
				try
				{
					await workflowCards.WithId(cardId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
