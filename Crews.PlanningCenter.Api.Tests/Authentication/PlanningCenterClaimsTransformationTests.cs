using Crews.PlanningCenter.Api.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using RichardSzalay.MockHttp;
using System.Security.Claims;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterClaimsTransformationTests
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PlanningCenterClaimsTransformation> _logger;
    private readonly IOptions<PlanningCenterClaimsTransformationOptions> _options;
    private readonly MockHttpMessageHandler _mockHttp;
    private readonly PlanningCenterClaimsTransformation _transformation;

    public PlanningCenterClaimsTransformationTests()
    {
        _mockHttp = new MockHttpMessageHandler();
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(_mockHttp.ToHttpClient());

        _logger = Substitute.For<ILogger<PlanningCenterClaimsTransformation>>();

		PlanningCenterClaimsTransformationOptions transformationOptions = new PlanningCenterClaimsTransformationOptions
        {
            EnableClaimsRefresh = true,
            ClaimsRefreshInterval = TimeSpan.FromMinutes(30)
        };
        _options = Options.Create(transformationOptions);

        _transformation = new PlanningCenterClaimsTransformation(_httpClientFactory, _logger, _options);
    }

    [Fact(DisplayName = "TransformAsync returns original principal for non-Planning Center authentication")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_ForNonPlanningCenterAuth()
    {
		// Arrange
		ClaimsIdentity identity = new ClaimsIdentity("other-scheme");
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
    }

    [Fact(DisplayName = "TransformAsync returns original principal when refresh disabled")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_WhenRefreshDisabled()
    {
		// Arrange
		IOptions<PlanningCenterClaimsTransformationOptions> optionsWithRefreshDisabled = Options.Create(new PlanningCenterClaimsTransformationOptions
        {
            EnableClaimsRefresh = false
        });
		PlanningCenterClaimsTransformation transformation = new PlanningCenterClaimsTransformation(_httpClientFactory, _logger, optionsWithRefreshDisabled);

		ClaimsIdentity identity = new ClaimsIdentity(PlanningCenterOAuthDefaults.AuthenticationScheme);
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
    }

    [Fact(DisplayName = "TransformAsync refreshes claims when no last refresh timestamp exists")]
    public async Task TransformAsync_RefreshesClaims_WhenNoLastRefreshTimestamp()
    {
		// Arrange
		string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "John Updated",
                    first_name = "John",
                    last_name = "Updated",
                    avatar = "https://example.com/new-avatar.jpg",
                    status = "active",
                    site_administrator = false
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

		ClaimsIdentity identity = new ClaimsIdentity(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Updated");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:last_refresh");
    }

    [Fact(DisplayName = "TransformAsync refreshes claims when refresh interval exceeded")]
    public async Task TransformAsync_RefreshesClaims_WhenRefreshIntervalExceeded()
    {
		// Arrange
		string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "John Refreshed",
                    first_name = "John",
                    last_name = "Refreshed",
                    avatar = "https://example.com/refreshed-avatar.jpg",
                    status = "active",
                    site_administrator = true,
                    accounting_administrator = false,
                    birthdate = "1990-05-15",
                    people_permissions = "Editor",
                    gender = "Male",
                    membership = "Member",
                    directory_status = "Listed",
                    passed_background_check = true,
                    resource_permission_flags = "read,write",
                    login_identifier = "john.refreshed@example.com",
                    mfa_configured = true
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

		ClaimsIdentity identity = new ClaimsIdentity(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        // Add old refresh timestamp (2 hours ago, beyond 30 minute interval)
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.AddHours(-2).ToString("O")));
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Refreshed");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:site_administrator" && c.Value == "true");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:accounting_administrator" && c.Value == "false");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.DateOfBirth && c.Value == "1990-05-15");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:people_permissions" && c.Value == "Editor");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Gender && c.Value == "Male");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:mfa_configured" && c.Value == "true");
    }

    [Fact(DisplayName = "TransformAsync does not refresh when within refresh interval")]
    public async Task TransformAsync_DoesNotRefresh_WhenWithinRefreshInterval()
    {
		// Arrange
		ClaimsIdentity identity = new ClaimsIdentity(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
        // Add recent refresh timestamp (10 minutes ago, within 30 minute interval)
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.AddMinutes(-10).ToString("O")));
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "Original Name");
        
        // Verify no HTTP request was made
        _mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact(DisplayName = "TransformAsync handles API failure gracefully")]
    public async Task TransformAsync_HandlesApiFailureGracefully()
    {
        // Arrange
        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond(System.Net.HttpStatusCode.InternalServerError);

		ClaimsIdentity identity = new ClaimsIdentity(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "Original Name");
    }

    [Fact(DisplayName = "TransformAsync handles missing access token gracefully")]
    public async Task TransformAsync_HandlesMissingAccessTokenGracefully()
    {
		// Arrange
		ClaimsIdentity identity = new ClaimsIdentity(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
    }
}

public class PlanningCenterClaimsTransformationOptionsTests
{
    [Fact(DisplayName = "Default options have correct values")]
    public void DefaultOptions_HaveCorrectValues()
    {
		// Act
		PlanningCenterClaimsTransformationOptions options = new PlanningCenterClaimsTransformationOptions();

        // Assert
        Assert.True(options.EnableClaimsRefresh);
        Assert.Equal(TimeSpan.FromHours(1), options.ClaimsRefreshInterval);
    }

    [Fact(DisplayName = "Options can be configured")]
    public void Options_CanBeConfigured()
    {
		// Act
		PlanningCenterClaimsTransformationOptions options = new PlanningCenterClaimsTransformationOptions
        {
            EnableClaimsRefresh = false,
            ClaimsRefreshInterval = TimeSpan.FromMinutes(15)
        };

        // Assert
        Assert.False(options.EnableClaimsRefresh);
        Assert.Equal(TimeSpan.FromMinutes(15), options.ClaimsRefreshInterval);
    }
}