using System.Net;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Tests.Dummies.OAuth;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthClientTests
{
	private readonly PlanningCenterOAuthClientOptions _validOptions = new()
	{
		ClientId = "test_client_id",
		ClientSecret = "test_client_secret",
		RedirectUri = "https://example.com/callback"
	};

	#region Constructor Tests

	[Fact]
	public void Constructor_WithValidParameters_CreatesClient()
	{
		// Arrange
		var httpClient = new HttpClient();

		// Act
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Assert
		Assert.NotNull(client);
	}

	[Fact]
	public void Constructor_WithNullHttpClient_ThrowsArgumentNullException()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			new PlanningCenterOAuthClient(null!, _validOptions));
	}

	[Fact]
	public void Constructor_WithNullOptions_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			new PlanningCenterOAuthClient(httpClient, null!));
	}

	[Fact]
	public void Constructor_WithEmptyClientId_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var options = new PlanningCenterOAuthClientOptions
		{
			ClientId = string.Empty,
			ClientSecret = "test_secret",
			RedirectUri = "https://example.com/callback"
		};

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() =>
			new PlanningCenterOAuthClient(httpClient, options));
		Assert.Contains("ClientId", exception.Message);
	}

	[Fact]
	public void Constructor_WithEmptyClientSecret_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var options = new PlanningCenterOAuthClientOptions
		{
			ClientId = "test_client_id",
			ClientSecret = string.Empty,
			RedirectUri = "https://example.com/callback"
		};

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() =>
			new PlanningCenterOAuthClient(httpClient, options));
		Assert.Contains("ClientSecret", exception.Message);
	}

	[Fact]
	public void Constructor_WithEmptyRedirectUri_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var options = new PlanningCenterOAuthClientOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			RedirectUri = string.Empty
		};

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() =>
			new PlanningCenterOAuthClient(httpClient, options));
		Assert.Contains("RedirectUri", exception.Message);
	}

	#endregion

	#region BuildAuthorizationUrl Tests

	[Fact]
	public void BuildAuthorizationUrl_WithBasicScopes_ReturnsValidUrl()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var url = client.BuildAuthorizationUrl(PlanningCenterOAuthScope.People);

		// Assert
		Assert.Contains("https://api.planningcenteronline.com/oauth/authorize", url);
		Assert.Contains("client_id=test_client_id", url);
		Assert.Contains("redirect_uri=https%3A%2F%2Fexample.com%2Fcallback", url, StringComparison.OrdinalIgnoreCase);
		Assert.Contains("response_type=code", url);
		Assert.Contains("scope=people", url);
	}

	[Fact]
	public void BuildAuthorizationUrl_WithMultipleScopes_CombinesScopesCorrectly()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);
		var scopes = PlanningCenterOAuthScope.People | PlanningCenterOAuthScope.Groups;

		// Act
		var url = client.BuildAuthorizationUrl(scopes);

		// Assert
		Assert.Contains("scope=", url);
		Assert.Contains("people", url);
		Assert.Contains("groups", url);
	}

	[Fact]
	public void BuildAuthorizationUrl_WithState_IncludesStateParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var url = client.BuildAuthorizationUrl(PlanningCenterOAuthScope.People, state: "test_state_123");

		// Assert
		Assert.Contains("state=test_state_123", url);
	}

	[Fact]
	public void BuildAuthorizationUrl_WithNonce_IncludesNonceParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var url = client.BuildAuthorizationUrl(PlanningCenterOAuthScope.OpenId, nonce: "test_nonce_456");

		// Assert
		Assert.Contains("nonce=test_nonce_456", url);
	}

	[Fact]
	public void BuildAuthorizationUrl_WithPrompt_IncludesPromptParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var url = client.BuildAuthorizationUrl(PlanningCenterOAuthScope.People, prompt: "select_account");

		// Assert
		Assert.Contains("prompt=select_account", url);
	}

	[Fact]
	public void BuildAuthorizationUrl_WithAllParameters_BuildsCompleteUrl()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var url = client.BuildAuthorizationUrl(
			PlanningCenterOAuthScope.OpenId | PlanningCenterOAuthScope.People,
			state: "test_state",
			nonce: "test_nonce",
			prompt: "select_account");

		// Assert
		Assert.Contains("client_id=test_client_id", url);
		Assert.Contains("redirect_uri=", url);
		Assert.Contains("response_type=code", url);
		Assert.Contains("scope=", url);
		Assert.Contains("state=test_state", url);
		Assert.Contains("nonce=test_nonce", url);
		Assert.Contains("prompt=select_account", url);
	}

	[Fact]
	public void BuildAuthorizationUrl_WithNoScopes_ReturnsEmptyScope()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var url = client.BuildAuthorizationUrl(PlanningCenterOAuthScope.None);

		// Assert
		Assert.Contains("scope=", url);
	}

	#endregion

	#region ExchangeCodeForTokenAsync Tests

	[Fact]
	public async Task ExchangeCodeForTokenAsync_WithValidCode_ReturnsToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond("application/json", SampleTokenResponses.ValidOAuthTokenResponse);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var response = await client.ExchangeCodeForTokenAsync("test_code");

		// Assert
		Assert.NotNull(response);
		Assert.Equal("test_access_token_12345", response.AccessToken);
		Assert.Equal("bearer", response.TokenType);
		Assert.Equal(7200, response.ExpiresIn);
		Assert.Equal("test_refresh_token_67890", response.RefreshToken);
	}

	[Fact]
	public async Task ExchangeCodeForTokenAsync_WithNullCode_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() =>
			client.ExchangeCodeForTokenAsync(null!));
	}

	[Fact]
	public async Task ExchangeCodeForTokenAsync_WithEmptyCode_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() =>
			client.ExchangeCodeForTokenAsync(string.Empty));
	}

	[Fact]
	public async Task ExchangeCodeForTokenAsync_WithHttpError_ThrowsHttpRequestException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond(HttpStatusCode.BadRequest, "application/json", """{"error": "invalid_grant"}""");

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<HttpRequestException>(() =>
			client.ExchangeCodeForTokenAsync("invalid_code"));
	}

	[Fact]
	public async Task ExchangeCodeForTokenAsync_CancellationToken_PropagatesCancellation()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond(async () =>
			{
				await Task.Delay(1000);
				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(SampleTokenResponses.ValidOAuthTokenResponse)
				};
			});

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);
		var cts = new CancellationTokenSource();
		cts.Cancel();

		// Act & Assert
		await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
			client.ExchangeCodeForTokenAsync("test_code", cts.Token));
	}

	#endregion

	#region ExchangeCodeForOidcTokenAsync Tests

	[Fact]
	public async Task ExchangeCodeForOidcTokenAsync_WithValidCode_ReturnsOidcToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond("application/json", SampleTokenResponses.ValidOidcTokenResponse);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var response = await client.ExchangeCodeForOidcTokenAsync("test_code");

		// Assert
		Assert.NotNull(response);
		Assert.Equal("test_access_token_12345", response.AccessToken);
		Assert.NotNull(response.IdToken);
	}

	[Fact]
	public async Task ExchangeCodeForOidcTokenAsync_IncludesIdToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond("application/json", SampleTokenResponses.ValidOidcTokenResponse);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var response = await client.ExchangeCodeForOidcTokenAsync("test_code");

		// Assert
		Assert.Contains(".", response.IdToken);
	}

	[Fact]
	public async Task ExchangeCodeForOidcTokenAsync_WithNullCode_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() =>
			client.ExchangeCodeForOidcTokenAsync(null!));
	}

	[Fact]
	public async Task ExchangeCodeForOidcTokenAsync_WithHttpError_ThrowsHttpRequestException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond(HttpStatusCode.Unauthorized);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<HttpRequestException>(() =>
			client.ExchangeCodeForOidcTokenAsync("invalid_code"));
	}

	#endregion

	#region RefreshTokenAsync Tests

	[Fact]
	public async Task RefreshTokenAsync_WithValidRefreshToken_ReturnsNewToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond("application/json", SampleTokenResponses.ValidOAuthTokenResponse);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var response = await client.RefreshTokenAsync("test_refresh_token");

		// Assert
		Assert.NotNull(response);
		Assert.Equal("test_access_token_12345", response.AccessToken);
	}

	[Fact]
	public async Task RefreshTokenAsync_WithNullRefreshToken_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() =>
			client.RefreshTokenAsync(null!));
	}

	[Fact]
	public async Task RefreshTokenAsync_WithEmptyRefreshToken_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() =>
			client.RefreshTokenAsync(string.Empty));
	}

	[Fact]
	public async Task RefreshTokenAsync_WithHttpError_ThrowsHttpRequestException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, PlanningCenterAuthenticationDefaults.TokenEndpoint)
			.Respond(HttpStatusCode.BadRequest);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<HttpRequestException>(() =>
			client.RefreshTokenAsync("invalid_refresh_token"));
	}

	#endregion

	#region GetUserInfoAsync Tests

	[Fact]
	public async Task GetUserInfoAsync_WithValidAccessToken_ReturnsClaims()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, PlanningCenterAuthenticationDefaults.UserInfoEndpoint)
			.Respond("application/json", SampleClaims.ValidUserInfoResponse);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var claims = await client.GetUserInfoAsync("test_access_token");

		// Assert
		Assert.NotNull(claims);
		Assert.Equal("1234567890", claims.Subject);
		Assert.Equal("John Doe", claims.Name);
		Assert.Equal("john.doe@example.com", claims.Email);
	}

	[Fact]
	public async Task GetUserInfoAsync_WithNullAccessToken_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() =>
			client.GetUserInfoAsync(null!));
	}

	[Fact]
	public async Task GetUserInfoAsync_WithEmptyAccessToken_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() =>
			client.GetUserInfoAsync(string.Empty));
	}

	[Fact]
	public async Task GetUserInfoAsync_SetsBearerAuthorizationHeader()
	{
		// Arrange
		string? capturedAuthHeader = null;
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, PlanningCenterAuthenticationDefaults.UserInfoEndpoint)
			.With(request =>
			{
				capturedAuthHeader = request.Headers.Authorization?.ToString();
				return true;
			})
			.Respond("application/json", SampleClaims.ValidUserInfoResponse);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		await client.GetUserInfoAsync("test_token");

		// Assert
		Assert.NotNull(capturedAuthHeader);
		Assert.Equal("Bearer test_token", capturedAuthHeader);
	}

	[Fact]
	public async Task GetUserInfoAsync_WithHttpError_ThrowsHttpRequestException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, PlanningCenterAuthenticationDefaults.UserInfoEndpoint)
			.Respond(HttpStatusCode.Unauthorized);

		var httpClient = mockHttp.ToHttpClient();
		httpClient.BaseAddress = new Uri("https://api.planningcenteronline.com/");
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		await Assert.ThrowsAsync<HttpRequestException>(() =>
			client.GetUserInfoAsync("invalid_token"));
	}

	#endregion

	#region ParseIdToken Tests

	[Fact]
	public void ParseIdToken_WithValidJwt_ParsesClaims()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act
		var claims = client.ParseIdToken(SampleTokenResponses.ValidIdToken);

		// Assert
		Assert.NotNull(claims);
		Assert.Equal("1234567890", claims.Subject);
		Assert.Equal("John Doe", claims.Name);
		Assert.Equal("john.doe@example.com", claims.Email);
		Assert.Equal(123, claims.OrganizationId);
		Assert.Equal("Test Org", claims.OrganizationName);
	}

	[Fact]
	public void ParseIdToken_WithNullToken_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			client.ParseIdToken(null!));
	}

	[Fact]
	public void ParseIdToken_WithEmptyToken_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		Assert.Throws<ArgumentException>(() =>
			client.ParseIdToken(string.Empty));
	}

	[Fact]
	public void ParseIdToken_WithTwoPartToken_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() =>
			client.ParseIdToken(SampleTokenResponses.InvalidIdTokenTwoParts));
		Assert.Contains("Invalid JWT format", exception.Message);
	}

	[Fact]
	public void ParseIdToken_WithFourPartToken_ThrowsArgumentException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() =>
			client.ParseIdToken(SampleTokenResponses.InvalidIdTokenFourParts));
		Assert.Contains("Invalid JWT format", exception.Message);
	}

	[Fact]
	public void ParseIdToken_WithBase64Padding_HandlesCorrectly()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act - ValidIdToken uses Base64 URL encoding (padding is added by the method)
		var claims = client.ParseIdToken(SampleTokenResponses.ValidIdToken);

		// Assert
		Assert.NotNull(claims);
		Assert.NotNull(claims.Subject);
	}

	[Fact]
	public void ParseIdToken_WithBase64UrlEncoding_DecodesCorrectly()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act - ValidIdToken uses Base64 URL encoding (- and _ instead of + and /)
		var claims = client.ParseIdToken(SampleTokenResponses.ValidIdToken);

		// Assert
		Assert.NotNull(claims);
		Assert.Equal("1234567890", claims.Subject);
	}

	[Fact]
	public void ParseIdToken_WithInvalidBase64_ThrowsFormatException()
	{
		// Arrange
		var httpClient = new HttpClient();
		var client = new PlanningCenterOAuthClient(httpClient, _validOptions);

		// Act & Assert
		Assert.ThrowsAny<Exception>(() =>
			client.ParseIdToken(SampleTokenResponses.InvalidIdTokenBadBase64));
	}

	#endregion
}
