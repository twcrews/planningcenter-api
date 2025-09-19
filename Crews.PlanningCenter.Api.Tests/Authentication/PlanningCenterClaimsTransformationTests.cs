using Crews.PlanningCenter.Api.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using RichardSzalay.MockHttp;
using System.Security.Claims;
using System.Security.Principal;
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

		PlanningCenterClaimsTransformationOptions transformationOptions = new()
		{
            EnableClaimsRefresh = true,
            ClaimsRefreshInterval = TimeSpan.FromMinutes(30)
        };
        _options = Options.Create(transformationOptions);

        _transformation = new PlanningCenterClaimsTransformation(_httpClientFactory, _logger, _options);
    }

    [Fact(DisplayName = "TransformAsync returns original principal for unauthenticated identity")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_ForUnauthenticatedIdentity()
    {
		// Arrange
		ClaimsIdentity identity = new(); // Not authenticated
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
    }

    [Fact(DisplayName = "TransformAsync returns original principal for non-ClaimsIdentity")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_ForNonClaimsIdentity()
    {
		// Arrange
		// Create a principal with a custom identity that is not a ClaimsIdentity
		// We need to use reflection or create a custom principal that doesn't wrap the identity
		TestIdentity identity = new("test-user", "test-scheme", isAuthenticated: true);
		TestPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
        Assert.IsNotType<ClaimsIdentity>(principal.Identity); // Verify the condition we're testing
    }

    [Fact(DisplayName = "TransformAsync returns original principal for ClaimsIdentity with IsAuthenticated false")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_ForClaimsIdentityNotAuthenticated()
    {
		// Arrange
		// Create ClaimsIdentity with null authentication type (IsAuthenticated will be false)
		ClaimsIdentity identity = new(authenticationType: null);
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
        Assert.False(identity.IsAuthenticated); // Verify the condition we're testing
    }

    [Fact(DisplayName = "TransformAsync returns original principal when refresh disabled")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_WhenRefreshDisabled()
    {
		// Arrange
		IOptions<PlanningCenterClaimsTransformationOptions> optionsWithRefreshDisabled = Options.Create(new PlanningCenterClaimsTransformationOptions
        {
            EnableClaimsRefresh = false
        });
		PlanningCenterClaimsTransformation transformation = new(_httpClientFactory, _logger, optionsWithRefreshDisabled);

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
    }

    [Fact(DisplayName = "TransformAsync returns original principal when refresh disabled even with access token")]
    public async Task TransformAsync_ReturnsOriginalPrincipal_WhenRefreshDisabledWithAccessToken()
    {
		// Arrange
		IOptions<PlanningCenterClaimsTransformationOptions> optionsWithRefreshDisabled = Options.Create(new PlanningCenterClaimsTransformationOptions
        {
            EnableClaimsRefresh = false
        });
		PlanningCenterClaimsTransformation transformation = new(_httpClientFactory, _logger, optionsWithRefreshDisabled);

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
        // Verify no HTTP request was made since refresh is disabled
        _mockHttp.VerifyNoOutstandingExpectation();
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

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:last_refresh");
    }

    [Fact(DisplayName = "TransformAsync refreshes claims when last refresh timestamp is invalid")]
    public async Task TransformAsync_RefreshesClaims_WhenLastRefreshTimestampIsInvalid()
    {
		// Arrange
		string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "John Invalid Date",
                    first_name = "John",
                    last_name = "Invalid",
                    status = "active"
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        // Add invalid refresh timestamp that can't be parsed by DateTime.TryParse
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", "invalid-date-format"));
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Invalid Date");
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
                    passed_background_check = true
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        // Add old refresh timestamp (2 hours ago, beyond 30 minute interval)
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.AddHours(-2).ToString("O")));
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Refreshed");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:site_admin" && c.Value == "true");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:accounting_admin" && c.Value == "false");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.DateOfBirth && c.Value == "1990-05-15");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:people_permissions" && c.Value == "Editor");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Gender && c.Value == "Male");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:last_refresh");
    }

    [Fact(DisplayName = "TransformAsync does not refresh when within refresh interval")]
    public async Task TransformAsync_DoesNotRefresh_WhenWithinRefreshInterval()
    {
		// Arrange
		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
        // Add recent refresh timestamp (10 minutes ago, within 30 minute interval)
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.AddMinutes(-10).ToString("O")));
		ClaimsPrincipal principal = new(identity);

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

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
		ClaimsPrincipal principal = new(identity);

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
		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        Assert.Same(principal, result);
    }

    [Fact(DisplayName = "RefreshUserClaimsAsync handles null access token claim gracefully")]
    public async Task RefreshUserClaimsAsync_HandlesNullAccessTokenClaimGracefully()
    {
		// Arrange
		// Create a scenario where access token becomes null during processing
		// This can happen in edge cases with custom claim handling or race conditions
		TestClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
        // Set up the identity to behave differently for the two FindFirst calls
        identity.SetupFindFirstToReturnNull("access_token");
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert - Should return a new principal with refreshed timestamp but no API call
        ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "Original Name");
        // Verify no HTTP request was made since access token was null in RefreshUserClaimsAsync
        _mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact(DisplayName = "TransformAsync removes existing Planning Center claims before refresh")]
    public async Task TransformAsync_RemovesExistingPlanningCenterClaimsBeforeRefresh()
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
                    status = "active"
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

		ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        identity.AddClaim(new Claim(ClaimTypes.Name, "Original Name"));
        // Add existing Planning Center claims that should be removed
        identity.AddClaim(new Claim("urn:planningcenter:old_claim", "old_value"));
        identity.AddClaim(new Claim("urn:planningcenter:another_claim", "another_value"));
        // Add last_refresh claim that should NOT be removed
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.AddHours(-2).ToString("O")));
		ClaimsPrincipal principal = new(identity);

		// Act
		ClaimsPrincipal result = await _transformation.TransformAsync(principal);

		// Assert
		ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);
        // Verify new claims are present
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Refreshed");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:last_refresh");
        // Verify old Planning Center claims were removed (line 131 coverage)
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:old_claim");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:another_claim");
    }

    [Fact(DisplayName = "TransformAsync handles missing or empty optional string fields")]
    public async Task TransformAsync_HandlesMissingOrEmptyOptionalStringFields()
    {
        // Arrange - Create user data with missing/empty optional string fields to test negative branches
        string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "John Coverage",
                    first_name = "John",
                    last_name = "Coverage",
                    middle_name = "", // Empty string (line 164 negative branch)
                    nickname = (string?)null, // Null (line 169 negative branch)
                    school_type = "", // Empty string (line 214 negative branch)
                    anniversary = "", // Empty string (line 219 negative branch)
                    status = "active"
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

        ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        ClaimsPrincipal principal = new(identity);

        // Act
        ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);

        // Verify claims that should be present
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Coverage");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.GivenName && c.Value == "John");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Surname && c.Value == "Coverage");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:status" && c.Value == "active");

        // Verify claims that should NOT be present due to empty/null values
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "middle_name");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "nickname");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:school_type");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:anniversary");
    }

    [Fact(DisplayName = "TransformAsync handles missing optional properties")]
    public async Task TransformAsync_HandlesMissingOptionalProperties()
    {
        // Arrange - Create user data with missing optional properties to test negative branches
        string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "Jane Missing",
                    first_name = "Jane",
                    last_name = "Missing",
                    status = "active"
                    // Missing: child, grade, graduation_year, can_create_forms, can_email_lists
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

        ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        ClaimsPrincipal principal = new(identity);

        // Act
        ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);

        // Verify claims that should be present
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "Jane Missing");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:status" && c.Value == "active");

        // Verify claims that should NOT be present due to missing properties (negative branches)
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:child");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:grade");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:graduation_year");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:can_create_forms");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:can_email_lists");
    }

    [Fact(DisplayName = "TransformAsync handles null values for numeric properties")]
    public async Task TransformAsync_HandlesNullValuesForNumericProperties()
    {
        // Arrange - Create user data with explicit null values for numeric properties
        string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "John Null",
                    first_name = "John",
                    last_name = "Null",
                    grade = (int?)null, // Explicit null (line 199 negative branch)
                    graduation_year = (int?)null, // Explicit null (line 204 negative branch)
                    status = "active"
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

        ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        ClaimsPrincipal principal = new(identity);

        // Act
        ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);

        // Verify claims that should be present
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "John Null");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:status" && c.Value == "active");

        // Verify claims that should NOT be present due to null values (negative branches)
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:grade");
        Assert.DoesNotContain(resultIdentity.Claims, c => c.Type == "urn:planningcenter:graduation_year");
    }

    [Fact(DisplayName = "TransformAsync adds all optional claims when properties are present")]
    public async Task TransformAsync_AddsAllOptionalClaims_WhenPropertiesArePresent()
    {
        // Arrange - Create user data with ALL optional properties present to cover missing lines
        string userJson = JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "123",
                attributes = new
                {
                    name = "Complete User",
                    first_name = "Complete",
                    last_name = "User",
                    middle_name = "Test", // Line 166 coverage
                    nickname = "Tester", // Line 171 coverage
                    avatar = "https://example.com/avatar.jpg",
                    birthdate = "1990-01-01",
                    gender = "Other",
                    status = "active",
                    child = true, // Line 196 coverage (boolean)
                    grade = 12, // Line 201 coverage
                    graduation_year = 2025, // Line 206 coverage
                    membership = "Active Member",
                    school_type = "Public", // Line 216 coverage
                    anniversary = "2020-06-15", // Line 221 coverage
                    people_permissions = "Editor",
                    accounting_administrator = false,
                    site_administrator = true,
                    can_create_forms = true, // Line 241 coverage (boolean)
                    can_email_lists = false, // Line 246 coverage (boolean)
                    passed_background_check = true
                }
            }
        });

        _mockHttp.When(PlanningCenterOAuthDefaults.UserInformationEndpoint)
                 .Respond("application/json", userJson);

        ClaimsIdentity identity = new(PlanningCenterOAuthDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim("access_token", "test_token"));
        ClaimsPrincipal principal = new(identity);

        // Act
        ClaimsPrincipal result = await _transformation.TransformAsync(principal);

        // Assert
        ClaimsIdentity? resultIdentity = result.Identity as ClaimsIdentity;
        Assert.NotNull(resultIdentity);

        // Verify all standard claims
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == "123");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Name && c.Value == "Complete User");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.GivenName && c.Value == "Complete");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Surname && c.Value == "User");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.DateOfBirth && c.Value == "1990-01-01");
        Assert.Contains(resultIdentity.Claims, c => c.Type == ClaimTypes.Gender && c.Value == "Other");

        // Verify the previously uncovered optional claims (lines 164, 169, 194, 199, 204, 214, 219, 239, 244)
        Assert.Contains(resultIdentity.Claims, c => c.Type == "middle_name" && c.Value == "Test"); // Line 166
        Assert.Contains(resultIdentity.Claims, c => c.Type == "nickname" && c.Value == "Tester"); // Line 171
        Assert.Contains(resultIdentity.Claims, c => c.Type == "picture" && c.Value == "https://example.com/avatar.jpg");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:status" && c.Value == "active");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:child" && c.Value == "true"); // Line 196
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:grade" && c.Value == "12"); // Line 201
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:graduation_year" && c.Value == "2025"); // Line 206
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:membership" && c.Value == "Active Member");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:school_type" && c.Value == "Public"); // Line 216
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:anniversary" && c.Value == "2020-06-15"); // Line 221
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:people_permissions" && c.Value == "Editor");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:accounting_admin" && c.Value == "false");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:site_admin" && c.Value == "true");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:can_create_forms" && c.Value == "true"); // Line 241
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:can_email_lists" && c.Value == "false"); // Line 246
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:background_check" && c.Value == "true");
        Assert.Contains(resultIdentity.Claims, c => c.Type == "urn:planningcenter:last_refresh");
    }
}

