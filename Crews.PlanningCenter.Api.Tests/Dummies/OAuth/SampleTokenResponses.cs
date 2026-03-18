namespace Crews.PlanningCenter.Api.Tests.Dummies.OAuth;

/// <summary>
/// Sample OAuth and OIDC token responses for testing.
/// </summary>
public static class SampleTokenResponses
{
	/// <summary>
	/// A valid OAuth token response with all fields populated.
	/// CreatedAt: 1704067200 (2024-01-01 00:00:00 UTC)
	/// ExpiresIn: 7200 (2 hours)
	/// </summary>
	public const string ValidOAuthTokenResponse = """
	{
		"access_token": "test_access_token_12345",
		"token_type": "bearer",
		"expires_in": 7200,
		"refresh_token": "test_refresh_token_67890",
		"scope": "people groups",
		"created_at": 1704067200
	}
	""";

	/// <summary>
	/// A valid OIDC token response including an ID token.
	/// CreatedAt: 1704067200 (2024-01-01 00:00:00 UTC)
	/// ExpiresIn: 7200 (2 hours)
	/// </summary>
	public const string ValidOidcTokenResponse = """
	{
		"access_token": "test_access_token_12345",
		"token_type": "bearer",
		"expires_in": 7200,
		"refresh_token": "test_refresh_token_67890",
		"scope": "openid people groups",
		"created_at": 1704067200,
		"id_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiZW1haWwiOiJqb2huLmRvZUBleGFtcGxlLmNvbSIsIm9yZ2FuaXphdGlvbl9pZCI6MTIzLCJvcmdhbml6YXRpb25fbmFtZSI6IlRlc3QgT3JnIiwiaWF0IjoxNzA0MDY3MjAwLCJleHAiOjE3MDQwNzA4MDB9.test_signature"
	}
	""";

	/// <summary>
	/// An expired OAuth token response.
	/// CreatedAt: 946684800 (2000-01-01 00:00:00 UTC)
	/// ExpiresIn: 7200 (2 hours)
	/// This token expired on 2000-01-01 02:00:00 UTC
	/// </summary>
	public const string ExpiredTokenResponse = """
	{
		"access_token": "expired_access_token",
		"token_type": "bearer",
		"expires_in": 7200,
		"refresh_token": "expired_refresh_token",
		"scope": "api",
		"created_at": 946684800
	}
	""";

	/// <summary>
	/// A token response with an empty scope string.
	/// </summary>
	public const string TokenResponseWithEmptyScope = """
	{
		"access_token": "test_access_token",
		"token_type": "bearer",
		"expires_in": 7200,
		"refresh_token": "test_refresh_token",
		"scope": "",
		"created_at": 1704067200
	}
	""";

	/// <summary>
	/// A token response with all available scopes.
	/// </summary>
	public const string TokenResponseWithAllScopes = """
	{
		"access_token": "test_access_token",
		"token_type": "bearer",
		"expires_in": 7200,
		"refresh_token": "test_refresh_token",
		"scope": "api calendar check_ins giving groups people publishing registrations services openid",
		"created_at": 1704067200
	}
	""";

	/// <summary>
	/// Malformed JSON for error testing.
	/// </summary>
	public const string InvalidJsonResponse = """
	{
		"invalid": "this is not a valid token response"
	""";

	/// <summary>
	/// A well-formed JWT (3 parts: header.payload.signature) for ID token testing.
	/// Header: {"alg":"HS256","typ":"JWT"}
	/// Payload: {
	///   "iss": "https://api.planningcenteronline.com",
	///   "sub": "1234567890",
	///   "aud": "test_client_id",
	///   "name": "John Doe",
	///   "email": "john.doe@example.com",
	///   "organization_id": 123,
	///   "organization_name": "Test Org",
	///   "iat": 1704067200,
	///   "exp": 1704070800,
	///   "nonce": "test_nonce"
	/// }
	/// </summary>
	public const string ValidIdToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL2FwaS5wbGFubmluZ2NlbnRlcm9ubGluZS5jb20iLCJzdWIiOiIxMjM0NTY3ODkwIiwiYXVkIjoidGVzdF9jbGllbnRfaWQiLCJuYW1lIjoiSm9obiBEb2UiLCJlbWFpbCI6ImpvaG4uZG9lQGV4YW1wbGUuY29tIiwib3JnYW5pemF0aW9uX2lkIjoxMjMsIm9yZ2FuaXphdGlvbl9uYW1lIjoiVGVzdCBPcmciLCJpYXQiOjE3MDQwNjcyMDAsImV4cCI6MTcwNDA3MDgwMCwibm9uY2UiOiJ0ZXN0X25vbmNlIn0.test_signature";

	/// <summary>
	/// A JWT with only 2 parts (missing signature) - invalid format.
	/// </summary>
	public const string InvalidIdTokenTwoParts = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIn0";

	/// <summary>
	/// A JWT with 4 parts - invalid format.
	/// </summary>
	public const string InvalidIdTokenFourParts = "part1.part2.part3.part4";

	/// <summary>
	/// A JWT with invalid Base64 encoding in the payload.
	/// </summary>
	public const string InvalidIdTokenBadBase64 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.!!!invalid-base64!!!.test_signature";

	/// <summary>
	/// A JWT with valid Base64 but invalid JSON in the payload.
	/// </summary>
	public const string InvalidIdTokenBadJson = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.bm90X2pzb24.test_signature";
}
