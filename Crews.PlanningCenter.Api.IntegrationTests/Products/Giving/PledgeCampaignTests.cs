using System.Text.Json;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class PledgeCampaignTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	PaginatedPledgeCampaignClient PledgeCampaigns =>
		new(HttpClient, new Uri(HttpClient.BaseAddress!, "giving/v2/pledge_campaigns/"));

	[Fact]
	public async Task PledgeCampaign_FullCrudLifecycle()
	{
		string? campaignId = null;
		try
		{
			var createResult = await PledgeCampaigns.PostAsync(
				new JsonApiDocument<PledgeCampaignResource>
				{
					Data = new PledgeCampaignResource
					{
						Attributes = new PledgeCampaign 
						{
							Name = $"IntTest-Campaign-{UniqueId}",
							Description = "Pledge campaign for integration testing",
							StartsAt = DateTime.Now,
							EndsAt = DateTime.Now.AddMonths(1)
						},
						Relationships = new() { Fund = new() { Data = new() { Id = Fixture.FundId, Type = "funds" } } }
					}
				});

			Assert.NotNull(createResult.Data);
			campaignId = createResult.Data.Id;
			Assert.NotNull(campaignId);
			Assert.Equal($"IntTest-Campaign-{UniqueId}", createResult.Data.Attributes?.Name);

			var readResult = await PledgeCampaigns.WithId(campaignId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(campaignId, readResult.Data.Id);

			var updateResult = await PledgeCampaigns.WithId(campaignId).PatchAsync(
				new PledgeCampaign { Name = $"IntTest-Campaign-{UniqueId}-Updated" });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await PledgeCampaigns.WithId(campaignId).GetAsync();
			Assert.Equal($"IntTest-Campaign-{UniqueId}-Updated", verifyResult.Data?.Attributes?.Name);

			await PledgeCampaigns.WithId(campaignId).DeleteAsync();
			campaignId = null;
		}
		finally
		{
			if (campaignId is not null)
			{
				try { await PledgeCampaigns.WithId(campaignId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
