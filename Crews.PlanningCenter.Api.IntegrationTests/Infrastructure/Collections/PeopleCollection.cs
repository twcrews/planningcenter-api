using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class PeopleCollection : ICollectionFixture<PeopleFixture>
{
	public const string Name = "People";
}
