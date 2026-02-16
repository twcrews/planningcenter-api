using System.Text.Json;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Tests.Dummies.OAuth;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PlanningCenterOidcTokenResponseTests
{
	[Fact]
	public void Deserialize_FromValidJson_IncludesIdToken()
	{
		// Act
		var response = JsonSerializer.Deserialize<PlanningCenterOidcTokenResponse>(
			SampleTokenResponses.ValidOidcTokenResponse);

		// Assert
		Assert.NotNull(response);
		Assert.NotNull(response.IdToken);
		Assert.Contains(".", response.IdToken);
	}

	[Fact]
	public void Deserialize_IncludesBaseProperties()
	{
		// Act
		var response = JsonSerializer.Deserialize<PlanningCenterOidcTokenResponse>(
			SampleTokenResponses.ValidOidcTokenResponse);

		// Assert
		Assert.Equal("test_access_token_12345", response!.AccessToken);
		Assert.Equal("bearer", response.TokenType);
		Assert.Equal(7200, response.ExpiresIn);
		Assert.Equal("test_refresh_token_67890", response.RefreshToken);
	}

	[Fact]
	public void InheritsFromOAuthTokenResponse()
	{
		// Arrange
		var response = JsonSerializer.Deserialize<PlanningCenterOidcTokenResponse>(
			SampleTokenResponses.ValidOidcTokenResponse);

		// Act & Assert
		Assert.IsAssignableFrom<PlanningCenterOAuthTokenResponse>(response);
	}
}
