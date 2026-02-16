using System.Net.Http.Headers;
using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class HttpClientAuthenticationExtensionsTests
{
	[Fact]
	public void AddPlanningCenterAuth_WithPersonalAccessToken_SetsAuthorizationHeader()
	{
		// Arrange
		var client = new HttpClient();
		var token = new PlanningCenterPersonalAccessToken
		{
			AppId = "test-app",
			Secret = "test-secret"
		};

		// Act
		client.AddPlanningCenterAuth(token);

		// Assert
		Assert.NotNull(client.DefaultRequestHeaders.Authorization);
		Assert.Equal("Basic", client.DefaultRequestHeaders.Authorization.Scheme);
	}

	[Fact]
	public void AddPlanningCenterAuth_WithBearerToken_SetsAuthorizationHeader()
	{
		// Arrange
		var client = new HttpClient();
		var bearerToken = "test-bearer-token";

		// Act
		client.AddPlanningCenterAuth(bearerToken);

		// Assert
		Assert.NotNull(client.DefaultRequestHeaders.Authorization);
		Assert.Equal("Bearer", client.DefaultRequestHeaders.Authorization.Scheme);
		Assert.Equal(bearerToken, client.DefaultRequestHeaders.Authorization.Parameter);
	}

	[Fact]
	public void AddPlanningCenterAuth_WithNullClient_ThrowsArgumentNullException()
	{
		// Arrange
		HttpClient? client = null;
		var token = new PlanningCenterPersonalAccessToken
		{
			AppId = "test-app",
			Secret = "test-secret"
		};

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => client!.AddPlanningCenterAuth(token));
	}

	[Fact]
	public void AddPlanningCenterAuth_WithNullBearerToken_ThrowsArgumentNullException()
	{
		// Arrange
		var client = new HttpClient();
		string? bearerToken = null;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => client.AddPlanningCenterAuth(bearerToken!));
	}

	[Fact]
	public void AddPlanningCenterAuth_WithEmptyBearerToken_ThrowsArgumentException()
	{
		// Arrange
		var client = new HttpClient();
		var bearerToken = string.Empty;

		// Act & Assert
		Assert.Throws<ArgumentException>(() => client.AddPlanningCenterAuth(bearerToken));
	}

	[Fact]
	public void ConfigureForPlanningCenter_SetsBaseAddressAndAcceptHeader()
	{
		// Arrange
		var client = new HttpClient();

		// Act
		client.ConfigureForPlanningCenter();

		// Assert
		Assert.NotNull(client.BaseAddress);
		Assert.Equal("https://api.planningcenteronline.com/", client.BaseAddress.ToString());
		Assert.Contains(client.DefaultRequestHeaders.Accept,
			h => h.MediaType == "application/vnd.api+json");
	}

	[Fact]
	public void ConfigureForPlanningCenter_WithCustomBaseUrl_SetsCustomBaseAddress()
	{
		// Arrange
		var client = new HttpClient();
		var customBaseUrl = "https://custom.example.com";

		// Act
		client.ConfigureForPlanningCenter(customBaseUrl);

		// Assert
		Assert.NotNull(client.BaseAddress);
		Assert.Equal($"{customBaseUrl}/", client.BaseAddress.ToString());
	}

	[Fact]
	public void ConfigureForPlanningCenter_WithNullClient_ThrowsArgumentNullException()
	{
		// Arrange
		HttpClient? client = null;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => client!.ConfigureForPlanningCenter());
	}

	[Fact]
	public void ConfigureForPlanningCenter_WithNullBaseUrl_ThrowsArgumentNullException()
	{
		// Arrange
		var client = new HttpClient();
		string? baseUrl = null;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => client.ConfigureForPlanningCenter(baseUrl!));
	}

	[Fact]
	public void ConfigureForPlanningCenter_WithEmptyBaseUrl_ThrowsArgumentException()
	{
		// Arrange
		var client = new HttpClient();
		var baseUrl = string.Empty;

		// Act & Assert
		Assert.Throws<ArgumentException>(() => client.ConfigureForPlanningCenter(baseUrl));
	}

	[Fact]
	public void HttpClientExtensions_CanBeChained()
	{
		// Arrange
		var client = new HttpClient();
		var token = new PlanningCenterPersonalAccessToken
		{
			AppId = "test-app",
			Secret = "test-secret"
		};

		// Act
		var result = client
			.ConfigureForPlanningCenter()
			.AddPlanningCenterAuth(token);

		// Assert
		Assert.Same(client, result);
		Assert.NotNull(client.BaseAddress);
		Assert.NotNull(client.DefaultRequestHeaders.Authorization);
	}
}
