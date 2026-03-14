using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SkippedAttachmentTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SkippedAttachment_GetCollectionAsync_ReturnsCollection()
	{
		// FIXME: I cannot find an endpoint for this resource. Could be an attribute on another resource.
	}
}
