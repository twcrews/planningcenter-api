using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class RecurringDonationDesignationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task RecurringDonationDesignation_GetAsync_ReturnsDesignation()
	{
		var designationId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"giving/v2/recurring_donations/{Fixture.RecurringDonationId}/designations");
		var result = await Org.RecurringDonations.WithId(Fixture.RecurringDonationId).Designations.WithId(designationId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
