using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Represents the response from the OIDC token endpoint, which includes an ID token
/// in addition to the standard OAuth 2.0 tokens.
/// </summary>
public class PlanningCenterOidcTokenResponse : PlanningCenterOAuthTokenResponse
{
	/// <summary>
	/// The ID token (JWT) containing identity claims for the authenticated user.
	/// ID tokens expire after 1 hour from the time they are issued.
	/// </summary>
	[JsonPropertyName("id_token")]
	public required string IdToken { get; init; }
}
