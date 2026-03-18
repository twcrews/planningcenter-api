using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class PublishingCollection : ICollectionFixture<PublishingFixture>
{
	public const string Name = "Publishing";
}
