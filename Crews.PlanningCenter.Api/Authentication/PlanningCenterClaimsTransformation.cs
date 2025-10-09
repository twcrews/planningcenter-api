using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Crews.PlanningCenter.Api.Authentication;

internal sealed class PlanningCenterClaimsTransformation : IClaimsTransformation
{
	public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	{
		if (principal.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
			return Task.FromResult(principal);

		if (string.IsNullOrEmpty(identity.Name))
		{
			string? nameClaim = principal.FindFirst("name")?.Value;
			if (!string.IsNullOrEmpty(nameClaim))
			{
				identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim));
			}
		}

		return Task.FromResult(principal);
	}
}
