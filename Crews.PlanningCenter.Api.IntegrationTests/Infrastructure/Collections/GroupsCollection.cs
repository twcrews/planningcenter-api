using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class GroupsCollection : ICollectionFixture<GroupsFixture>
{
	public const string Name = "Groups";
}
