using System.Security.Claims;
using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterClaimsTransformationTests
{
	private readonly PlanningCenterClaimsTransformation _transformation = new();

	[Fact(DisplayName = "TransformAsync returns principal unchanged when not authenticated")]
	public async Task TransformAsync_ReturnsUnchanged_WhenNotAuthenticated()
	{
		// Arrange
		ClaimsPrincipal principal = new(new ClaimsIdentity());

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		Assert.Same(principal, result);
		Assert.False(result.Identity?.IsAuthenticated);
	}

	[Fact(DisplayName = "TransformAsync adds Name claim when missing")]
	public async Task TransformAsync_AddsNameClaim_WhenMissing()
	{
		// Arrange
		ClaimsIdentity identity = new([
			new Claim("name", "John Doe")
		], "TestAuthType");

		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		Assert.True(result.Identity!.IsAuthenticated);
		Claim? nameClaim = result.FindFirst(ClaimTypes.Name);
		Assert.NotNull(nameClaim);
		Assert.Equal("John Doe", nameClaim.Value);
	}

	[Fact(DisplayName = "TransformAsync does not add Name claim when already present")]
	public async Task TransformAsync_DoesNotAddNameClaim_WhenAlreadyPresent()
	{
		// Arrange
		ClaimsIdentity identity = new([
			new Claim(ClaimTypes.Name, "Existing Name"),
			new Claim("name", "Different Name")
		], "TestAuthType");

		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		Assert.True(result.Identity!.IsAuthenticated);
		Assert.Equal("Existing Name", result.Identity.Name);
		IEnumerable<Claim> nameClaims = result.FindAll(ClaimTypes.Name);
		Assert.Single(nameClaims);
	}

	[Fact(DisplayName = "TransformAsync does not add Name claim when name claim is empty")]
	public async Task TransformAsync_DoesNotAddNameClaim_WhenNameClaimEmpty()
	{
		// Arrange
		ClaimsIdentity identity = new([
			new Claim("name", "")
		], "TestAuthType");

		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		Assert.True(result.Identity!.IsAuthenticated);
		Claim? nameClaim = result.FindFirst(ClaimTypes.Name);
		Assert.Null(nameClaim);
	}

	[Fact(DisplayName = "TransformAsync handles principal with no name claim")]
	public async Task TransformAsync_HandlesNoClaim()
	{
		// Arrange
		ClaimsIdentity identity = new([
			new Claim("sub", "123")
		], "TestAuthType");

		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		Assert.True(result.Identity!.IsAuthenticated);
		Claim? nameClaim = result.FindFirst(ClaimTypes.Name);
		Assert.Null(nameClaim);
	}

	[Fact(DisplayName = "TransformAsync handles null identity")]
	public async Task TransformAsync_HandlesNullIdentity()
	{
		// Arrange
		ClaimsPrincipal principal = new();

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		Assert.Same(principal, result);
	}
}
