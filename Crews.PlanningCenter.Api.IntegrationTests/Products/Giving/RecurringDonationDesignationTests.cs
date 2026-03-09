using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class RecurringDonationDesignationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task RecurringDonationDesignation_GetAsync_ReturnsDesignation()
	{
		// FIXME: Testing this seems to require a real recurring donation set up via a real payment provider.
	}
}
