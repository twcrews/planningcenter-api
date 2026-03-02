using System.Text.Json;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class DonationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Donation_FullCrudLifecycle()
	{
		string? donationId = null;
		try
		{
			var createResult = await Org.Donations.PostAsync(
				new JsonApiDocument<DonationResource>
				{
					Data = new DonationResource
					{
						Attributes = new Donation
						{
							PaymentMethod = "cash",
							AmountCents = 100
						},
						Relationships = new DonationRelationships
						{
							Batch = new JsonApiRelationship
							{
								Data = JsonSerializer.SerializeToElement(
									new JsonApiResourceIdentifier { Id = Fixture.BatchId, Type = "batches" })
							},
							PaymentSource = new JsonApiRelationship
							{
								Data = JsonSerializer.SerializeToElement(
									new JsonApiResourceIdentifier { Id = Fixture.PaymentSourceId, Type = "payment_sources" })
							}
						}
					}
				});

			Assert.NotNull(createResult.Data);
			donationId = createResult.Data.Id;
			Assert.NotNull(donationId);
			Assert.Equal(100, createResult.Data.Attributes?.AmountCents);

			var readResult = await Org.Donations.WithId(donationId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(donationId, readResult.Data.Id);

			var updateResult = await Org.Donations.WithId(donationId).PatchAsync(
				new Donation { AmountCents = 200 });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.Donations.WithId(donationId).GetAsync();
			Assert.Equal(200, verifyResult.Data?.Attributes?.AmountCents);

			await Org.Donations.WithId(donationId).DeleteAsync();
			donationId = null;
		}
		finally
		{
			if (donationId is not null)
			{
				try { await Org.Donations.WithId(donationId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
