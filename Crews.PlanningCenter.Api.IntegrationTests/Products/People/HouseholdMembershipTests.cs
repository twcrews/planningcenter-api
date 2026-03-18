using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class HouseholdMembershipTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task HouseholdMembership_FullCrudLifecycle()
	{
		string? tempPersonId = null;
		string? membershipId = null;

		try
		{
			// Create a temporary person to add to the fixture household
			var personResult = await Org.People.PostAsync(new Person
			{
				FirstName = "TempMember",
				LastName = $"IntTest-{UniqueId}"
			});
			Assert.NotNull(personResult.Data);
			tempPersonId = personResult.Data.Id;
			Assert.NotNull(tempPersonId);

			// -- Create membership --
			var createResult = await Org.Households.WithId(Fixture.HouseholdId).HouseholdMemberships
				.PostAsync(new JsonApiDocument<HouseholdMembershipResource>
				{
					Data = new()
					{
						Attributes = new HouseholdMembership { HouseholdRole = "adult" },
						Relationships = new() { Person = new() { Data = new() { Type = "Person", Id = tempPersonId } } }
					}
				});
			Assert.NotNull(createResult.Data);
			membershipId = createResult.Data.Id;
			Assert.NotNull(membershipId);

			// -- Read --
			var readResult = await Org.Households.WithId(Fixture.HouseholdId)
				.HouseholdMemberships.WithId(membershipId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(membershipId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Households.WithId(Fixture.HouseholdId)
				.HouseholdMemberships.WithId(membershipId).PatchAsync(
					new HouseholdMembership { HouseholdRole = "child_or_dependent" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Households.WithId(Fixture.HouseholdId)
				.HouseholdMemberships.WithId(membershipId).GetAsync();
			Assert.Equal("child_or_dependent", verifyResult.Data?.Attributes?.HouseholdRole);

			// -- Delete --
			await Org.Households.WithId(Fixture.HouseholdId)
				.HouseholdMemberships.WithId(membershipId).DeleteAsync();
			membershipId = null;
		}
		finally
		{
			if (membershipId is not null)
			{
				try
				{
					await Org.Households.WithId(Fixture.HouseholdId)
						.HouseholdMemberships.WithId(membershipId).DeleteAsync();
				}
				catch { /* best effort */ }
			}

			if (tempPersonId is not null)
			{
				try
				{
					await Org.People.WithId(tempPersonId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
