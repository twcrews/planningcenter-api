using System.Security.Claims;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Tests.Dummies;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterClaimsTransformationTests
{
	[Fact]
	public async Task TransformAsync_WithUnauthenticatedPrincipal_ReturnsUnchanged()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthentication(false)
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.Same(principal, result);
	}

	[Fact]
	public async Task TransformAsync_WithNonPlanningCenterScheme_ReturnsUnchanged()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType("OtherScheme")
			.WithSubject("123")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.Same(principal, result);
	}

	[Fact]
	public async Task TransformAsync_WithPlanningCenterIdentity_TransformsClaims()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("John Doe")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotSame(principal, result);
		Assert.True(result.Identity?.IsAuthenticated);
	}

	[Fact]
	public async Task TransformAsync_MapsSub_ToNameIdentifier()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123456")
			.WithName("Test User")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotNull(result.FindFirst(ClaimTypes.NameIdentifier));
		Assert.Equal("123456", result.FindFirst(ClaimTypes.NameIdentifier)?.Value);
	}

	[Fact]
	public async Task TransformAsync_MapsName_ToName()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("John Doe")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotNull(result.FindFirst(ClaimTypes.Name));
		Assert.Equal("John Doe", result.FindFirst(ClaimTypes.Name)?.Value);
	}

	[Fact]
	public async Task TransformAsync_MapsEmail_ToEmail()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("Test User")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotNull(result.FindFirst(ClaimTypes.Email));
		Assert.Equal("test@example.com", result.FindFirst(ClaimTypes.Email)?.Value);
	}

	[Fact]
	public async Task TransformAsync_MapsOrganizationId_ToOrganizationId()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("Test User")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotNull(result.FindFirst("organization_id"));
		Assert.Equal("789", result.FindFirst("organization_id")?.Value);
	}

	[Fact]
	public async Task TransformAsync_MapsOrganizationName_ToOrganizationName()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("Test User")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotNull(result.FindFirst("organization_name"));
		Assert.Equal("Test Org", result.FindFirst("organization_name")?.Value);
	}

	[Fact]
	public async Task TransformAsync_WithExistingTargetClaim_DoesNotDuplicate()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var identity = new ClaimsIdentity(
			[
				new Claim("sub", "123"),
				new Claim(ClaimTypes.NameIdentifier, "existing"),
				new Claim("name", "Test"),
				new Claim("email", "test@example.com"),
				new Claim("organization_id", "1"),
				new Claim("organization_name", "Org")
			],
			PlanningCenterAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		var nameIdentifiers = result.FindAll(ClaimTypes.NameIdentifier).ToList();
		Assert.Single(nameIdentifiers);
		Assert.Equal("existing", nameIdentifiers[0].Value);
	}

	[Fact]
	public async Task TransformAsync_PreservesOriginalClaims()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("Test User")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotNull(result.FindFirst("sub"));
		Assert.NotNull(result.FindFirst("name"));
		Assert.NotNull(result.FindFirst("email"));
		Assert.Equal("123", result.FindFirst("sub")?.Value);
	}

	[Fact]
	public async Task TransformAsync_WithMissingSourceClaim_DoesNotAddTarget()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var identity = new ClaimsIdentity(
			[new Claim("sub", "123")],
			PlanningCenterAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.Null(result.FindFirst(ClaimTypes.Email));
		Assert.Null(result.FindFirst("organization_id"));
	}

	[Fact]
	public async Task TransformAsync_CreatesNewPrincipal()
	{
		// Arrange
		var transformation = new PlanningCenterClaimsTransformation();
		var principal = ClaimsPrincipalBuilder.Create()
			.WithAuthenticationType(PlanningCenterAuthenticationDefaults.AuthenticationScheme)
			.WithSubject("123")
			.WithName("Test User")
			.WithEmail("test@example.com")
			.WithOrganizationId("789")
			.WithOrganizationName("Test Org")
			.Build();

		// Act
		var result = await transformation.TransformAsync(principal);

		// Assert
		Assert.NotSame(principal, result);
		Assert.IsType<ClaimsPrincipal>(result);
	}
}
