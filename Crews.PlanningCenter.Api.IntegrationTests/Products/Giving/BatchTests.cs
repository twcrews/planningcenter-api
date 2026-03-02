using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class BatchTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Batch_FullCrudLifecycle()
	{
		string? batchId = null;
		try
		{
			var createResult = await Org.Batches.PostAsync(
				new Batch { Description = $"IntTest-Batch-{UniqueId}" });

			Assert.NotNull(createResult.Data);
			batchId = createResult.Data.Id;
			Assert.NotNull(batchId);
			Assert.Equal($"IntTest-Batch-{UniqueId}", createResult.Data.Attributes?.Description);

			var readResult = await Org.Batches.WithId(batchId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(batchId, readResult.Data.Id);

			var updateResult = await Org.Batches.WithId(batchId).PatchAsync(
				new Batch { Description = $"IntTest-Batch-{UniqueId}-Updated" });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.Batches.WithId(batchId).GetAsync();
			Assert.Equal($"IntTest-Batch-{UniqueId}-Updated", verifyResult.Data?.Attributes?.Description);

			await Org.Batches.WithId(batchId).DeleteAsync();
			batchId = null;
		}
		finally
		{
			if (batchId is not null)
			{
				try { await Org.Batches.WithId(batchId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
