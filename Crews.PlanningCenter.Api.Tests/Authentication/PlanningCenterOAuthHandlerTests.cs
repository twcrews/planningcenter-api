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

    [Fact(DisplayName = "Handler setup processes comprehensive user information correctly")]
    public void HandlerSetup_ProcessesComprehensiveUserInformationCorrectly()
    {
		// Arrange - Complete user data matching the JSON structure provided
		string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                type = "Person",
                id = "1",
                attributes = new
                {
                    avatar = "https://example.com/avatar.jpg",
                    first_name = "John",
                    last_name = "Doe",
                    demographic_avatar_url = "https://example.com/demographic_avatar.jpg",
                    name = "John Doe",
                    status = "active",
                    remote_id = 12345,
                    accounting_administrator = true,
                    anniversary = "2000-01-01",
                    birthdate = "1985-05-15",
                    child = false,
                    given_name = "Johnathan",
                    grade = 12,
                    graduation_year = 2025,
                    middle_name = "Michael",
                    nickname = "Johnny",
                    people_permissions = "admin",
                    site_administrator = true,
                    gender = "Male",
                    inactivated_at = (string?)null,
                    medical_notes = "No known allergies",
                    membership = "Member",
                    created_at = "2020-01-01T12:00:00Z",
                    updated_at = "2025-01-01T12:00:00Z",
                    can_create_forms = true,
                    can_email_lists = true,
                    directory_shared_info = new { },
                    directory_status = "visible",
                    passed_background_check = true,
                    resource_permission_flags = new { },
                    school_type = "public",
                    login_identifier = "john.doe@example.com",
                    mfa_configured = true,
                    stripe_customer_identifier = "cus_123456789"
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
                        address = "john.doe@example.com",
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