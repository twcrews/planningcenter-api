using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class CurrentCollection : ICollectionFixture<CurrentFixture>
{
	public const string Name = "Current";
}