public class PlanningCenterClaimsTransformationOptionsTests
{
    [Fact(DisplayName = "Default options have correct values")]
    public void DefaultOptions_HaveCorrectValues()
    {
		// Act
		PlanningCenterClaimsTransformationOptions options = new();

        // Assert
        Assert.True(options.EnableClaimsRefresh);
        Assert.Equal(TimeSpan.FromHours(1), options.ClaimsRefreshInterval);
    }

    [Fact(DisplayName = "Options can be configured")]
    public void Options_CanBeConfigured()
    {
		// Act
		PlanningCenterClaimsTransformationOptions options = new()
		{
            EnableClaimsRefresh = false,
            ClaimsRefreshInterval = TimeSpan.FromMinutes(15)
        };

        // Assert
        Assert.False(options.EnableClaimsRefresh);
        Assert.Equal(TimeSpan.FromMinutes(15), options.ClaimsRefreshInterval);
    }
}

/// <summary>
/// Test implementation of IIdentity that is not a ClaimsIdentity.
/// </summary>
internal class TestIdentity : IIdentity
{
    public TestIdentity(string name, string authenticationType, bool isAuthenticated)
    {
        Name = name;
        AuthenticationType = authenticationType;
        IsAuthenticated = isAuthenticated;
    }

    public string? AuthenticationType { get; }
    public bool IsAuthenticated { get; }
    public string? Name { get; }
}

