using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SongScheduleTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SongSchedule_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.Songs.WithId(Fixture.SongId).SongSchedules.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
