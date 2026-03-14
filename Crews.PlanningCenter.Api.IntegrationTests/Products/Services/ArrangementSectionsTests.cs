using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ArrangementSectionsTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ArrangementSections_GetAsync_ReturnsSections()
	{
		string? arrangementId = null;

		try
		{
			// Create an arrangement with a chord chart so it has sections
			var createResult = await Org.Songs.WithId(Fixture.SongId).Arrangements.PostAsync(
				new Arrangement
				{
					Name = $"IntTest-Arrangement-{UniqueId}",
					ChordChart = "VERSE 1\nD G\nTest lyrics",
					ChordChartKey = "C"
				});
			arrangementId = createResult.Data!.Id!;

			var result = await Org.Songs.WithId(Fixture.SongId).Arrangements
				.WithId(arrangementId).Sections.GetAsync();
			Assert.NotNull(result);
			Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
		}
		finally
		{
			if (arrangementId is not null)
			{
				try
				{
					await Org.Songs.WithId(Fixture.SongId).Arrangements
						.WithId(arrangementId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
