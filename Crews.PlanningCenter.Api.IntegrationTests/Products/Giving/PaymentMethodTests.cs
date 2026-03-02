using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class PaymentMethodTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task PaymentMethod_GetAsync_ReturnsPaymentMethod()
	{
		var paymentMethodId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"giving/v2/people/{Fixture.PersonId}/payment_methods");
		var result = await Org.People.WithId(Fixture.PersonId).PaymentMethods.WithId(paymentMethodId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