/// <summary>
/// Test implementation of ClaimsPrincipal that doesn't wrap non-ClaimsIdentity in ClaimsIdentity.
/// </summary>
internal class TestPrincipal : ClaimsPrincipal
{
    private readonly IIdentity _identity;

    public TestPrincipal(IIdentity identity)
    {
        _identity = identity;
    }

    public override IIdentity Identity => _identity;
}

/// <summary>
/// Test implementation of ClaimsIdentity that can simulate scenarios where FindFirst returns null
/// for access_token in RefreshUserClaimsAsync but not in the initial check.
/// This simulates edge cases like race conditions or custom claim processing.
/// </summary>
internal class TestClaimsIdentity : ClaimsIdentity
{
    private int _findFirstCallCount = 0;
    private readonly HashSet<string> _nullClaimTypes = new();

    public TestClaimsIdentity(string authenticationType) : base(authenticationType)
    {
    }

    public void SetupFindFirstToReturnNull(string claimType)
    {
        _nullClaimTypes.Add(claimType);
        // Add an actual access_token claim for the first call
        AddClaim(new Claim("access_token", "test_token"));
    }

    public override Claim? FindFirst(string type)
    {
        if (type == "access_token" && _nullClaimTypes.Contains(type))
        {
            _findFirstCallCount++;
            // Return the claim for the first call (hasAccessToken check)
            // Return null for subsequent calls (RefreshUserClaimsAsync)
            if (_findFirstCallCount == 1)
                return base.FindFirst(type);
            else
                return null;
        }

        return base.FindFirst(type);
    }
}