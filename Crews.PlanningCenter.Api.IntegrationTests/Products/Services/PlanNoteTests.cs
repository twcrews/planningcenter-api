using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PlanNoteTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task PlanNote_FullCrudLifecycle()
	{
		string? planNoteId = null;

		try
		{
			var planNoteCategoryId = await CollectionReadHelper.GetFirstIdAsync(
				HttpClient, $"services/v2/service_types/{Fixture.ServiceTypeId}/plan_note_categories");

			// -- Create --
			var createResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Notes.PostAsync(new JsonApiDocument<PlanNoteResource>
				{
					Data = new()
					{
						Attributes = new()
						{
					Content = $"IntTest-PlanNote-{UniqueId}"
						}, Relationships = new()
						{
							PlanNoteCategory = new()
							{
								Data = new()
								{
									Id = planNoteCategoryId
								}
							}
						}
					}
				});
			Assert.NotNull(createResult.Data);
			planNoteId = createResult.Data.Id;
			Assert.NotNull(planNoteId);
			Assert.Equal($"IntTest-PlanNote-{UniqueId}", createResult.Data.Attributes?.Content);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Notes.WithId(planNoteId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(planNoteId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Notes.WithId(planNoteId).PatchAsync(new PlanNote
				{
					Content = $"IntTest-PlanNote-Updated-{UniqueId}"
				});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Notes.WithId(planNoteId).GetAsync();
			Assert.Equal($"IntTest-PlanNote-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Content);

			// -- Delete --
			await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Notes.WithId(planNoteId).DeleteAsync();
			planNoteId = null;
		}
		finally
		{
			if (planNoteId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(Fixture.PlanId).Notes.WithId(planNoteId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
