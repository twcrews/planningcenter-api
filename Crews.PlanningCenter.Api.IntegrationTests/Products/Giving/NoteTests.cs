using System.Text.Json;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class NoteTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Note_PostAndGetAsync_ReturnsNote()
	{
		string? donationId = null;
		try
		{
			var donationResult = await Org.Donations.PostAsync(
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

			Assert.NotNull(donationResult.Data);
			donationId = donationResult.Data.Id;
			Assert.NotNull(donationId);

			var noteClient = new PaginatedNoteClient(
				HttpClient,
				new Uri(new Uri(HttpClient.BaseAddress!, "giving/v2/"), $"donations/{donationId}/note/"));

			var postResult = await noteClient.PostAsync(
				new Note { Body = $"IntTest-Note-{UniqueId}" });

			Assert.NotNull(postResult.Data);
			Assert.Equal($"IntTest-Note-{UniqueId}", postResult.Data.Attributes?.Body);

			var getResult = await Org.Donations.WithId(donationId).Note.GetAsync();
			Assert.NotNull(getResult.Data);
			Assert.Equal($"IntTest-Note-{UniqueId}", getResult.Data.Attributes?.Body);
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
