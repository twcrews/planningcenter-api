using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class ChannelNextTimeTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task ChannelNextTime_GetAsync_ReturnsCollection()
	{
		// FIXME: Channels require a paid subscription.
	}
}
