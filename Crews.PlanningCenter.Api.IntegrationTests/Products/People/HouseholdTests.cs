using System.Text.Json;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class HouseholdTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Household_FullCrudLifecycle()
	{
		string? householdId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Households.PostAsync(new JsonApiDocument<HouseholdResource>
			{
				Data = new()
				{
					Attributes = new Household { Name = $"IntTest-Household-{UniqueId}" },
					Relationships = new()
					{
						People = new()
						{
							Data = JsonSerializer.SerializeToElement<IEnumerable<JsonApiResourceIdentifier>>([
								new() { Type = "Person", Id = Fixture.PersonId }
							])
						},
						PrimaryContact = new()
						{
							Data = JsonSerializer.SerializeToElement<JsonApiResourceIdentifier>(new()
							{
								Type = "Person",
								Id = Fixture.PersonId
							})
						}
					}
				}
			});
			Assert.NotNull(createResult.Data);
			householdId = createResult.Data.Id;
			Assert.NotNull(householdId);
			Assert.Equal($"IntTest-Household-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Households.WithId(householdId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(householdId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Households.WithId(householdId).PatchAsync(
				new Household { Name = $"IntTest-Household-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Households.WithId(householdId).GetAsync();
			Assert.Equal($"IntTest-Household-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Households.WithId(householdId).DeleteAsync();
			householdId = null;
		}
		finally
		{
			if (householdId is not null)
			{
				try
				{
					await Org.Households.WithId(householdId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
