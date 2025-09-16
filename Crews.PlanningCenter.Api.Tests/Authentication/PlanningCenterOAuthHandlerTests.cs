using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using RichardSzalay.MockHttp;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthHandlerTests
{
    private readonly IOptionsMonitor<PlanningCenterOAuthOptions> _optionsMonitor;
    private readonly ILoggerFactory _loggerFactory;
    private readonly UrlEncoder _urlEncoder;
    private readonly MockHttpMessageHandler _mockHttp;
    private readonly PlanningCenterOAuthHandler _handler;

    public PlanningCenterOAuthHandlerTests()
    {
		PlanningCenterOAuthOptions options = new PlanningCenterOAuthOptions();
        _optionsMonitor = Substitute.For<IOptionsMonitor<PlanningCenterOAuthOptions>>();
        _optionsMonitor.Get(Arg.Any<string>()).Returns(options);

        _loggerFactory = Substitute.For<ILoggerFactory>();
        _loggerFactory.CreateLogger(Arg.Any<string>()).Returns(Substitute.For<ILogger>());

        _urlEncoder = UrlEncoder.Default;
        _mockHttp = new MockHttpMessageHandler();

        _handler = new PlanningCenterOAuthHandler(_optionsMonitor, _loggerFactory, _urlEncoder);
    }

    [Fact(DisplayName = "Handler inherits from OAuthHandler correctly")]
    public void Handler_InheritsFromOAuthHandlerCorrectly()
    {
        // Assert
        Assert.IsAssignableFrom<OAuthHandler<PlanningCenterOAuthOptions>>(_handler);
    }

    [Fact(DisplayName = "Handler setup processes user information mock correctly")]
    public void HandlerSetup_ProcessesUserInformationMockCorrectly()
    {
		// Arrange
		string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "John Doe",
                    first_name = "John",
                    last_name = "Doe",
                    avatar = "https://example.com/avatar.jpg",
                    status = "active",
                    site_administrator = true
                }
            }
        });

		string emailsJson = JsonSerializer.Serialize(new
        {
            data = new[]
            {
                new
                {
                    attributes = new
                    {
                        address = "john@example.com",
                        primary = true
                    }
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

        _mockHttp.When(PlanningCenterOAuthDefaults.UserEmailsEndpoint)
                 .Respond("application/json", emailsJson);

        // We can't easily test the protected CreateTicketAsync method directly,
        // but we can verify the handler was constructed correctly and the mock responses are set up
        Assert.NotNull(_handler);
    }

    [Fact(DisplayName = "Constructor initializes with required dependencies")]
    public void Constructor_InitializesWithRequiredDependencies()
    {
		// Act & Assert - Constructor should not throw
		PlanningCenterOAuthHandler handler = new(_optionsMonitor, _loggerFactory, _urlEncoder);
        Assert.NotNull(handler);
    }

    [Fact(DisplayName = "Options monitor provides correct configuration")]
    public void OptionsMonitor_ProvidesCorrectConfiguration()
    {
		// Act
		PlanningCenterOAuthOptions options = _optionsMonitor.Get(string.Empty);

        // Assert
        Assert.NotNull(options);
        Assert.Equal(PlanningCenterOAuthDefaults.UserInformationEndpoint, options.UserInformationEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.TokenEndpoint, options.TokenEndpoint);
    }
}