using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PhoneNumberTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PhoneNumber_FullCrudLifecycle()
	{
		string? phoneNumberId = null;

		try
		{
			var personPhones = Org.People.WithId(Fixture.PersonId).PhoneNumbers;

			// -- Create --
			var createResult = await personPhones.PostAsync(new PhoneNumber
			{
				Number = "5555550100",
				Location = "Mobile"
			});
			Assert.NotNull(createResult.Data);
			phoneNumberId = createResult.Data.Id;
			Assert.NotNull(phoneNumberId);

			// -- Read --
			var readResult = await Org.People.WithId(Fixture.PersonId).PhoneNumbers
				.WithId(phoneNumberId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(phoneNumberId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.People.WithId(Fixture.PersonId).PhoneNumbers
				.WithId(phoneNumberId).PatchAsync(new PhoneNumber { Location = "Home" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.People.WithId(Fixture.PersonId).PhoneNumbers
				.WithId(phoneNumberId).GetAsync();
			Assert.Equal("Home", verifyResult.Data?.Attributes?.Location);

			// -- Delete --
			await Org.People.WithId(Fixture.PersonId).PhoneNumbers
				.WithId(phoneNumberId).DeleteAsync();
			phoneNumberId = null;
		}
		finally
		{
			if (phoneNumberId is not null)
			{
				try
				{
					await Org.People.WithId(Fixture.PersonId).PhoneNumbers
						.WithId(phoneNumberId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
