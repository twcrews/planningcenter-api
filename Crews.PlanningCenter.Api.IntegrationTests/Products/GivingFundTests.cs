using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.Giving.V2019_10_18;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "Giving")]
public class GivingFundTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task Fund_FullCrudLifecycle()
	{
		var givingClient = new GivingClient(HttpClient);
		var org = givingClient.Latest;

		string? fundId = null;

		try
		{
			// -- Create --
			var fundEndpoint = org.Funds; 
			var createResult = await fundEndpoint.PostAsync(new Fund
			{
				Name = $"IntTest-Fund-{UniqueId}",
				Description = "Integration test fund"
			});
			Assert.NotNull(createResult.Data);
			fundId = createResult.Data.Id;
			Assert.NotNull(fundId);
			Assert.Equal($"IntTest-Fund-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var singleClient = org.Funds.WithId(fundId);
			var readResult = await singleClient.GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(fundId, readResult.Data.Id);
			Assert.Equal($"IntTest-Fund-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update --
			var updateClient = org.Funds.WithId(fundId);
			var updateResult = await updateClient.PatchAsync(new Fund
			{
				Description = "Updated integration test fund"
			});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyClient = org.Funds.WithId(fundId);
			var verifyResult = await verifyClient.GetAsync();
			Assert.Equal("Updated integration test fund",
				verifyResult.Data?.Attributes?.Description);

			// -- Delete --
			var deleteClient = org.Funds.WithId(fundId);
			await deleteClient.DeleteAsync();
			fundId = null;
		}
		finally
		{
			if (fundId is not null)
			{
				try
				{
					var cleanup = org.Funds.WithId(fundId);
					await cleanup.DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
