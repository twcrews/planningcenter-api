using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class WorkflowCardNoteTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task WorkflowCardNote_GetAndPost()
	{
		var cardId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/workflows/{Fixture.WorkflowId}/cards");

		// -- Create note --
		var cardNotes = Org.Workflows.WithId(Fixture.WorkflowId).Cards
			.WithId(cardId!).Notes;
		var createResult = await cardNotes.PostAsync(
			new WorkflowCardNote { Note = $"IntTest-{UniqueId}" });
		Assert.NotNull(createResult.Data);
		var noteId = createResult.Data.Id;
		Assert.NotNull(noteId);

		// -- Read --
		var readResult = await cardNotes.WithId(noteId!).GetAsync();
		Assert.NotNull(readResult.Data);
		Assert.Equal(noteId, readResult.Data.Id);
	}
}
