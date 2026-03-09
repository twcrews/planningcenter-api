using System.Text.Json;
using Crews.PlanningCenter.Api.Groups.V2023_07_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class MembershipTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Membership_FullCrudLifecycle()
	{
		string? membershipId = null;
		var groupId = await CollectionReadHelper.GetLastIdAsync(HttpClient, "groups/v2/groups");

		try
		{
			// -- Create --
			var createResult = await Org.Groups.WithId(groupId!).Memberships.PostAsync(
				new JsonApiDocument<MembershipResource>
				{
					Data = new MembershipResource
					{
						Attributes = new Membership { Role = "member" },
						Relationships = new() { Person = new() { Data = new() { Id = Fixture.PersonId!, Type = "people" } } }
					}
				});
			Assert.NotNull(createResult.Data);
			membershipId = createResult.Data.Id;
			Assert.NotNull(membershipId);
			Assert.Equal("member", createResult.Data.Attributes?.Role);

			// -- Read --
			var readResult = await Org.Groups.WithId(groupId!).Memberships.WithId(membershipId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(membershipId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Groups.WithId(groupId!).Memberships.WithId(membershipId).PatchAsync(
				new Membership { Role = "leader" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Groups.WithId(groupId!).Memberships.WithId(membershipId).GetAsync();
			Assert.Equal("leader", verifyResult.Data?.Attributes?.Role);

			// -- Delete --
			await Org.Groups.WithId(groupId!).Memberships.WithId(membershipId).DeleteAsync();
			membershipId = null;
		}
		finally
		{
			if (membershipId is not null)
			{
				try
				{
					await Org.Groups.WithId(groupId!).Memberships.WithId(membershipId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
