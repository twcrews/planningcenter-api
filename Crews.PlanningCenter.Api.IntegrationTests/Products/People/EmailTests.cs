using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class EmailTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Email_FullCrudLifecycle()
	{
		string? emailId = null;

		try
		{
			var personEmails = Org.People.WithId(Fixture.PersonId).Emails;

			// -- Create Email on fixture Person --
			var createResult = await personEmails.PostAsync(new Email
			{
				Address = $"inttest-{UniqueId}@gmail.com",
				Location = "Home"
			});
			Assert.NotNull(createResult.Data);
			emailId = createResult.Data.Id;
			Assert.NotNull(emailId);
			Assert.Equal($"inttest-{UniqueId}@gmail.com",
				createResult.Data.Attributes?.Address);

			// -- Read --
			var readResult = await Org.People.WithId(Fixture.PersonId).Emails.WithId(emailId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(emailId, readResult.Data.Id);
			Assert.Equal($"inttest-{UniqueId}@gmail.com",
				readResult.Data.Attributes?.Address);

			// -- Update --
			var updateResult = await Org.People.WithId(Fixture.PersonId).Emails.WithId(emailId).PatchAsync(
				new Email { Location = "Work" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.People.WithId(Fixture.PersonId).Emails.WithId(emailId).GetAsync();
			Assert.Equal("Work", verifyResult.Data?.Attributes?.Location);

			// -- Delete --
			await Org.People.WithId(Fixture.PersonId).Emails.WithId(emailId).DeleteAsync();
			emailId = null;
		}
		finally
		{
			if (emailId is not null)
			{
				try
				{
					await Org.People.WithId(Fixture.PersonId).Emails.WithId(emailId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
