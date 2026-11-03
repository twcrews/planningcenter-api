using Crews.PlanningCenter.Api.People.V2026_10_05;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
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

			// The list outlives the run, so a share left by a crashed run would make the create below fail.
			await TestDataSweeper.DeleteWhereAsync(
				HttpClient,
				$"people/v2/lists/{Fixture.ListId}/shares",
				share => share["attributes"]?["group"]?.GetValue<string>() == "Viewer");

			// -- Create --
			var createResult = await listShares.PostAsync(
				new JsonApiDocument<ListShareResource>
				{
					Data = new()
					{
						// Shares can only target people with People permissions, which the fixture person can't have
						// because it has no email address. Share with a permission group instead.
						Attributes = new ListShare { Group = "Viewer", Permission = "view" }
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
