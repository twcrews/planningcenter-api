using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class SpeakerTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task Speaker_GetAsync_ReturnsSpeaker()
	{
		// FIXME: Speakers require a paid subscription.
	}
}
