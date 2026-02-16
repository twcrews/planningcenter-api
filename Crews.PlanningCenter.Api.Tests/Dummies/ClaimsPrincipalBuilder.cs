using System.Security.Claims;
using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

/// <summary>
/// Fluent builder for creating ClaimsPrincipal instances in tests.
/// </summary>
public class ClaimsPrincipalBuilder
{
	private readonly List<Claim> _claims = [];
	private string _authenticationType = "TestAuth";
	private bool _isAuthenticated = true;

	/// <summary>
	/// Sets the authentication type for the identity.
	/// </summary>
	public ClaimsPrincipalBuilder WithAuthenticationType(string authenticationType)
	{
		_authenticationType = authenticationType;
		return this;
	}

	/// <summary>
	/// Sets whether the identity is authenticated.
	/// </summary>
	public ClaimsPrincipalBuilder WithAuthentication(bool isAuthenticated)
	{
		_isAuthenticated = isAuthenticated;
		return this;
	}

	/// <summary>
	/// Adds a claim with the specified type and value.
	/// </summary>
	public ClaimsPrincipalBuilder WithClaim(string type, string value)
	{
		_claims.Add(new Claim(type, value));
		return this;
	}

	/// <summary>
	/// Adds a subject claim (sub).
	/// </summary>
	public ClaimsPrincipalBuilder WithSubject(string subject)
	{
		return WithClaim("sub", subject);
	}

	/// <summary>
	/// Adds a name claim.
	/// </summary>
	public ClaimsPrincipalBuilder WithName(string name)
	{
		return WithClaim("name", name);
	}

	/// <summary>
	/// Adds an email claim.
	/// </summary>
	public ClaimsPrincipalBuilder WithEmail(string email)
	{
		return WithClaim("email", email);
	}

	/// <summary>
	/// Adds an organization_id claim.
	/// </summary>
	public ClaimsPrincipalBuilder WithOrganizationId(string organizationId)
	{
		return WithClaim("organization_id", organizationId);
	}

	/// <summary>
	/// Adds an organization_name claim.
	/// </summary>
	public ClaimsPrincipalBuilder WithOrganizationName(string organizationName)
	{
		return WithClaim("organization_name", organizationName);
	}

	/// <summary>
	/// Builds the ClaimsPrincipal with Planning Center authentication scheme.
	/// </summary>
	public ClaimsPrincipal Build()
	{
		var identity = _isAuthenticated
			? new ClaimsIdentity(_claims, _authenticationType)
			: new ClaimsIdentity(_claims);

		return new ClaimsPrincipal(identity);
	}

	/// <summary>
	/// Builds the ClaimsPrincipal with Planning Center OIDC authentication scheme.
	/// </summary>
	public ClaimsPrincipal BuildWithPlanningCenterAuth()
	{
		var identity = new ClaimsIdentity(_claims, PlanningCenterAuthenticationDefaults.AuthenticationScheme);
		return new ClaimsPrincipal(identity);
	}

	/// <summary>
	/// Creates a new builder instance.
	/// </summary>
	public static ClaimsPrincipalBuilder Create() => new();
}
