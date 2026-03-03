using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class RecurringDonationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task RecurringDonation_GetAsync_ReturnsRecurringDonation()
	{
		// FIXME: Testing this seems to require a real recurring donation set up via a real payment provider.
	}
}
