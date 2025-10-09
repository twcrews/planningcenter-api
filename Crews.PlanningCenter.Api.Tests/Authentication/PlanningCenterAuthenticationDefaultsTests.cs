using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterAuthenticationDefaultsTests
{
	[Fact(DisplayName = "AuthenticationScheme has correct value")]
	public void AuthenticationScheme_HasCorrectValue()
	{
		// Act & Assert
		Assert.Equal("planningcenter", PlanningCenterAuthenticationDefaults.AuthenticationScheme);
	}

	[Fact(DisplayName = "Authority has correct value")]
	public void Authority_HasCorrectValue()
	{
		// Act & Assert
		Assert.Equal("https://api.planningcenteronline.com", PlanningCenterAuthenticationDefaults.Authority);
	}

	[Fact(DisplayName = "Authority is a valid URI")]
	public void Authority_IsValidUri()
	{
		// Act
		bool isValid = Uri.TryCreate(PlanningCenterAuthenticationDefaults.Authority, UriKind.Absolute, out Uri? uri);

		// Assert
		Assert.True(isValid);
		Assert.NotNull(uri);
		Assert.Equal(Uri.UriSchemeHttps, uri.Scheme);
	}
}
