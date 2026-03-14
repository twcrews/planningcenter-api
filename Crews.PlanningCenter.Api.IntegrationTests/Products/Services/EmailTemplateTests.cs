using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class EmailTemplateTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task EmailTemplate_FullCrudLifecycle()
	{
		// FIXME: Email templates seem to POST proeprly and return a 200 with a valid ID, but attempting to read the
		// same template later returns a 404. Checking a list of all templates, it appears it was never created.	
	}
}
