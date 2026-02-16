using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterAuthenticationExtensionsTests
{
	[Fact]
	public void AddPlanningCenterAuthentication_WithValidConfiguration_RegistersServices()
	{
		// Arrange
		var services = new ServiceCollection();

		// Act
		services.AddPlanningCenterAuthentication(options =>
		{
			options.ClientId = "test_client_id";
			options.ClientSecret = "test_secret";
		});

		// Assert
		var provider = services.BuildServiceProvider();
		Assert.NotNull(provider);
	}

	[Fact]
	public void AddPlanningCenterAuthentication_RegistersClaimsTransformation()
	{
		// Arrange
		var services = new ServiceCollection();

		// Act
		services.AddPlanningCenterAuthentication(options =>
		{
			options.ClientId = "test_client_id";
			options.ClientSecret = "test_secret";
		});

		// Assert
		Assert.Contains(services, sd => sd.ServiceType == typeof(IClaimsTransformation));
	}

	[Fact]
	public void AddPlanningCenterAuthentication_WithNullServices_ThrowsArgumentNullException()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			services.AddPlanningCenterAuthentication(options =>
			{
				options.ClientId = "test";
				options.ClientSecret = "test";
			}));
	}

	[Fact]
	public void AddPlanningCenterAuthentication_WithNullConfigure_ThrowsArgumentNullException()
	{
		// Arrange
		var services = new ServiceCollection();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			services.AddPlanningCenterAuthentication(null!));
	}

	[Fact]
	public void AddPlanningCenterAuthentication_ReturnsServiceCollection()
	{
		// Arrange
		var services = new ServiceCollection();

		// Act
		var result = services.AddPlanningCenterAuthentication(options =>
		{
			options.ClientId = "test_client_id";
			options.ClientSecret = "test_secret";
		});

		// Assert
		Assert.Same(services, result);
	}

	[Fact]
	public void AddPlanningCenterOidc_WithAction_ConfiguresOidc()
	{
		// Arrange
		var services = new ServiceCollection();
		var builder = services.AddAuthentication();

		// Act
		var result = builder.AddPlanningCenterOidc(options =>
		{
			options.ClientId = "test_client_id";
			options.ClientSecret = "test_secret";
		});

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public void AddPlanningCenterOidc_WithNullBuilder_ThrowsArgumentNullException()
	{
		// Arrange
		AuthenticationBuilder builder = null!;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			builder.AddPlanningCenterOidc(options =>
			{
				options.ClientId = "test";
				options.ClientSecret = "test";
			}));
	}

	[Fact]
	public void AddPlanningCenterOidc_WithNullAction_ThrowsArgumentNullException()
	{
		// Arrange
		var services = new ServiceCollection();
		var builder = services.AddAuthentication();
		Action<PlanningCenterOidcOptions> configure = null!;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			builder.AddPlanningCenterOidc(configure));
	}

	[Fact]
	public void AddPlanningCenterOidc_RegistersClaimsTransformation()
	{
		// Arrange
		var services = new ServiceCollection();
		var builder = services.AddAuthentication();

		// Act
		builder.AddPlanningCenterOidc(options =>
		{
			options.ClientId = "test_client_id";
			options.ClientSecret = "test_secret";
		});

		// Assert
		Assert.Contains(services, sd => sd.ServiceType == typeof(IClaimsTransformation));
	}

	[Fact]
	public void AddPlanningCenterOidc_ReturnsAuthenticationBuilder()
	{
		// Arrange
		var services = new ServiceCollection();
		var builder = services.AddAuthentication();

		// Act
		var result = builder.AddPlanningCenterOidc(options =>
		{
			options.ClientId = "test_client_id";
			options.ClientSecret = "test_secret";
		});

		// Assert
		Assert.IsType<AuthenticationBuilder>(result);
	}

	[Fact]
	public void AddPlanningCenterOidc_WithOptions_ConfiguresOidc()
	{
		// Arrange
		var services = new ServiceCollection();
		var builder = services.AddAuthentication();
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Act
		var result = builder.AddPlanningCenterOidc(options);

		// Assert
		Assert.NotNull(result);
	}

	[Fact]
	public void AddPlanningCenterOidc_WithNullBuilderWhenOptionsProvided_ThrowsArgumentNullException()
	{
		// Arrange
		AuthenticationBuilder builder = null!;
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			builder.AddPlanningCenterOidc(options));
	}

	[Fact]
	public void AddPlanningCenterOidc_WithNullOptions_ThrowsArgumentNullException()
	{
		// Arrange
		var services = new ServiceCollection();
		var builder = services.AddAuthentication();
		PlanningCenterOidcOptions options = null!;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			builder.AddPlanningCenterOidc(options));
	}
}
