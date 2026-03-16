using Crews.PlanningCenter.Api.Webhooks.V2022_10_20;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(WebhooksCollection.Name)]
[Trait("Product", "Webhooks")]
public abstract class WebhooksTestBase(WebhooksFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected WebhooksFixture Fixture => fixture;
	protected OrganizationClient Org => new WebhooksClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
