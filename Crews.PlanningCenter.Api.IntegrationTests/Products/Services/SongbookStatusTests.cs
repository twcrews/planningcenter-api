using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SongbookStatusTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SongbookStatus_GetCollectionAsync_ReturnsCollection()
	{
		// FIXME: I cannot find an endpoint for this resource. It may be an attribute on another resource.
	}
}
