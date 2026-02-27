using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(PeopleCollection.Name)]
[Trait("Product", "People")]
public abstract class PeopleTestBase(PeopleFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected PeopleFixture Fixture => fixture;
	protected OrganizationClient Org => new PeopleClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
