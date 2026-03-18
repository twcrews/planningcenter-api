using Crews.PlanningCenter.Api.Api.V2025_09_30;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(ApiCollection.Name)]
[Trait("Product", "Api")]
public abstract class ApiTestBase(ApiFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected ApiFixture Fixture => fixture;
	protected OrganizationClient Org => new ApiClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
