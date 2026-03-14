using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class KeyTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Key_GetCollectionAsync_ReturnsCollection()
	{
		string? arrangementId = null;

		try
		{
			var arrangementResult = await Org.Songs.WithId(Fixture.SongId).Arrangements
				.PostAsync(new Arrangement { Name = $"IntTest-Arrangement-{UniqueId}" });
			Assert.NotNull(arrangementResult.Data);
			arrangementId = arrangementResult.Data.Id;
			Assert.NotNull(arrangementId);

			var result = await Org.Songs.WithId(Fixture.SongId).Arrangements
				.WithId(arrangementId).Keys.GetAsync();
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
