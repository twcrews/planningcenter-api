using Crews.PlanningCenter.Api.Current.V2018_08_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(CurrentCollection.Name)]
[Trait("Product", "Current")]
public abstract class CurrentTestBase(CurrentFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected CurrentFixture Fixture => fixture;
	protected OrganizationClient Org => new CurrentClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
