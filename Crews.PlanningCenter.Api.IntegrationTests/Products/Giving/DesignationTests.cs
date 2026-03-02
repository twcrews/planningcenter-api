using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class DesignationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Designation_GetAsync_ReturnsDesignation()
	{
		var donationId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "giving/v2/donations");
		var designationId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"giving/v2/donations/{donationId}/designations");
		var result = await Org.Donations.WithId(donationId!).Designations.WithId(designationId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
