using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ArrangementTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Arrangement_FullCrudLifecycle()
	{
		string? arrangementId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Songs.WithId(Fixture.SongId).Arrangements.PostAsync(
				new Arrangement { Name = $"IntTest-Arrangement-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			arrangementId = createResult.Data.Id;
			Assert.NotNull(arrangementId);
			Assert.Equal($"IntTest-Arrangement-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Songs.WithId(Fixture.SongId).Arrangements
				.WithId(arrangementId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(arrangementId, readResult.Data.Id);
			Assert.Equal($"IntTest-Arrangement-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update --
			var updateResult = await Org.Songs.WithId(Fixture.SongId).Arrangements
				.WithId(arrangementId).PatchAsync(new Arrangement
				{
					Name = $"IntTest-Arrangement-Updated-{UniqueId}"
				});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Songs.WithId(Fixture.SongId).Arrangements
				.WithId(arrangementId).GetAsync();
			Assert.Equal($"IntTest-Arrangement-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Songs.WithId(Fixture.SongId).Arrangements
				.WithId(arrangementId).DeleteAsync();
			arrangementId = null;
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
