using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class MediaScheduleTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task MediaSchedule_GetCollectionAsync_ReturnsCollection()
	{
		var mediaId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/media");
		Assert.NotNull(mediaId);

		var result = await Org.Media.WithId(mediaId).MediaSchedules.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
