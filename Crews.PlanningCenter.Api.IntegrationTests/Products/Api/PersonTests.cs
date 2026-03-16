using Crews.PlanningCenter.Api.Api.V2025_09_30;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Api;

public class PersonTests(ApiFixture fixture) : ApiTestBase(fixture)
{
	[Fact]
	public async Task Person_GetAsync_ReturnsPerson()
	{
		// FIXME: I can't find an endpoint for this resource.
	}
}
