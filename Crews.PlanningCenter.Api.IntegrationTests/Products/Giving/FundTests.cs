using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class FundTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Fund_FullCrudLifecycle()
	{
		string? fundId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Funds.PostAsync(new Fund
			{
				Name = $"IntTest-Fund-{UniqueId}",
				Description = "Integration test fund"
			});
			Assert.NotNull(createResult.Data);
			fundId = createResult.Data.Id;
			Assert.NotNull(fundId);
			Assert.Equal($"IntTest-Fund-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Funds.WithId(fundId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(fundId, readResult.Data.Id);
			Assert.Equal($"IntTest-Fund-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update --
			var updateResult = await Org.Funds.WithId(fundId).PatchAsync(new Fund
			{
				Description = "Updated integration test fund"
			});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Funds.WithId(fundId).GetAsync();
			Assert.Equal("Updated integration test fund",
				verifyResult.Data?.Attributes?.Description);

			// -- Delete --
			await Org.Funds.WithId(fundId).DeleteAsync();
			fundId = null;
		}
		finally
		{
			if (fundId is not null)
			{
				try
				{
					await Org.Funds.WithId(fundId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
