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
		string? serviceTypeId = null;
		string? planId = null;

		try
		{
			serviceTypeId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"services/v2/service_types");
			var teamId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"services/v2/service_types/{serviceTypeId}/teams");
			planId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"services/v2/service_types/{serviceTypeId}/plans");

			// We need to make sure the assignment doesn't already exist.
			try
			{
				var teamMemberId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"services/v2/service_types/{serviceTypeId}/plans/{planId}/team_members");
				await Org.ServiceTypes.WithId(serviceTypeId!).Plans.WithId(planId!).TeamMembers.WithId(teamMemberId!).DeleteAsync();
			}
			catch (System.Exception)
			{ /* best effort */ }
			var createResult = await Org.ServiceTypes.WithId(serviceTypeId!)
				.Plans.WithId(planId!).TeamMembers.PostAsync(
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
								},
								Team = new() { Data = new() { Id = teamId }}
							}
						}
					});
			Assert.NotNull(createResult.Data);
			planPersonId = createResult.Data.Id;
			Assert.NotNull(planPersonId);

			var result = await Org.ServiceTypes.WithId(serviceTypeId!)
				.Plans.WithId(planId!).TeamMembers.WithId(planPersonId)
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
					await Org.ServiceTypes.WithId(serviceTypeId!)
						.Plans.WithId(planId!).TeamMembers.WithId(planPersonId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
