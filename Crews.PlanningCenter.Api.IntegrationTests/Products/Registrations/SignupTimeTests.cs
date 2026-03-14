using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Registrations;

public class SignupTimeTests(RegistrationsFixture fixture) : RegistrationsTestBase(fixture)
{
	[Fact]
	public async Task SignupTime_GetAsync_ReturnsSignupTime()
	{
		var result = await Org.Signups.WithId(Fixture.SignupId)
			.SignupTimes.WithId(Fixture.SignupTimeId)
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
