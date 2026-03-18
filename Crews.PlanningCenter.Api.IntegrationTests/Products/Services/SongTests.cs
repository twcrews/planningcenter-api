using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SongTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Song_FullCrudLifecycle()
	{
		string? songId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Songs.PostAsync(new Song
			{
				Title = $"IntTest-Song-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			songId = createResult.Data.Id;
			Assert.NotNull(songId);
			Assert.Equal($"IntTest-Song-{UniqueId}", createResult.Data.Attributes?.Title);

			// -- Read --
			var readResult = await Org.Songs.WithId(songId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(songId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Songs.WithId(songId).PatchAsync(
				new Song { Title = $"IntTest-Song-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Songs.WithId(songId).GetAsync();
			Assert.Equal($"IntTest-Song-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Title);

			// -- Delete --
			await Org.Songs.WithId(songId).DeleteAsync();
			songId = null;
		}
		finally
		{
			if (songId is not null)
			{
				try { await Org.Songs.WithId(songId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
