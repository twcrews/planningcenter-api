using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ListStarTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ListStar_GetPostDelete()
	{
		Skip.If(Fixture.ListId is null, "No list data available for list star tests.");

		bool starred = false;

		try
		{
			// Ensure the list is not starred before starting
			try
			{
				await Org.Lists.WithId(Fixture.ListId!).Star.DeleteAsync();
			}
			catch { /* may not be starred */ }

			// -- Star (POST via collection client) --
			var starClient = new PaginatedListStarClient(
				HttpClient,
				new Uri(HttpClient.BaseAddress!, $"people/v2/lists/{Fixture.ListId}/star/"));
			var createResult = await starClient.PostAsync(new ListStar());
			Assert.NotNull(createResult.Data);
			starred = true;

			// -- Read --
			var readResult = await Org.Lists.WithId(Fixture.ListId!).Star.GetAsync();
			Assert.NotNull(readResult.Data);

			// -- Unstar (DELETE) --
			await Org.Lists.WithId(Fixture.ListId!).Star.DeleteAsync();
			starred = false;
		}
		finally
		{
			if (starred)
			{
				try
				{
					await Org.Lists.WithId(Fixture.ListId!).Star.DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
