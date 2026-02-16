using System.Text.Json;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Tests.Dummies.OAuth;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PlanningCenterOidcClaimsTests
{
	[Fact]
	public void ExpirationDateTime_WithValue_ConvertsUnixTimestamp()
	{
		// Arrange
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ValidIdTokenPayload);

		// Act
		var expirationDateTime = claims!.ExpirationDateTime;

		// Assert
		Assert.NotNull(expirationDateTime);
		// 1704070800 = 2024-01-01 01:00:00 UTC
		Assert.Equal(new DateTimeOffset(2024, 1, 1, 1, 0, 0, TimeSpan.Zero), expirationDateTime.Value);
	}

	[Fact]
	public void ExpirationDateTime_WithNull_ReturnsNull()
	{
		// Arrange
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ClaimsWithoutTimestamps);

		// Act
		var expirationDateTime = claims!.ExpirationDateTime;

		// Assert
		Assert.Null(expirationDateTime);
	}

	[Fact]
	public void IssuedAtDateTime_WithValue_ConvertsUnixTimestamp()
	{
		// Arrange
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ValidIdTokenPayload);

		// Act
		var issuedAtDateTime = claims!.IssuedAtDateTime;

		// Assert
		Assert.NotNull(issuedAtDateTime);
		// 1704067200 = 2024-01-01 00:00:00 UTC
		Assert.Equal(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), issuedAtDateTime.Value);
	}

	[Fact]
	public void IssuedAtDateTime_WithNull_ReturnsNull()
	{
		// Arrange
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ClaimsWithoutTimestamps);

		// Act
		var issuedAtDateTime = claims!.IssuedAtDateTime;

		// Assert
		Assert.Null(issuedAtDateTime);
	}

	[Fact]
	public void IsExpired_WithFutureExpiration_ReturnsFalse()
	{
		// Arrange - Create claims with far future expiration
		var futureClaims = """
		{
			"sub": "123",
			"name": "Test",
			"email": "test@example.com",
			"organization_id": 1,
			"organization_name": "Test",
			"exp": 9999999999
		}
		""";
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(futureClaims);

		// Act
		var isExpired = claims!.IsExpired;

		// Assert
		Assert.False(isExpired);
	}

	[Fact]
	public void IsExpired_WithPastExpiration_ReturnsTrue()
	{
		// Arrange
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ExpiredIdTokenPayload);

		// Act
		var isExpired = claims!.IsExpired;

		// Assert
		Assert.True(isExpired);
	}

	[Fact]
	public void IsExpired_WithNullExpiration_ReturnsFalse()
	{
		// Arrange
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ClaimsWithoutTimestamps);

		// Act
		var isExpired = claims!.IsExpired;

		// Assert
		Assert.False(isExpired);
	}

	[Fact]
	public void Deserialize_FromValidJson_MapsAllProperties()
	{
		// Act
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.ValidIdTokenPayload);

		// Assert
		Assert.Equal("https://api.planningcenteronline.com", claims!.Issuer);
		Assert.Equal("1234567890", claims.Subject);
		Assert.Equal("test_client_id", claims.Audience);
		Assert.Equal("John Doe", claims.Name);
		Assert.Equal("john.doe@example.com", claims.Email);
		Assert.Equal(123, claims.OrganizationId);
		Assert.Equal("Test Organization", claims.OrganizationName);
		Assert.Equal("test_nonce_value", claims.Nonce);
		Assert.Equal(1704067200, claims.IssuedAt);
		Assert.Equal(1704070800, claims.ExpirationTime);
	}

	[Fact]
	public void Deserialize_WithMissingOptionalProperties_HandlesGracefully()
	{
		// Act
		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(
			SampleClaims.MinimalClaims);

		// Assert
		Assert.Null(claims!.Issuer);
		Assert.Null(claims.Audience);
		Assert.Null(claims.Nonce);
		Assert.Null(claims.ExpirationTime);
		Assert.Null(claims.IssuedAt);
		Assert.NotNull(claims.Subject);
		Assert.NotNull(claims.Name);
		Assert.NotNull(claims.Email);
	}
}
