using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class ServicesCollection : ICollectionFixture<ServicesFixture>
{
	public const string Name = "Services";
}
