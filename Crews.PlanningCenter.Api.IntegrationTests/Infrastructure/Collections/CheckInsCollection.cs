using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class CheckInsCollection : ICollectionFixture<CheckInsFixture>
{
	public const string Name = "CheckIns";
}
