using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class FeedTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Feed_GetAsync_ReturnsFeed()
	{
		var feedId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/feeds");
		var result = await Org.Feeds.WithId(feedId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
