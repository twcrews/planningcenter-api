using System.Text.Json;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class PledgeTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	PaginatedPledgeCampaignClient PledgeCampaigns =>
		new(HttpClient, new Uri(HttpClient.BaseAddress!, "giving/v2/pledge_campaigns/"));

	[Fact]
	public async Task Pledge_FullCrudLifecycle()
	{
		string? pledgeId = null;
		try
		{
			var createResult = await PledgeCampaigns.WithId(Fixture.PledgeCampaignId).Pledges.PostAsync(
				new JsonApiDocument<PledgeResource>
				{
					Data = new PledgeResource
					{
						Attributes = new Pledge { AmountCents = 1000 },
						Relationships = new PledgeRelationships
						{
							Person = new JsonApiRelationship
							{
								Data = JsonSerializer.SerializeToElement(
									new JsonApiResourceIdentifier { Id = Fixture.PersonId, Type = "people" })
							},
							PledgeCampaign = new JsonApiRelationship
							{
								Data = JsonSerializer.SerializeToElement(
									new JsonApiResourceIdentifier { Id = Fixture.PledgeCampaignId, Type = "pledge_campaigns" })
							}
						}
					}
				});

			Assert.NotNull(createResult.Data);
			pledgeId = createResult.Data.Id;
			Assert.NotNull(pledgeId);
			Assert.Equal(1000, createResult.Data.Attributes?.AmountCents);

			var readResult = await PledgeCampaigns.WithId(Fixture.PledgeCampaignId).Pledges.WithId(pledgeId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(pledgeId, readResult.Data.Id);

			var updateResult = await PledgeCampaigns.WithId(Fixture.PledgeCampaignId).Pledges.WithId(pledgeId).PatchAsync(
				new Pledge { AmountCents = 2000 });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await PledgeCampaigns.WithId(Fixture.PledgeCampaignId).Pledges.WithId(pledgeId).GetAsync();
			Assert.Equal(2000, verifyResult.Data?.Attributes?.AmountCents);

			await PledgeCampaigns.WithId(Fixture.PledgeCampaignId).Pledges.WithId(pledgeId).DeleteAsync();
			pledgeId = null;
		}
		finally
		{
			if (pledgeId is not null)
			{
				try { await PledgeCampaigns.WithId(Fixture.PledgeCampaignId).Pledges.WithId(pledgeId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
