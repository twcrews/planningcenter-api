using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ConnectedPersonTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ConnectedPerson_GetAsync_ReturnsConnectedPerson()
	{
		// FIXME: I have not yet discovered how to add connected people to a Person resource.
	}
}
