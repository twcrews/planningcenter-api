using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class BatchGroupTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task BatchGroup_FullCrudLifecycle()
	{
		string? batchGroupId = null;
		try
		{
			var createResult = await Org.BatchGroups.PostAsync(
				new BatchGroup { Description = $"IntTest-BatchGroup-{UniqueId}" });

			Assert.NotNull(createResult.Data);
			batchGroupId = createResult.Data.Id;
			Assert.NotNull(batchGroupId);
			Assert.Equal($"IntTest-BatchGroup-{UniqueId}", createResult.Data.Attributes?.Description);

			var readResult = await Org.BatchGroups.WithId(batchGroupId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(batchGroupId, readResult.Data.Id);

			var updateResult = await Org.BatchGroups.WithId(batchGroupId).PatchAsync(
				new BatchGroup { Description = $"IntTest-BatchGroup-{UniqueId}-Updated" });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.BatchGroups.WithId(batchGroupId).GetAsync();
			Assert.Equal($"IntTest-BatchGroup-{UniqueId}-Updated", verifyResult.Data?.Attributes?.Description);

			await Org.BatchGroups.WithId(batchGroupId).DeleteAsync();
			batchGroupId = null;
		}
		finally
		{
			if (batchGroupId is not null)
			{
				try { await Org.BatchGroups.WithId(batchGroupId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
