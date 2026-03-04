using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class NamePrefixTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task NamePrefix_FullCrudLifecycle()
	{
		string? namePrefixId = null;

		try
		{
			// -- Create --
			var createResult = await Org.NamePrefixes.PostAsync(
				new NamePrefix { Value = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			namePrefixId = createResult.Data.Id;
			Assert.NotNull(namePrefixId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Value);

			// -- Read --
			var readResult = await Org.NamePrefixes.WithId(namePrefixId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(namePrefixId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.NamePrefixes.WithId(namePrefixId).PatchAsync(
				new NamePrefix { Value = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.NamePrefixes.WithId(namePrefixId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await Org.NamePrefixes.WithId(namePrefixId).DeleteAsync();
			namePrefixId = null;
		}
		finally
		{
			if (namePrefixId is not null)
			{
				try
				{
					await Org.NamePrefixes.WithId(namePrefixId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
