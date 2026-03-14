using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class SpeakershipTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task Speakership_FullCrudLifecycle()
	{
		// FIXME: Speakers cannot be created via the API and the Sermons feature is paid.
	}
}
