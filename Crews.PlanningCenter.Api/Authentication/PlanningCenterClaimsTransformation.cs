using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Transforms Planning Center OIDC claims into standardized ASP.NET Core identity claims.
/// </summary>
public class PlanningCenterClaimsTransformation : IClaimsTransformation
{
	/// <summary>
	/// Transforms the claims principal by mapping Planning Center-specific claims to standard claim types.
	/// </summary>
	/// <param name="principal">The claims principal to transform.</param>
	/// <returns>The transformed claims principal.</returns>
	public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	{
		if (principal.Identity?.IsAuthenticated != true)
		{
			return Task.FromResult(principal);
		}

		// Check if this is a Planning Center identity
		var identity = principal.Identity as ClaimsIdentity;
		if (identity?.AuthenticationType != PlanningCenterAuthenticationDefaults.AuthenticationScheme)
		{
			return Task.FromResult(principal);
		}

		// Create a new identity with the transformed claims
		var transformedIdentity = new ClaimsIdentity(
			identity.AuthenticationType,
			identity.NameClaimType,
			identity.RoleClaimType);

		// Copy existing claims
		foreach (var claim in identity.Claims)
		{
			transformedIdentity.AddClaim(claim);
		}

		// Map Planning Center claims to standard claim types
		MapClaimIfExists(identity, transformedIdentity, "sub", ClaimTypes.NameIdentifier);
		MapClaimIfExists(identity, transformedIdentity, "name", ClaimTypes.Name);
		MapClaimIfExists(identity, transformedIdentity, "email", ClaimTypes.Email);

		// Add organization claims with custom claim types
		MapClaimIfExists(identity, transformedIdentity, "organization_id", "organization_id");
		MapClaimIfExists(identity, transformedIdentity, "organization_name", "organization_name");

		// Create a new principal with the transformed identity
		var transformedPrincipal = new ClaimsPrincipal(transformedIdentity);

		return Task.FromResult(transformedPrincipal);
	}

	private static void MapClaimIfExists(
		ClaimsIdentity sourceIdentity,
		ClaimsIdentity targetIdentity,
		string sourceClaimType,
		string targetClaimType)
	{
		var claim = sourceIdentity.FindFirst(sourceClaimType);
		if (claim != null && targetIdentity.FindFirst(targetClaimType) == null)
		{
			targetIdentity.AddClaim(new Claim(targetClaimType, claim.Value, claim.ValueType, claim.Issuer));
		}
	}
}
