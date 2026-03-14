using Crews.PlanningCenter.Api.Publishing.V2024_03_25;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class ChannelTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task Channel_FullCrudLifecycle()
	{
		// FIXME: Channels require a paid subscription.
	}
}
