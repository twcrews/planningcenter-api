using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PersonTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Person_FullCrudLifecycle()
	{
		string? personId = null;

		try
		{
			// -- Create --
			var createResult = await Org.People.PostAsync(new Person
			{
				FirstName = "IntTest",
				LastName = $"Person-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			personId = createResult.Data.Id;
			Assert.NotNull(personId);
			Assert.Equal("IntTest", createResult.Data.Attributes?.FirstName);

			// -- Read --
			var readResult = await Org.People.WithId(personId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(personId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.People.WithId(personId).PatchAsync(
				new Person { FirstName = "IntTest-Updated" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.People.WithId(personId).GetAsync();
			Assert.Equal("IntTest-Updated", verifyResult.Data?.Attributes?.FirstName);

			// -- Delete --
			await Org.People.WithId(personId).DeleteAsync();
			personId = null;
		}
		finally
		{
			if (personId is not null)
			{
				try
				{
					await Org.People.WithId(personId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
