using System.Security.Claims;
using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterTokenHandlerTests
{
    private sealed class CapturingHandler : HttpMessageHandler
    {
        public HttpRequestMessage? LastRequest { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(new HttpResponseMessage());
        }
    }

    private static (HttpClient client, CapturingHandler capture) Build(
        IHttpContextAccessor accessor,
        IPlanningCenterTokenProvider? tokenProvider = null)
    {
        var capture = new CapturingHandler();
        var handler = new PlanningCenterTokenHandler(accessor, tokenProvider)
        {
            InnerHandler = capture
        };
        return (new HttpClient(handler), capture);
    }

    // HttpContext.GetTokenAsync() is an extension that calls IAuthenticationService.AuthenticateAsync,
    // then reads the token from the resulting AuthenticationProperties.
    private static HttpContext CreateHttpContextWithToken(string? token)
    {
        var authService = Substitute.For<IAuthenticationService>();

        var properties = new AuthenticationProperties();
        if (token is not null)
        {
            properties.StoreTokens([new AuthenticationToken { Name = "access_token", Value = token }]);
        }

        var result = AuthenticateResult.Success(
            new AuthenticationTicket(new ClaimsPrincipal(), properties, "Test"));
        authService.AuthenticateAsync(Arg.Any<HttpContext>(), Arg.Any<string?>()).Returns(result);

        return new DefaultHttpContext
        {
            RequestServices = new ServiceCollection()
                .AddSingleton(authService)
                .BuildServiceProvider()
        };
    }

    [Fact(DisplayName = "Token from IPlanningCenterTokenProvider is used as the Bearer token")]
    public async Task SendAsync_WithTokenProvider_SetsAuthorizationHeader()
    {
        var provider = Substitute.For<IPlanningCenterTokenProvider>();
        provider.GetAccessTokenAsync().Returns("provider-token");

        var accessor = Substitute.For<IHttpContextAccessor>();
        var (client, capture) = Build(accessor, provider);

        await client.GetAsync("http://example.com/");

        Assert.Equal("Bearer", capture.LastRequest?.Headers.Authorization?.Scheme);
        Assert.Equal("provider-token", capture.LastRequest?.Headers.Authorization?.Parameter);
    }

    [Fact(DisplayName = "No Authorization header is set when IPlanningCenterTokenProvider returns null")]
    public async Task SendAsync_WithTokenProviderReturningNull_DoesNotSetAuthorizationHeader()
    {
        var provider = Substitute.For<IPlanningCenterTokenProvider>();
        provider.GetAccessTokenAsync().Returns((string?)null);

        var accessor = Substitute.For<IHttpContextAccessor>();
        var (client, capture) = Build(accessor, provider);

        await client.GetAsync("http://example.com/");

        Assert.Null(capture.LastRequest?.Headers.Authorization);
    }

    [Fact(DisplayName = "IPlanningCenterTokenProvider takes precedence over HttpContext")]
    public async Task SendAsync_WithTokenProvider_DoesNotConsultHttpContext()
    {
        var provider = Substitute.For<IPlanningCenterTokenProvider>();
        provider.GetAccessTokenAsync().Returns("provider-token");

        HttpContext httpContext = CreateHttpContextWithToken("context-token");
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(httpContext);
        var (client, capture) = Build(accessor, provider);

        await client.GetAsync("http://example.com/");

        Assert.Equal("provider-token", capture.LastRequest?.Headers.Authorization?.Parameter);
    }

    [Fact(DisplayName = "Token from HttpContext is used when no IPlanningCenterTokenProvider is registered")]
    public async Task SendAsync_WithoutTokenProvider_SetsAuthorizationHeaderFromHttpContext()
    {
        HttpContext httpContext = CreateHttpContextWithToken("context-token");
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(httpContext);
        var (client, capture) = Build(accessor);

        await client.GetAsync("http://example.com/");

        Assert.Equal("Bearer", capture.LastRequest?.Headers.Authorization?.Scheme);
        Assert.Equal("context-token", capture.LastRequest?.Headers.Authorization?.Parameter);
    }

    [Fact(DisplayName = "No Authorization header is set when HttpContext is null (Blazor SignalR circuit)")]
    public async Task SendAsync_WithoutTokenProvider_NullHttpContext_DoesNotSetAuthorizationHeader()
    {
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns((HttpContext?)null);
        var (client, capture) = Build(accessor);

        await client.GetAsync("http://example.com/");

        Assert.Null(capture.LastRequest?.Headers.Authorization);
    }

    [Fact(DisplayName = "No Authorization header is set when HttpContext has no access token")]
    public async Task SendAsync_WithoutTokenProvider_HttpContextHasNoToken_DoesNotSetAuthorizationHeader()
    {
        HttpContext httpContext = CreateHttpContextWithToken(null);
        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext.Returns(httpContext);
        var (client, capture) = Build(accessor);

        await client.GetAsync("http://example.com/");

        Assert.Null(capture.LastRequest?.Headers.Authorization);
    }
}
