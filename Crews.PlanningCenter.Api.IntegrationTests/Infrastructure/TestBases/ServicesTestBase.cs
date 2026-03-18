using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(ServicesCollection.Name)]
[Trait("Product", "Services")]
public abstract class ServicesTestBase(ServicesFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected ServicesFixture Fixture => fixture;
	protected OrganizationClient Org => new ServicesClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
