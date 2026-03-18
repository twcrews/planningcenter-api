using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PlatformNotificationTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PlatformNotification_GetAsync_ReturnsPlatformNotification()
	{
		// FIXME: I cannot figure out how to trigger a platform notification for a user.
	}
}
