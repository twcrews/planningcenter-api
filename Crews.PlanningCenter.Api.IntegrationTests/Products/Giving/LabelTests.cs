using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class LabelTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Label_FullCrudLifecycle()
	{
		string? labelId = null;
		try
		{
			var createResult = await Org.Labels.PostAsync(
				new Label { Slug = $"inttest-label-{UniqueId}" });

			Assert.NotNull(createResult.Data);
			labelId = createResult.Data.Id;
			Assert.NotNull(labelId);

			var readResult = await Org.Labels.WithId(labelId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(labelId, readResult.Data.Id);

			var updateResult = await Org.Labels.WithId(labelId).PatchAsync(
				new Label { Slug = $"inttest-label-{UniqueId}-upd" });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.Labels.WithId(labelId).GetAsync();
			Assert.Equal($"inttest-label-{UniqueId}-upd", verifyResult.Data?.Attributes?.Slug);

			await Org.Labels.WithId(labelId).DeleteAsync();
			labelId = null;
		}
		finally
		{
			if (labelId is not null)
			{
				try { await Org.Labels.WithId(labelId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
