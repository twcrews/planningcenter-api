using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.People.V2025_11_10;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "People")]
public class PeopleEmailTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task Person_And_Email_FullCrudLifecycle()
	{
		var peopleClient = new PeopleClient(HttpClient);
		var org = peopleClient.Latest;

		string? personId = null;
		string? emailId = null;

		try
		{
			// -- Create Person (parent) --
			var personEndpoint = org.People;
			var personResult = await personEndpoint.PostAsync(new Person
			{
				FirstName = "IntTest",
				LastName = $"Person-{UniqueId}"
			});
			Assert.NotNull(personResult.Data);
			personId = personResult.Data.Id;
			Assert.NotNull(personId);

			// -- Create Email on Person --
			var emailEndpoint = personEndpoint.WithId(personId).Emails;
			var createResult = await emailEndpoint.PostAsync(new Email
			{
				Address = $"inttest-{UniqueId}@gmail.com",
				Location = "Home"
			});
			Assert.NotNull(createResult.Data);
			emailId = createResult.Data.Id;
			Assert.NotNull(emailId);
			Assert.Equal($"inttest-{UniqueId}@gmail.com",
				createResult.Data.Attributes?.Address);

			// -- Read Email --
			var singleEmailClient = org.People.WithId(personId).Emails.WithId(emailId);
			var readResult = await singleEmailClient.GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(emailId, readResult.Data.Id);
			Assert.Equal($"inttest-{UniqueId}@gmail.com",
				readResult.Data.Attributes?.Address);

			// -- Update Email --
			var updateClient = org.People.WithId(personId).Emails.WithId(emailId);
			var updateResult = await updateClient.PatchAsync(new Email
			{
				Location = "Work"
			});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyClient = org.People.WithId(personId).Emails.WithId(emailId);
			var verifyResult = await verifyClient.GetAsync();
			Assert.Equal("Work", verifyResult.Data?.Attributes?.Location);

			// -- Delete Email --
			var deleteEmailClient = org.People.WithId(personId).Emails.WithId(emailId);
			await deleteEmailClient.DeleteAsync();
			emailId = null;

			// -- Delete Person --
			var deletePersonClient = org.People.WithId(personId);
			await deletePersonClient.DeleteAsync();
			personId = null;
		}
		finally
		{
			if (emailId is not null && personId is not null)
			{
				try
				{
					var cleanup = org.People.WithId(personId).Emails.WithId(emailId);
					await cleanup.DeleteAsync();
				}
				catch { /* best effort */ }
			}

			if (personId is not null)
			{
				try
				{
					var cleanup = org.People.WithId(personId);
					await cleanup.DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
