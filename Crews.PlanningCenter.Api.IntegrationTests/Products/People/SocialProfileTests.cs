using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class SocialProfileTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task SocialProfile_FullCrudLifecycle()
	{
		string? socialProfileId = null;

		try
		{
			var personSocialProfiles = Org.People.WithId(Fixture.PersonId).SocialProfiles;

			// -- Create --
			var createResult = await personSocialProfiles.PostAsync(new SocialProfile
			{
				Site = "twitter",
				Url = $"https://twitter.com/inttest{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			socialProfileId = createResult.Data.Id;
			Assert.NotNull(socialProfileId);

			// -- Read --
			var readResult = await Org.People.WithId(Fixture.PersonId).SocialProfiles
				.WithId(socialProfileId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(socialProfileId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.People.WithId(Fixture.PersonId).SocialProfiles
				.WithId(socialProfileId).PatchAsync(
					new SocialProfile { Url = $"https://twitter.com/updated{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Delete --
			await Org.People.WithId(Fixture.PersonId).SocialProfiles
				.WithId(socialProfileId).DeleteAsync();
			socialProfileId = null;
		}
		finally
		{
			if (socialProfileId is not null)
			{
				try
				{
					await Org.People.WithId(Fixture.PersonId).SocialProfiles
						.WithId(socialProfileId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
