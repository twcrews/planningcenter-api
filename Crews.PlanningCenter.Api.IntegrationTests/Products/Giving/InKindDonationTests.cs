using System.Text.Json;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class InKindDonationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task InKindDonation_FullCrudLifecycle()
	{
		string? inKindDonationId = null;
		try
		{
			var createResult = await Org.InKindDonations.PostAsync(
				new JsonApiDocument<InKindDonationResource>
				{
					Data = new InKindDonationResource
					{
						Attributes = new InKindDonation
						{
							Description = $"IntTest-InKind-{UniqueId}",
							ReceivedOn = DateOnly.FromDateTime(DateTime.Today)
						},
						Relationships = new InKindDonationRelationships
						{
							Fund = new JsonApiRelationship
							{
								Data = JsonSerializer.SerializeToElement(
									new JsonApiResourceIdentifier { Id = Fixture.FundId, Type = "funds" })
							},
							Person = new JsonApiRelationship
							{
								Data = JsonSerializer.SerializeToElement(
									new JsonApiResourceIdentifier { Id = Fixture.PersonId, Type = "people" })
							}
						}
					}
				});

			Assert.NotNull(createResult.Data);
			inKindDonationId = createResult.Data.Id;
			Assert.NotNull(inKindDonationId);
			Assert.Equal($"IntTest-InKind-{UniqueId}", createResult.Data.Attributes?.Description);

			var readResult = await Org.InKindDonations.WithId(inKindDonationId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(inKindDonationId, readResult.Data.Id);

			var updateResult = await Org.InKindDonations.WithId(inKindDonationId).PatchAsync(
				new InKindDonation { Description = $"IntTest-InKind-{UniqueId}-Updated" });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.InKindDonations.WithId(inKindDonationId).GetAsync();
			Assert.Equal($"IntTest-InKind-{UniqueId}-Updated", verifyResult.Data?.Attributes?.Description);

			await Org.InKindDonations.WithId(inKindDonationId).DeleteAsync();
			inKindDonationId = null;
		}
		finally
		{
			if (inKindDonationId is not null)
			{
				try { await Org.InKindDonations.WithId(inKindDonationId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
