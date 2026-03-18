using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class EpisodeStatisticsTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task EpisodeStatistics_GetAsync_ReturnsCollection()
	{
		// FIXME: Episodes require a paid subscription.
	}
}
