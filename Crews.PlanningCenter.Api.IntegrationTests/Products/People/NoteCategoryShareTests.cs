using System.Text.Json;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class NoteCategoryShareTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task NoteCategoryShare_FullCrudLifecycle()
	{
		string? shareId = null;

		try
		{
			var noteCategoryShares = Org.NoteCategories.WithId(Fixture.NoteCategoryId).Shares;

			// -- Create --
			var createResult = await noteCategoryShares.PostAsync(
				new JsonApiDocument<NoteCategoryShareResource>
				{
					Data = new()
					{
						Attributes = new NoteCategoryShare { Permission = "view" },
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
			var readResult = await noteCategoryShares.WithId(shareId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(shareId, readResult.Data.Id);

			// -- Update --
			var updateResult = await noteCategoryShares.WithId(shareId).PatchAsync(
				new NoteCategoryShare { Permission = "view_create" });
			Assert.NotNull(updateResult.Data);

			// -- Delete --
			await noteCategoryShares.WithId(shareId).DeleteAsync();
			shareId = null;
		}
		finally
		{
			if (shareId is not null)
			{
				try
				{
					await Org.NoteCategories.WithId(Fixture.NoteCategoryId).Shares
						.WithId(shareId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
