using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class WebhooksCollection : ICollectionFixture<WebhooksFixture>
{
	public const string Name = "Webhooks";
}
