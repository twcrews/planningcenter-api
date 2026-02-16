using System.Text.Json;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Tests.Dummies.OAuth;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PlanningCenterOAuthTokenResponseTests
{
	[Fact]
	public void Scopes_ParsesScopeString()
	{
		// Arrange
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.ValidOAuthTokenResponse);

		// Act
		var scopes = response!.Scopes;

		// Assert
		Assert.True(scopes.HasFlag(PlanningCenterOAuthScope.People));
		Assert.True(scopes.HasFlag(PlanningCenterOAuthScope.Groups));
	}

	[Fact]
	public void Scopes_WithEmptyScope_ReturnsNone()
	{
		// Arrange
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.TokenResponseWithEmptyScope);

		// Act
		var scopes = response!.Scopes;

		// Assert
		Assert.Equal(PlanningCenterOAuthScope.None, scopes);
	}

	[Fact]
	public void CreatedAtDateTime_ConvertsUnixTimestamp()
	{
		// Arrange
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.ValidOAuthTokenResponse);

		// Act
		var createdAt = response!.CreatedAtDateTime;

		// Assert
		// 1704067200 = 2024-01-01 00:00:00 UTC
		Assert.Equal(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), createdAt);
	}

	[Fact]
	public void ExpiresAtDateTime_AddsExpiresInToCreatedAt()
	{
		// Arrange
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.ValidOAuthTokenResponse);

		// Act
		var expiresAt = response!.ExpiresAtDateTime;

		// Assert
		// CreatedAt (1704067200) + ExpiresIn (7200) = 1704074400 = 2024-01-01 02:00:00 UTC
		Assert.Equal(new DateTimeOffset(2024, 1, 1, 2, 0, 0, TimeSpan.Zero), expiresAt);
	}

	[Fact]
	public void IsExpired_WithFutureExpiration_ReturnsFalse()
	{
		// Arrange - Token expires in 2024, which is in the future relative to when tests run
		// We need a token that won't expire, so let's use a future timestamp
		var futureToken = """
		{
			"access_token": "test",
			"token_type": "bearer",
			"expires_in": 7200,
			"refresh_token": "test",
			"scope": "api",
			"created_at": 9999999999
		}
		""";
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(futureToken);

		// Act
		var isExpired = response!.IsExpired;

		// Assert
		Assert.False(isExpired);
	}

	[Fact]
	public void IsExpired_WithPastExpiration_ReturnsTrue()
	{
		// Arrange
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.ExpiredTokenResponse);

		// Act
		var isExpired = response!.IsExpired;

		// Assert
		Assert.True(isExpired);
	}

	[Fact]
	public void Deserialize_FromValidJson_CreatesInstance()
	{
		// Act
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.ValidOAuthTokenResponse);

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public void Deserialize_WithAllProperties_MapsCorrectly()
	{
		// Act
		var response = JsonSerializer.Deserialize<PlanningCenterOAuthTokenResponse>(
			SampleTokenResponses.ValidOAuthTokenResponse);

		// Assert
		Assert.Equal("test_access_token_12345", response!.AccessToken);
		Assert.Equal("bearer", response.TokenType);
		Assert.Equal(7200, response.ExpiresIn);
		Assert.Equal("test_refresh_token_67890", response.RefreshToken);
		Assert.Equal("people groups", response.Scope);
		Assert.Equal(1704067200, response.CreatedAt);
	}
}
