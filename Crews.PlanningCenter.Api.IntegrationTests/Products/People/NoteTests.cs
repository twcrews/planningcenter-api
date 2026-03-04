using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class NoteTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Note_FullCrudLifecycle()
	{
		string? noteId = null;

		try
		{
			var personNotes = Org.People.WithId(Fixture.PersonId).Notes;

			// -- Create --
			var createResult = await personNotes.PostAsync(new Note
			{
				NoteCategoryId = Fixture.NoteCategoryId,
				NoteAttribute = $"IntTest-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			noteId = createResult.Data.Id;
			Assert.NotNull(noteId);

			// -- Read --
			var readResult = await Org.Notes.WithId(noteId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(noteId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Notes.WithId(noteId).PatchAsync(
				new Note { NoteAttribute = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Delete --
			await Org.Notes.WithId(noteId).DeleteAsync();
			noteId = null;
		}
		finally
		{
			if (noteId is not null)
			{
				try
				{
					await Org.Notes.WithId(noteId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
