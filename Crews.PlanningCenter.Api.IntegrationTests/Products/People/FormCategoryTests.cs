using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FormCategoryTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FormCategory_FullCrudLifecycle()
	{
		string? formCategoryId = null;

		try
		{
			// -- Create --
			var createResult = await Org.FormCategories.PostAsync(new FormCategory
			{
				Name = $"IntTest-FormCat-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			formCategoryId = createResult.Data.Id;
			Assert.NotNull(formCategoryId);
			Assert.Equal($"IntTest-FormCat-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.FormCategories.WithId(formCategoryId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(formCategoryId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.FormCategories.WithId(formCategoryId).PatchAsync(
				new FormCategory { Name = $"IntTest-FormCat-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.FormCategories.WithId(formCategoryId).GetAsync();
			Assert.Equal($"IntTest-FormCat-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.FormCategories.WithId(formCategoryId).DeleteAsync();
			formCategoryId = null;
		}
		finally
		{
			if (formCategoryId is not null)
			{
				try
				{
					await Org.FormCategories.WithId(formCategoryId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
