using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Represents the identity claims for a Planning Center user from an OIDC ID token or userinfo endpoint.
/// </summary>
public class PlanningCenterOidcClaims
{
	/// <summary>
	/// Issuer identifier. Always "https://api.planningcenteronline.com".
	/// </summary>
	[JsonPropertyName("iss")]
	public string? Issuer { get; init; }

	/// <summary>
	/// Subject identifier - the unique Planning Center user ID.
	/// </summary>
	[JsonPropertyName("sub")]
	public required string Subject { get; init; }

	/// <summary>
	/// Audience - the client ID for which the ID token is intended.
	/// </summary>
	[JsonPropertyName("aud")]
	public string? Audience { get; init; }

	/// <summary>
	/// The user's full name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// The user's email address.
	/// </summary>
	[JsonPropertyName("email")]
	public required string Email { get; init; }

	/// <summary>
	/// The organization ID the user belongs to.
	/// </summary>
	[JsonPropertyName("organization_id")]
	public required long OrganizationId { get; init; }

	/// <summary>
	/// The name of the organization the user belongs to.
	/// </summary>
	[JsonPropertyName("organization_name")]
	public required string OrganizationName { get; init; }

	/// <summary>
	/// The nonce value that was included in the authorization request.
	/// Used to prevent replay attacks.
	/// </summary>
	[JsonPropertyName("nonce")]
	public string? Nonce { get; init; }

	/// <summary>
	/// Expiration time (Unix timestamp). Only present in ID tokens.
	/// ID tokens expire after 1 hour.
	/// </summary>
	[JsonPropertyName("exp")]
	public long? ExpirationTime { get; init; }

	/// <summary>
	/// Issued at time (Unix timestamp). Only present in ID tokens.
	/// </summary>
	[JsonPropertyName("iat")]
	public long? IssuedAt { get; init; }

	/// <summary>
	/// Gets the expiration date and time. Returns null if not present.
	/// </summary>
	[JsonIgnore]
	public DateTimeOffset? ExpirationDateTime =>
		ExpirationTime.HasValue ? DateTimeOffset.FromUnixTimeSeconds(ExpirationTime.Value) : null;

	/// <summary>
	/// Gets the issued at date and time. Returns null if not present.
	/// </summary>
	[JsonIgnore]
	public DateTimeOffset? IssuedAtDateTime =>
		IssuedAt.HasValue ? DateTimeOffset.FromUnixTimeSeconds(IssuedAt.Value) : null;

	/// <summary>
	/// Gets a value indicating whether the ID token has expired. Returns false if expiration time is not present.
	/// </summary>
	[JsonIgnore]
	public bool IsExpired =>
		ExpirationDateTime.HasValue && DateTimeOffset.UtcNow >= ExpirationDateTime.Value;
}
