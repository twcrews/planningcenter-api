using System.Security.Claims;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class ClaimsPrincipalExtensionsTests
{
	[Fact(DisplayName = "GetSubject returns subject claim value")]
	public void GetSubject_ReturnsSubjectClaimValue()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity([
			new Claim(ClaimTypes.NameIdentifier, "12345")
		]));

		// Act
		string? subject = principal.GetSubject();

		// Assert
		Assert.Equal("12345", subject);
	}

	[Fact(DisplayName = "GetSubject returns null when claim not found")]
	public void GetSubject_ReturnsNull_WhenClaimNotFound()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity());

		// Act
		string? subject = principal.GetSubject();

		// Assert
		Assert.Null(subject);
	}

	[Fact(DisplayName = "GetName returns user's name")]
	public void GetName_ReturnsUserName()
	{
		// Arrange
		ClaimsIdentity identity = new([
			new Claim(ClaimTypes.Name, "John Doe")
		], "TestAuthType");
		ClaimsPrincipal principal = new(identity);

		// Act
		string? name = principal.GetName();

		// Assert
		Assert.Equal("John Doe", name);
	}

	[Fact(DisplayName = "GetName returns null when identity is null")]
	public void GetName_ReturnsNull_WhenIdentityNull()
	{
		// Arrange
		ClaimsPrincipal principal = new();

		// Act
		string? name = principal.GetName();

		// Assert
		Assert.Null(name);
	}

	[Fact(DisplayName = "GetEmail returns email claim value")]
	public void GetEmail_ReturnsEmailClaimValue()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity([
			new Claim(ClaimTypes.Email, "test@example.com")
		]));

		// Act
		string? email = principal.GetEmail();

		// Assert
		Assert.Equal("test@example.com", email);
	}

	[Fact(DisplayName = "GetEmail returns null when claim not found")]
	public void GetEmail_ReturnsNull_WhenClaimNotFound()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity());

		// Act
		string? email = principal.GetEmail();

		// Assert
		Assert.Null(email);
	}

	[Fact(DisplayName = "GetOrganizationId returns organization ID claim value")]
	public void GetOrganizationId_ReturnsOrganizationIdClaimValue()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity([
			new Claim("organization_id", "org-123")
		]));

		// Act
		string? organizationId = principal.GetOrganizationId();

		// Assert
		Assert.Equal("org-123", organizationId);
	}

	[Fact(DisplayName = "GetOrganizationId returns null when claim not found")]
	public void GetOrganizationId_ReturnsNull_WhenClaimNotFound()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity());

		// Act
		string? organizationId = principal.GetOrganizationId();

		// Assert
		Assert.Null(organizationId);
	}

	[Fact(DisplayName = "GetOrganizationName returns organization name claim value")]
	public void GetOrganizationName_ReturnsOrganizationNameClaimValue()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity([
			new Claim("organization_name", "Test Church")
		]));

		// Act
		string? organizationName = principal.GetOrganizationName();

		// Assert
		Assert.Equal("Test Church", organizationName);
	}

	[Fact(DisplayName = "GetOrganizationName returns null when claim not found")]
	public void GetOrganizationName_ReturnsNull_WhenClaimNotFound()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity());

		// Act
		string? organizationName = principal.GetOrganizationName();

		// Assert
		Assert.Null(organizationName);
	}

	[Fact(DisplayName = "All extension methods work with multiple claims")]
	public void AllExtensionMethods_WorkWithMultipleClaims()
	{
		// Arrange
		ClaimsIdentity identity = new([
			new Claim(ClaimTypes.NameIdentifier, "user-123"),
			new Claim(ClaimTypes.Name, "Jane Smith"),
			new Claim(ClaimTypes.Email, "jane@example.com"),
			new Claim("organization_id", "org-456"),
			new Claim("organization_name", "Example Org")
		], "TestAuthType");
		ClaimsPrincipal principal = new(identity);

		// Act & Assert
		Assert.Equal("user-123", principal.GetSubject());
		Assert.Equal("Jane Smith", principal.GetName());
		Assert.Equal("jane@example.com", principal.GetEmail());
		Assert.Equal("org-456", principal.GetOrganizationId());
		Assert.Equal("Example Org", principal.GetOrganizationName());
	}
}
