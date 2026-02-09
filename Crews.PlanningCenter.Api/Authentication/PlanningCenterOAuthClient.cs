using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Client for interacting with Planning Center OAuth 2.0 and OIDC endpoints.
/// </summary>
public class PlanningCenterOAuthClient
{
	private readonly HttpClient _httpClient;
	private readonly PlanningCenterOAuthClientOptions _options;

	/// <summary>
	/// Initializes a new instance of the <see cref="PlanningCenterOAuthClient"/> class.
	/// </summary>
	/// <param name="httpClient">The HTTP client to use for requests.</param>
	/// <param name="options">The OAuth client options.</param>
	public PlanningCenterOAuthClient(HttpClient httpClient, PlanningCenterOAuthClientOptions options)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_options = options ?? throw new ArgumentNullException(nameof(options));

		if (string.IsNullOrWhiteSpace(_options.ClientId))
		{
			throw new ArgumentException("ClientId is required.", nameof(options));
		}

		if (string.IsNullOrWhiteSpace(_options.ClientSecret))
		{
			throw new ArgumentException("ClientSecret is required.", nameof(options));
		}

		if (string.IsNullOrWhiteSpace(_options.RedirectUri))
		{
			throw new ArgumentException("RedirectUri is required.", nameof(options));
		}
	}

	/// <summary>
	/// Builds the authorization URL for initiating the OAuth/OIDC flow.
	/// </summary>
	/// <param name="scopes">The scopes to request.</param>
	/// <param name="state">A unique, unguessable string used to prevent CSRF attacks.</param>
	/// <param name="nonce">A unique string for OIDC flows to prevent replay attacks. Required when requesting openid scope.</param>
	/// <param name="prompt">The prompt parameter to control the login experience (e.g., "select_account" or "login").</param>
	/// <returns>The authorization URL.</returns>
	public string BuildAuthorizationUrl(
		PlanningCenterOAuthScope scopes,
		string? state = null,
		string? nonce = null,
		string? prompt = null)
	{
		var query = HttpUtility.ParseQueryString(string.Empty);
		query["client_id"] = _options.ClientId;
		query["redirect_uri"] = _options.RedirectUri;
		query["response_type"] = "code";
		query["scope"] = scopes.ToScopeString();

		if (!string.IsNullOrWhiteSpace(state))
		{
			query["state"] = state;
		}

		if (!string.IsNullOrWhiteSpace(nonce))
		{
			query["nonce"] = nonce;
		}

		if (!string.IsNullOrWhiteSpace(prompt))
		{
			query["prompt"] = prompt;
		}

		return $"{PlanningCenterAuthenticationDefaults.AuthorizationEndpoint}?{query}";
	}

	/// <summary>
	/// Exchanges an authorization code for access and refresh tokens.
	/// </summary>
	/// <param name="code">The authorization code received from the authorization endpoint.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The OAuth token response.</returns>
	public async Task<PlanningCenterOAuthTokenResponse> ExchangeCodeForTokenAsync(
		string code,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(code);

		var requestBody = new Dictionary<string, string>
		{
			["grant_type"] = "authorization_code",
			["code"] = code,
			["client_id"] = _options.ClientId,
			["client_secret"] = _options.ClientSecret,
			["redirect_uri"] = _options.RedirectUri
		};

		using var content = new FormUrlEncodedContent(requestBody);
		var response = await _httpClient.PostAsync(
			PlanningCenterAuthenticationDefaults.TokenEndpoint,
			content,
			cancellationToken);

		response.EnsureSuccessStatusCode();

		var tokenResponse = await response.Content.ReadFromJsonAsync<PlanningCenterOAuthTokenResponse>(
			cancellationToken: cancellationToken);

		return tokenResponse ?? throw new InvalidOperationException("Failed to deserialize token response.");
	}

	/// <summary>
	/// Exchanges an authorization code for access, refresh, and ID tokens (OIDC flow).
	/// </summary>
	/// <param name="code">The authorization code received from the authorization endpoint.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The OIDC token response including the ID token.</returns>
	public async Task<PlanningCenterOidcTokenResponse> ExchangeCodeForOidcTokenAsync(
		string code,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(code);

		var requestBody = new Dictionary<string, string>
		{
			["grant_type"] = "authorization_code",
			["code"] = code,
			["client_id"] = _options.ClientId,
			["client_secret"] = _options.ClientSecret,
			["redirect_uri"] = _options.RedirectUri
		};

		using var content = new FormUrlEncodedContent(requestBody);
		var response = await _httpClient.PostAsync(
			PlanningCenterAuthenticationDefaults.TokenEndpoint,
			content,
			cancellationToken);

		response.EnsureSuccessStatusCode();

		var tokenResponse = await response.Content.ReadFromJsonAsync<PlanningCenterOidcTokenResponse>(
			cancellationToken: cancellationToken);

		return tokenResponse ?? throw new InvalidOperationException("Failed to deserialize OIDC token response.");
	}

	/// <summary>
	/// Refreshes an expired access token using a refresh token.
	/// </summary>
	/// <param name="refreshToken">The refresh token.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The new OAuth token response.</returns>
	public async Task<PlanningCenterOAuthTokenResponse> RefreshTokenAsync(
		string refreshToken,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

		var requestBody = new Dictionary<string, string>
		{
			["grant_type"] = "refresh_token",
			["refresh_token"] = refreshToken,
			["client_id"] = _options.ClientId,
			["client_secret"] = _options.ClientSecret
		};

		using var content = new FormUrlEncodedContent(requestBody);
		var response = await _httpClient.PostAsync(
			PlanningCenterAuthenticationDefaults.TokenEndpoint,
			content,
			cancellationToken);

		response.EnsureSuccessStatusCode();

		var tokenResponse = await response.Content.ReadFromJsonAsync<PlanningCenterOAuthTokenResponse>(
			cancellationToken: cancellationToken);

		return tokenResponse ?? throw new InvalidOperationException("Failed to deserialize token response.");
	}

	/// <summary>
	/// Gets the current user's identity claims from the userinfo endpoint.
	/// </summary>
	/// <param name="accessToken">The access token.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The user's identity claims.</returns>
	public async Task<PlanningCenterOidcClaims> GetUserInfoAsync(
		string accessToken,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

		using var request = new HttpRequestMessage(HttpMethod.Get, PlanningCenterAuthenticationDefaults.UserInfoEndpoint);
		request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

		var response = await _httpClient.SendAsync(request, cancellationToken);
		response.EnsureSuccessStatusCode();

		var userInfo = await response.Content.ReadFromJsonAsync<PlanningCenterOidcClaims>(
			cancellationToken: cancellationToken);

		return userInfo ?? throw new InvalidOperationException("Failed to deserialize userinfo response.");
	}

	/// <summary>
	/// Parses and validates an ID token JWT to extract claims.
	/// </summary>
	/// <param name="idToken">The ID token JWT string.</param>
	/// <returns>The parsed identity claims.</returns>
	/// <remarks>
	/// This is a simple JWT parser that does not perform signature validation.
	/// For production use, consider using a proper JWT library that validates signatures.
	/// </remarks>
	public PlanningCenterOidcClaims ParseIdToken(string idToken)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(idToken);

		var parts = idToken.Split('.');
		if (parts.Length != 3)
		{
			throw new ArgumentException("Invalid JWT format.", nameof(idToken));
		}

		// Decode the payload (second part)
		var payload = parts[1];

		// Add padding if necessary
		var paddingLength = 4 - (payload.Length % 4);
		if (paddingLength < 4)
		{
			payload += new string('=', paddingLength);
		}

		var payloadBytes = Convert.FromBase64String(payload.Replace('-', '+').Replace('_', '/'));
		var payloadJson = System.Text.Encoding.UTF8.GetString(payloadBytes);

		var claims = JsonSerializer.Deserialize<PlanningCenterOidcClaims>(payloadJson)
			?? throw new InvalidOperationException("Failed to deserialize ID token claims.");

		return claims;
	}
}
