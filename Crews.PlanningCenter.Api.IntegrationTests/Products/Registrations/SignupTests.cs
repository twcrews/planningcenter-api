using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Registrations;

public class SignupTests(RegistrationsFixture fixture) : RegistrationsTestBase(fixture)
{
	[Fact]
	public async Task Signup_GetAsync_ReturnsSignup()
	{
		var result = await Org.Signups.WithId(Fixture.SignupId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
