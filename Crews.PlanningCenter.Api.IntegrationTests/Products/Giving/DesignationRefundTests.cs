using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class DesignationRefundTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task DesignationRefund_GetAsync_ReturnsDesignationRefund()
	{
		var donationId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "giving/v2/donations?where[refunded]=true");
		var refundId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"giving/v2/donations/{donationId}/refund/designation_refunds");

		var result = await Org.Donations.WithId(donationId!).Refund.DesignationRefunds.WithId(refundId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
