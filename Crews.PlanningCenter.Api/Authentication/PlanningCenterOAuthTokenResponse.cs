using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Represents the response from the OAuth 2.0 token endpoint.
/// </summary>
public class PlanningCenterOAuthTokenResponse
{
	/// <summary>
	/// The access token issued by the authorization server.
	/// </summary>
	[JsonPropertyName("access_token")]
	public required string AccessToken { get; init; }

	/// <summary>
	/// The type of token issued. Always "bearer" for Planning Center.
	/// </summary>
	[JsonPropertyName("token_type")]
	public required string TokenType { get; init; }

	/// <summary>
	/// The lifetime in seconds of the access token. For Planning Center, this is 7200 (2 hours).
	/// </summary>
	[JsonPropertyName("expires_in")]
	public required int ExpiresIn { get; init; }

	/// <summary>
	/// The refresh token which can be used to obtain new access tokens.
	/// Refresh tokens are valid for 90 days from the original access token issuance.
	/// </summary>
	[JsonPropertyName("refresh_token")]
	public required string RefreshToken { get; init; }

	/// <summary>
	/// The scope of the access token as a space-separated string.
	/// </summary>
	[JsonPropertyName("scope")]
	public required string Scope { get; init; }

	/// <summary>
	/// The Unix timestamp when the token was created.
	/// </summary>
	[JsonPropertyName("created_at")]
	public required long CreatedAt { get; init; }

	/// <summary>
	/// Gets the parsed scope flags from the scope string.
	/// </summary>
	[JsonIgnore]
	public PlanningCenterOAuthScope Scopes =>
		PlanningCenterOAuthScopeExtensions.ParseScopeString(Scope);

	/// <summary>
	/// Gets the creation date and time of the token.
	/// </summary>
	[JsonIgnore]
	public DateTimeOffset CreatedAtDateTime =>
		DateTimeOffset.FromUnixTimeSeconds(CreatedAt);

	/// <summary>
	/// Gets the expiration date and time of the access token.
	/// </summary>
	[JsonIgnore]
	public DateTimeOffset ExpiresAtDateTime =>
		CreatedAtDateTime.AddSeconds(ExpiresIn);

	/// <summary>
	/// Gets a value indicating whether the access token has expired.
	/// </summary>
	[JsonIgnore]
	public bool IsExpired =>
		DateTimeOffset.UtcNow >= ExpiresAtDateTime;
}
