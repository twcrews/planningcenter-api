using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthClientFactoryTests
{
	private readonly PlanningCenterOAuthClientOptions _validOptions = new()
	{
		ClientId = "test_client_id",
		ClientSecret = "test_client_secret",
		RedirectUri = "https://example.com/callback"
	};

	[Fact]
	public void Create_WithOptions_CreatesClient()
	{
		// Act
		var client = PlanningCenterOAuthClientFactory.Create(_validOptions);

		// Assert
		Assert.NotNull(client);
	}

	[Fact]
	public void Create_WithNullOptions_ThrowsArgumentNullException()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			PlanningCenterOAuthClientFactory.Create(null!));
	}

	[Fact]
	public void Create_WithHttpClientAndOptions_CreatesClient()
	{
		// Arrange
		var httpClient = new HttpClient();

		// Act
		var client = PlanningCenterOAuthClientFactory.Create(httpClient, _validOptions);

		// Assert
		Assert.NotNull(client);
	}

	[Fact]
	public void Create_WithNullHttpClient_ThrowsArgumentNullException()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			PlanningCenterOAuthClientFactory.Create(null!, _validOptions));
	}

	[Fact]
	public void Create_WithNullOptionsWhenHttpClientProvided_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			PlanningCenterOAuthClientFactory.Create(httpClient, null!));
	}

	[Fact]
	public void Create_CreatesNewHttpClientWhenNotProvided()
	{
		// Act
		var client = PlanningCenterOAuthClientFactory.Create(_validOptions);

		// Assert - Client should be created successfully
		Assert.NotNull(client);
	}
}
