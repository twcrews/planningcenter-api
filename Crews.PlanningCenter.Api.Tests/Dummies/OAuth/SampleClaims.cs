namespace Crews.PlanningCenter.Api.Tests.Dummies.OAuth;

/// <summary>
/// Sample claims JSON responses for testing OIDC claims and userinfo endpoint.
/// </summary>
public static class SampleClaims
{
	/// <summary>
	/// A complete userinfo response with all claims.
	/// </summary>
	public const string ValidUserInfoResponse = """
	{
		"iss": "https://api.planningcenteronline.com",
		"sub": "1234567890",
		"aud": "test_client_id",
		"name": "John Doe",
		"email": "john.doe@example.com",
		"organization_id": 123,
		"organization_name": "Test Organization"
	}
	""";

	/// <summary>
	/// An ID token payload with expiration and issued-at timestamps.
	/// iat: 1704067200 (2024-01-01 00:00:00 UTC)
	/// exp: 1704070800 (2024-01-01 01:00:00 UTC)
	/// </summary>
	public const string ValidIdTokenPayload = """
	{
		"iss": "https://api.planningcenteronline.com",
		"sub": "1234567890",
		"aud": "test_client_id",
		"name": "John Doe",
		"email": "john.doe@example.com",
		"organization_id": 123,
		"organization_name": "Test Organization",
		"iat": 1704067200,
		"exp": 1704070800,
		"nonce": "test_nonce_value"
	}
	""";

	/// <summary>
	/// An expired ID token payload.
	/// iat: 946684800 (2000-01-01 00:00:00 UTC)
	/// exp: 946688400 (2000-01-01 01:00:00 UTC) - expired
	/// </summary>
	public const string ExpiredIdTokenPayload = """
	{
		"iss": "https://api.planningcenteronline.com",
		"sub": "1234567890",
		"aud": "test_client_id",
		"name": "John Doe",
		"email": "john.doe@example.com",
		"organization_id": 123,
		"organization_name": "Test Organization",
		"iat": 946684800,
		"exp": 946688400
	}
	""";

	/// <summary>
	/// Claims without optional exp and iat fields.
	/// </summary>
	public const string ClaimsWithoutTimestamps = """
	{
		"iss": "https://api.planningcenteronline.com",
		"sub": "1234567890",
		"name": "Jane Smith",
		"email": "jane.smith@example.com",
		"organization_id": 456,
		"organization_name": "Another Organization"
	}
	""";

	/// <summary>
	/// Minimal claims (only required fields).
	/// </summary>
	public const string MinimalClaims = """
	{
		"sub": "9876543210",
		"name": "Minimal User",
		"email": "minimal@example.com",
		"organization_id": 789,
		"organization_name": "Minimal Org"
	}
	""";
}
