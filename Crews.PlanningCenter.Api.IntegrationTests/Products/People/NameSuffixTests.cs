using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class NameSuffixTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task NameSuffix_FullCrudLifecycle()
	{
		string? nameSuffixId = null;

		try
		{
			// -- Create --
			var createResult = await Org.NameSuffixes.PostAsync(
				new NameSuffix { Value = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			nameSuffixId = createResult.Data.Id;
			Assert.NotNull(nameSuffixId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Value);

			// -- Read --
			var readResult = await Org.NameSuffixes.WithId(nameSuffixId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(nameSuffixId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.NameSuffixes.WithId(nameSuffixId).PatchAsync(
				new NameSuffix { Value = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.NameSuffixes.WithId(nameSuffixId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await Org.NameSuffixes.WithId(nameSuffixId).DeleteAsync();
			nameSuffixId = null;
		}
		finally
		{
			if (nameSuffixId is not null)
			{
				try
				{
					await Org.NameSuffixes.WithId(nameSuffixId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
