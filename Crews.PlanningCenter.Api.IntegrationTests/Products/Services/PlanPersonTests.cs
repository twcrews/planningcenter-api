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

			// -- Create --
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
								Team = new()
								{
									Data = new() { Id = teamId }
								}
							}
						}
					});
			Assert.NotNull(createResult.Data);
			planPersonId = createResult.Data.Id;
			Assert.NotNull(planPersonId);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(serviceTypeId!)
				.Plans.WithId(planId!).TeamMembers.WithId(planPersonId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(planPersonId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(serviceTypeId!)
				.Plans.WithId(planId!).TeamMembers.WithId(planPersonId)
				.PatchAsync(new PlanPerson { Notes = $"IntTest-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(serviceTypeId!)
				.Plans.WithId(planId!).TeamMembers.WithId(planPersonId).GetAsync();
			Assert.Equal($"IntTest-{UniqueId}", verifyResult.Data?.Attributes?.Notes);

			// -- Delete --
			await Org.ServiceTypes.WithId(serviceTypeId!)
				.Plans.WithId(planId!).TeamMembers.WithId(planPersonId).DeleteAsync();
			planPersonId = null;
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
