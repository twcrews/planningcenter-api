using System.Text.Json;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ListShareTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ListShare_FullCrudLifecycle()
	{
		string? shareId = null;

		try
		{
			var listShares = Org.Lists.WithId(Fixture.ListId!).Shares;

			// -- Create --
			var createResult = await listShares.PostAsync(
				new JsonApiDocument<ListShareResource>
				{
					Data = new()
					{
						Attributes = new ListShare { Permission = "view" },
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
			var readResult = await listShares.WithId(shareId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(shareId, readResult.Data.Id);

			// -- Update --
			var updateResult = await listShares.WithId(shareId).PatchAsync(
				new ListShare { Permission = "manage" });
			Assert.NotNull(updateResult.Data);

			// -- Delete --
			await listShares.WithId(shareId).DeleteAsync();
			shareId = null;
		}
		finally
		{
			if (shareId is not null)
			{
				try
				{
					await Org.Lists.WithId(Fixture.ListId!).Shares
						.WithId(shareId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
