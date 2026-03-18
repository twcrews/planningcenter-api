using Crews.PlanningCenter.Api.Registrations.V2025_05_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(RegistrationsCollection.Name)]
[Trait("Product", "Registrations")]
public abstract class RegistrationsTestBase(RegistrationsFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected RegistrationsFixture Fixture => fixture;
	protected OrganizationClient Org => new RegistrationsClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
