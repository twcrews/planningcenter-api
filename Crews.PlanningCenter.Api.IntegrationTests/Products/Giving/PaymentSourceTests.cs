using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class PaymentSourceTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task PaymentSource_FullCrudLifecycle()
	{
		string? paymentSourceId = null;
		try
		{
			var createResult = await Org.PaymentSources.PostAsync(
				new PaymentSource { Name = $"IntTest-Source-{UniqueId}" });

			Assert.NotNull(createResult.Data);
			paymentSourceId = createResult.Data.Id;
			Assert.NotNull(paymentSourceId);
			Assert.Equal($"IntTest-Source-{UniqueId}", createResult.Data.Attributes?.Name);

			var readResult = await Org.PaymentSources.WithId(paymentSourceId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(paymentSourceId, readResult.Data.Id);

			var updateResult = await Org.PaymentSources.WithId(paymentSourceId).PatchAsync(
				new PaymentSource { Name = $"IntTest-Source-{UniqueId}-Updated" });
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.PaymentSources.WithId(paymentSourceId).GetAsync();
			Assert.Equal($"IntTest-Source-{UniqueId}-Updated", verifyResult.Data?.Attributes?.Name);

			await Org.PaymentSources.WithId(paymentSourceId).DeleteAsync();
			paymentSourceId = null;
		}
		finally
		{
			if (paymentSourceId is not null)
			{
				try { await Org.PaymentSources.WithId(paymentSourceId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
