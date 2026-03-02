using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class RecurringDonationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task RecurringDonation_GetAsync_ReturnsRecurringDonation()
	{
		var result = await Org.RecurringDonations.WithId(Fixture.RecurringDonationId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
