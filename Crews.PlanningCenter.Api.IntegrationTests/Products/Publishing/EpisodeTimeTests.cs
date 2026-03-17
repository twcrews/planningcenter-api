using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class EpisodeTimeTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task EpisodeTime_FullCrudLifecycle()
	{
		// FIXME: Episodes require a paid subscription.
	}
}
