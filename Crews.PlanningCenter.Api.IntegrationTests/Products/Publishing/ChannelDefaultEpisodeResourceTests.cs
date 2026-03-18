using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class ChannelDefaultEpisodeResourceTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task ChannelDefaultEpisodeResource_GetAsync_ReturnsCollection()
	{
		// FIXME: The Sermons feature is paid.
	}
}
