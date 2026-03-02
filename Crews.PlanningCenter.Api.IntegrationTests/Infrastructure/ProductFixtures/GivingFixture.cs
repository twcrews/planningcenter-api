using System.Text.Json;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Giving product integration tests.
/// Pre-creates shared parent resources needed by child resource tests.
/// </summary>
public class GivingFixture : PlanningCenterFixture
{
	string _fixtureId = null!;

	/// <summary>ID of a pre-created Fund for Designation and other tests.</summary>
	public string FundId { get; private set; } = null!;

	/// <summary>ID of a pre-created Batch for Donation tests.</summary>
	public string BatchId { get; private set; } = null!;

	/// <summary>ID of a pre-created PaymentSource for Donation tests.</summary>
	public string PaymentSourceId { get; private set; } = null!;

	/// <summary>ID of an existing Person, or null if none exist.</summary>
	public string PersonId { get; private set; } = null!;

	/// <summary>ID of a pre-created PledgeCampaign for Pledge tests.</summary>
	public string PledgeCampaignId { get; private set; } = null!;

	/// <summary>ID of an existing RecurringDonation, or null if none exist.</summary>
	public string RecurringDonationId { get; private set; } = null!;

	/// <summary>ID of an existing Campus, or null if none exist.</summary>
	public string CampusId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();
		_fixtureId = Guid.NewGuid().ToString("N")[..8];

		var org = new GivingClient(HttpClient).Latest;

		var fundResult = await org.Funds.PostAsync(
			new Fund { Name = $"Fixture-Fund-{_fixtureId}" });
		FundId = fundResult.Data!.Id!;

		var batchResult = await org.Batches.PostAsync(
			new Batch { Description = $"Fixture-Batch-{_fixtureId}" });
		BatchId = batchResult.Data!.Id!;

		var paymentSourceResult = await org.PaymentSources.PostAsync(
			new PaymentSource { Name = $"Fixture-Source-{_fixtureId}" });
		PaymentSourceId = paymentSourceResult.Data!.Id!;

		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "giving/v2/people");
		PersonId = personId!;

		var pledgeCampaignsClient = new PaginatedPledgeCampaignClient(
			HttpClient, new Uri(HttpClient.BaseAddress!, "giving/v2/pledge_campaigns/"));
		var campaignResult = await pledgeCampaignsClient.PostAsync(
			new JsonApiDocument<PledgeCampaignResource>
			{
				Data = new PledgeCampaignResource
				{
					Attributes = new PledgeCampaign { Name = $"Fixture-Campaign-{_fixtureId}" },
					Relationships = new PledgeCampaignRelationships
					{
						Fund = new JsonApiRelationship
						{
							Data = JsonSerializer.SerializeToElement(
								new JsonApiResourceIdentifier { Id = FundId, Type = "funds" })
						}
					}
				}
			});
		PledgeCampaignId = campaignResult.Data!.Id!;

		var recurringDonationId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "giving/v2/recurring_donations");
		RecurringDonationId = recurringDonationId!;

		var campusId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "giving/v2/campuses");
		CampusId = campusId!;
	}

	public override async Task DisposeAsync()
	{
		var org = new GivingClient(HttpClient).Latest;

		var pledgeCampaignsClient = new PaginatedPledgeCampaignClient(
			HttpClient, new Uri(HttpClient.BaseAddress!, "giving/v2/pledge_campaigns/"));

		try { await pledgeCampaignsClient.WithId(PledgeCampaignId).DeleteAsync(); } catch { }
		try { await org.PaymentSources.WithId(PaymentSourceId).DeleteAsync(); } catch { }
		try { await org.Batches.WithId(BatchId).DeleteAsync(); } catch { }
		try { await org.Funds.WithId(FundId).DeleteAsync(); } catch { }

		await base.DisposeAsync();
	}
}
