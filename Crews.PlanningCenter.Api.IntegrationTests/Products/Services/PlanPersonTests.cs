using Crews.Web.JsonApiClient;
using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PlanPersonTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task PlanPerson_FullCrudLifecycle()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		string? planPersonId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).TeamMembers.PostAsync(
					new JsonApiDocument<PlanPersonResource>
					{
						Data = new()
						{
							Attributes = new PlanPerson { Status = "U" },
							Relationships = new PlanPersonRelationships
							{
								Person = new JsonApiRelationship<PersonResource>
								{
									Data = new PersonResource { Id = personId }
								}
							}
						}
					});
			Assert.NotNull(createResult.Data);
			planPersonId = createResult.Data.Id;
			Assert.NotNull(planPersonId);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).TeamMembers.WithId(planPersonId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(planPersonId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).TeamMembers.WithId(planPersonId)
				.PatchAsync(new PlanPerson { Notes = $"IntTest-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).TeamMembers.WithId(planPersonId).GetAsync();
			Assert.Equal($"IntTest-{UniqueId}", verifyResult.Data?.Attributes?.Notes);

			// -- Delete --
			await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).TeamMembers.WithId(planPersonId).DeleteAsync();
			planPersonId = null;
		}
		finally
		{
			if (planPersonId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(Fixture.PlanId).TeamMembers.WithId(planPersonId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
