using Crews.PlanningCenter.Api.Publishing.V2024_03_25;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(PublishingCollection.Name)]
[Trait("Product", "Publishing")]
public abstract class PublishingTestBase(PublishingFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected PublishingFixture Fixture => fixture;
	protected OrganizationClient Org => new PublishingClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
