using Crews.PlanningCenter.Api.Groups.V2023_07_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(GroupsCollection.Name)]
[Trait("Product", "Groups")]
public abstract class GroupsTestBase(GroupsFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected GroupsFixture Fixture => fixture;
	protected OrganizationClient Org => new GroupsClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
