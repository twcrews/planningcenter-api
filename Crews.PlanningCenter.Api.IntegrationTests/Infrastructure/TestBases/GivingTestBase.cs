using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(GivingCollection.Name)]
[Trait("Product", "Giving")]
public abstract class GivingTestBase(GivingFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected GivingFixture Fixture => fixture;
	protected OrganizationClient Org => new GivingClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
