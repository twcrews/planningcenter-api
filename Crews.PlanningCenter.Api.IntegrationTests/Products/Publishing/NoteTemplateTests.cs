using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Publishing;

public class NoteTemplateTests(PublishingFixture fixture) : PublishingTestBase(fixture)
{
	[Fact]
	public async Task NoteTemplate_GetAndPatch()
	{
		// FIXME: Episodes require a paid subscription.
	}
}
