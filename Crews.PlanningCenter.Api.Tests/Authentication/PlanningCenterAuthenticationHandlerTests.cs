using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterAuthenticationHandlerTests
{
	[Fact(DisplayName = "Handler adds Personal Access Token authorization when configured")]
	public async Task SendAsync_AddsPersonalAccessToken_WhenConfigured()
	{
		// Arrange
		PlanningCenterPersonalAccessToken token = new()
		{
			AppId = "testAppId",
			Secret = "testSecret"
		};

		PlanningCenterClientOptions options = new()
		{
			PersonalAccessToken = token
		};

		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		TestHttpMessageHandler innerHandler = new();

		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.NotNull(request.Headers.Authorization);
		Assert.Equal("Basic", request.Headers.Authorization.Scheme);
		Assert.Equal(token, request.Headers.Authorization);
	}

	[Fact(DisplayName = "Handler adds OAuth Bearer token when user is authenticated")]
	public async Task SendAsync_AddsBearerToken_WhenUserAuthenticated()
	{
		// Arrange
		string expectedAccessToken = "test-oauth-access-token";
		PlanningCenterClientOptions options = new();

		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		DefaultHttpContext httpContext = new();

		ClaimsIdentity identity = new(
		[
			new Claim(ClaimTypes.Name, "Test User")
		], "TestAuthType");

		AuthenticationProperties properties = new();
		properties.StoreTokens(
		[
			new AuthenticationToken { Name = "access_token", Value = expectedAccessToken }
		]);

		httpContext.User = new ClaimsPrincipal(identity);

		IAuthenticationService authService = Substitute.For<IAuthenticationService>();
		AuthenticateResult authResult = AuthenticateResult.Success(
			new AuthenticationTicket(httpContext.User, properties, "TestScheme"));

		authService.AuthenticateAsync(httpContext, Arg.Any<string>())
			.Returns(authResult);

		httpContext.RequestServices = CreateServiceProvider(authService);
		contextAccessor.HttpContext.Returns(httpContext);

		TestHttpMessageHandler innerHandler = new();
		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.NotNull(request.Headers.Authorization);
		Assert.Equal("Bearer", request.Headers.Authorization.Scheme);
		Assert.Equal(expectedAccessToken, request.Headers.Authorization.Parameter);
	}

	[Fact(DisplayName = "Handler prefers Personal Access Token over OAuth token")]
	public async Task SendAsync_PrefersPersonalAccessToken_OverOAuthToken()
	{
		// Arrange
		PlanningCenterPersonalAccessToken token = new()
		{
			AppId = "testAppId",
			Secret = "testSecret"
		};

		PlanningCenterClientOptions options = new()
		{
			PersonalAccessToken = token
		};

		string expectedAccessToken = "test-oauth-access-token";
		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		DefaultHttpContext httpContext = new();

		ClaimsIdentity identity = new(
		[
			new Claim(ClaimTypes.Name, "Test User")
		], "TestAuthType");

		AuthenticationProperties properties = new();
		properties.StoreTokens(
		[
			new AuthenticationToken { Name = "access_token", Value = expectedAccessToken }
		]);

		httpContext.User = new ClaimsPrincipal(identity);

		IAuthenticationService authService = Substitute.For<IAuthenticationService>();
		AuthenticateResult authResult = AuthenticateResult.Success(
			new AuthenticationTicket(httpContext.User, properties, "TestScheme"));

		authService.AuthenticateAsync(httpContext, Arg.Any<string>())
			.Returns(authResult);

		httpContext.RequestServices = CreateServiceProvider(authService);
		contextAccessor.HttpContext.Returns(httpContext);

		TestHttpMessageHandler innerHandler = new();
		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.NotNull(request.Headers.Authorization);
		Assert.Equal("Basic", request.Headers.Authorization.Scheme);
		Assert.Equal(token, request.Headers.Authorization);
	}

	[Fact(DisplayName = "Handler does not override existing authorization header")]
	public async Task SendAsync_DoesNotOverride_ExistingAuthorizationHeader()
	{
		// Arrange
		PlanningCenterPersonalAccessToken token = new()
		{
			AppId = "testAppId",
			Secret = "testSecret"
		};

		PlanningCenterClientOptions options = new()
		{
			PersonalAccessToken = token
		};

		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		TestHttpMessageHandler innerHandler = new();

		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		AuthenticationHeaderValue customAuth = new("Custom", "CustomToken");
		request.Headers.Authorization = customAuth;

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.NotNull(request.Headers.Authorization);
		Assert.Equal("Custom", request.Headers.Authorization.Scheme);
		Assert.Equal("CustomToken", request.Headers.Authorization.Parameter);
	}

	[Fact(DisplayName = "Handler sends request without authorization when not configured")]
	public async Task SendAsync_NoAuthorization_WhenNotConfigured()
	{
		// Arrange
		PlanningCenterClientOptions options = new();
		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		contextAccessor.HttpContext.Returns((HttpContext?)null);

		TestHttpMessageHandler innerHandler = new();
		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.Null(request.Headers.Authorization);
	}

	[Fact(DisplayName = "Handler handles null HttpContext gracefully")]
	public async Task SendAsync_HandlesNullHttpContext_Gracefully()
	{
		// Arrange
		PlanningCenterClientOptions options = new();
		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		contextAccessor.HttpContext.Returns((HttpContext?)null);

		TestHttpMessageHandler innerHandler = new();
		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		HttpResponseMessage response = await client.SendAsync(request);

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.Null(request.Headers.Authorization);
	}

	[Fact(DisplayName = "Handler handles empty access token gracefully")]
	public async Task SendAsync_HandlesEmptyAccessToken_Gracefully()
	{
		// Arrange
		PlanningCenterClientOptions options = new();
		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		DefaultHttpContext httpContext = new();

		ClaimsIdentity identity = new(
		[
			new Claim(ClaimTypes.Name, "Test User")
		], "TestAuthType");

		AuthenticationProperties properties = new();
		properties.StoreTokens(
		[
			new AuthenticationToken { Name = "access_token", Value = string.Empty }
		]);

		httpContext.User = new ClaimsPrincipal(identity);

		IAuthenticationService authService = Substitute.For<IAuthenticationService>();
		AuthenticateResult authResult = AuthenticateResult.Success(
			new AuthenticationTicket(httpContext.User, properties, "TestScheme"));

		authService.AuthenticateAsync(httpContext, Arg.Any<string>())
			.Returns(authResult);

		httpContext.RequestServices = CreateServiceProvider(authService);
		contextAccessor.HttpContext.Returns(httpContext);

		TestHttpMessageHandler innerHandler = new();
		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.Null(request.Headers.Authorization);
	}

	[Fact(DisplayName = "Handler handles null access token gracefully")]
	public async Task SendAsync_HandlesNullAccessToken_Gracefully()
	{
		// Arrange
		PlanningCenterClientOptions options = new();
		IHttpContextAccessor contextAccessor = Substitute.For<IHttpContextAccessor>();
		DefaultHttpContext httpContext = new();

		ClaimsIdentity identity = new(
		[
			new Claim(ClaimTypes.Name, "Test User")
		], "TestAuthType");

		AuthenticationProperties properties = new();
		// Don't add any access_token - simulates missing token

		httpContext.User = new ClaimsPrincipal(identity);

		IAuthenticationService authService = Substitute.For<IAuthenticationService>();
		AuthenticateResult authResult = AuthenticateResult.Success(
			new AuthenticationTicket(httpContext.User, properties, "TestScheme"));

		authService.AuthenticateAsync(httpContext, Arg.Any<string>())
			.Returns(authResult);

		httpContext.RequestServices = CreateServiceProvider(authService);
		contextAccessor.HttpContext.Returns(httpContext);

		TestHttpMessageHandler innerHandler = new();
		PlanningCenterAuthenticationHandler handler = new(contextAccessor, Options.Create(options))
		{
			InnerHandler = innerHandler
		};

		HttpClient client = new(handler);
		HttpRequestMessage request = new(HttpMethod.Get, "https://api.planningcenteronline.com/test");

		// Act
		await client.SendAsync(request);

		// Assert
		Assert.Null(request.Headers.Authorization);
	}

	private static IServiceProvider CreateServiceProvider(IAuthenticationService authService)
	{
		IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();
		serviceProvider.GetService(typeof(IAuthenticationService)).Returns(authService);
		return serviceProvider;
	}

	private class TestHttpMessageHandler : HttpMessageHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("{\"data\": {}}")
			});
		}
	}
}
