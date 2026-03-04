using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class NoteCategoryTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task NoteCategory_FullCrudLifecycle()
	{
		string? noteCategoryId = null;

		try
		{
			// -- Create --
			var createResult = await Org.NoteCategories.PostAsync(
				new NoteCategory { Name = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			noteCategoryId = createResult.Data.Id;
			Assert.NotNull(noteCategoryId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.NoteCategories.WithId(noteCategoryId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(noteCategoryId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.NoteCategories.WithId(noteCategoryId).PatchAsync(
				new NoteCategory { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.NoteCategories.WithId(noteCategoryId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.NoteCategories.WithId(noteCategoryId).DeleteAsync();
			noteCategoryId = null;
		}
		finally
		{
			if (noteCategoryId is not null)
			{
				try
				{
					await Org.NoteCategories.WithId(noteCategoryId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
