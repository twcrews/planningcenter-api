using Crews.Web.JsonApiClient;
using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PlanPersonTimeTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task PlanPersonTime_GetCollectionAsync_ReturnsCollection()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		string? planPersonId = null;

		try
		{
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

			var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).TeamMembers.WithId(planPersonId)
				.PlanPersonTimes.GetAsync();
			Assert.NotNull(result);
			Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
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
