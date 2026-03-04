using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ListCategoryTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ListCategory_FullCrudLifecycle()
	{
		string? listCategoryId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ListCategories.PostAsync(
				new ListCategory { Name = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			listCategoryId = createResult.Data.Id;
			Assert.NotNull(listCategoryId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.ListCategories.WithId(listCategoryId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(listCategoryId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ListCategories.WithId(listCategoryId).PatchAsync(
				new ListCategory { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ListCategories.WithId(listCategoryId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.ListCategories.WithId(listCategoryId).DeleteAsync();
			listCategoryId = null;
		}
		finally
		{
			if (listCategoryId is not null)
			{
				try
				{
					await Org.ListCategories.WithId(listCategoryId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
