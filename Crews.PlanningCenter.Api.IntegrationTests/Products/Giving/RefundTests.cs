using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class RefundTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Refund_GetAsync_ReturnsRefund()
	{
		var donationId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "giving/v2/donations?where[refunded]=true");
		var result = await Org.Donations.WithId(donationId!).Refund.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
