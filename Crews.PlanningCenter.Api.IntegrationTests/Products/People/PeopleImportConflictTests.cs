using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PeopleImportConflictTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PeopleImportConflict_GetAsync_ReturnsPeopleImportConflict()
	{
		// FIXME: I cannot figure out how to trigger a conflict response from the API.
	}
}
