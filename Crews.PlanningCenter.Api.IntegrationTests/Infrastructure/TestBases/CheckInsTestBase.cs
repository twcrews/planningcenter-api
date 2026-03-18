using Crews.PlanningCenter.Api.CheckIns.V2025_05_28;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(CheckInsCollection.Name)]
[Trait("Product", "CheckIns")]
public abstract class CheckInsTestBase(CheckInsFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected CheckInsFixture Fixture => fixture;
	protected OrganizationClient Org => new CheckInsClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
