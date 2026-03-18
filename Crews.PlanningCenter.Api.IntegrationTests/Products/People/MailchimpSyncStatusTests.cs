using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class MailchimpSyncStatusTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task MailchimpSyncStatus_GetAsync_ReturnsMailchimpSyncStatus()
	{
		// FIXME: This endpoint returns a 404 in testing. This may be due to improper setup.
	}
}
