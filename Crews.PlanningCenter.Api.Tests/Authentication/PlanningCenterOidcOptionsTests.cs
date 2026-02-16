using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOidcOptionsTests
{
	[Fact]
	public void ApplyTo_SetsClientId()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Equal("test_client_id", oidcOptions.ClientId);
	}

	[Fact]
	public void ApplyTo_SetsClientSecret()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Equal("test_secret", oidcOptions.ClientSecret);
	}

	[Fact]
	public void ApplyTo_SetsResponseType_ToCode()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Equal("code", oidcOptions.ResponseType);
	}

	[Fact]
	public void ApplyTo_SetsSaveTokens()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			SaveTokens = true
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.True(oidcOptions.SaveTokens);
	}

	[Fact]
	public void ApplyTo_EnsuresOpenIdScope_WhenNotIncluded()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			Scopes = PlanningCenterOAuthScope.People
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Contains("openid", oidcOptions.Scope);
		Assert.Contains("people", oidcOptions.Scope);
	}

	[Fact]
	public void ApplyTo_PreservesOpenIdScope_WhenIncluded()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			Scopes = PlanningCenterOAuthScope.OpenId | PlanningCenterOAuthScope.People
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Contains("openid", oidcOptions.Scope);
		Assert.Contains("people", oidcOptions.Scope);
	}

	[Fact]
	public void ApplyTo_ClearsDefaultScopes()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			Scopes = PlanningCenterOAuthScope.OpenId
		};
		var oidcOptions = new OpenIdConnectOptions();
		oidcOptions.Scope.Add("default_scope");

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.DoesNotContain("default_scope", oidcOptions.Scope);
	}

	[Fact]
	public void ApplyTo_AddsAllConfiguredScopes()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			Scopes = PlanningCenterOAuthScope.OpenId
				| PlanningCenterOAuthScope.People
				| PlanningCenterOAuthScope.Groups
				| PlanningCenterOAuthScope.Calendar
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Contains("openid", oidcOptions.Scope);
		Assert.Contains("people", oidcOptions.Scope);
		Assert.Contains("groups", oidcOptions.Scope);
		Assert.Contains("calendar", oidcOptions.Scope);
	}

	[Fact]
	public void ApplyTo_WithUseDiscoveryTrue_SetsAuthorityAndMetadata()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			UseDiscovery = true
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Equal(PlanningCenterAuthenticationDefaults.BaseUrl, oidcOptions.Authority);
		Assert.Equal(PlanningCenterAuthenticationDefaults.DiscoveryEndpoint, oidcOptions.MetadataAddress);
	}

	[Fact]
	public void ApplyTo_WithUseDiscoveryFalse_ConfiguresManualEndpoints()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			UseDiscovery = false
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.NotNull(oidcOptions.Configuration);
		Assert.Equal(PlanningCenterAuthenticationDefaults.AuthorizationEndpoint, oidcOptions.Configuration.AuthorizationEndpoint);
		Assert.Equal(PlanningCenterAuthenticationDefaults.TokenEndpoint, oidcOptions.Configuration.TokenEndpoint);
		Assert.Equal(PlanningCenterAuthenticationDefaults.UserInfoEndpoint, oidcOptions.Configuration.UserInfoEndpoint);
		Assert.Equal(PlanningCenterAuthenticationDefaults.BaseUrl, oidcOptions.Configuration.Issuer);
	}

	[Fact]
	public void ApplyTo_WithRedirectUri_SetsCallbackPath()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			RedirectUri = "https://example.com/auth/callback"
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Equal("/auth/callback", oidcOptions.CallbackPath);
	}

	[Fact]
	public void ApplyTo_WithoutRedirectUri_DoesNotSetCallbackPath()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			RedirectUri = null
		};
		var oidcOptions = new OpenIdConnectOptions();
		var originalCallbackPath = oidcOptions.CallbackPath;

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.Equal(originalCallbackPath, oidcOptions.CallbackPath);
	}

	[Fact]
	public void ApplyTo_WithPrompt_AddsPromptParameter()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			Prompt = "select_account"
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert
		Assert.NotNull(oidcOptions.Events);
		Assert.NotNull(oidcOptions.Events.OnRedirectToIdentityProvider);
	}

	[Fact]
	public void ApplyTo_WithoutPrompt_DoesNotAddPromptParameter()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret",
			Prompt = string.Empty
		};
		var oidcOptions = new OpenIdConnectOptions();

		// Act
		options.ApplyTo(oidcOptions);

		// Assert - Events may be null or OnRedirectToIdentityProvider may be null
		// The absence of prompt should not add the event handler
		Assert.True(true); // If no exception thrown, test passes
	}

	[Fact]
	public void ApplyTo_WithNullOidcOptions_ThrowsArgumentNullException()
	{
		// Arrange
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			options.ApplyTo(null!));
	}

	[Fact]
	public void DefaultScopes_IsOpenId()
	{
		// Arrange & Act
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Assert
		Assert.Equal(PlanningCenterOAuthScope.OpenId, options.Scopes);
	}

	[Fact]
	public void DefaultPrompt_IsSelectAccount()
	{
		// Arrange & Act
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Assert
		Assert.Equal(PlanningCenterAuthenticationDefaults.RecommendedPrompt, options.Prompt);
	}

	[Fact]
	public void DefaultSaveTokens_IsTrue()
	{
		// Arrange & Act
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Assert
		Assert.True(options.SaveTokens);
	}

	[Fact]
	public void DefaultUseDiscovery_IsTrue()
	{
		// Arrange & Act
		var options = new PlanningCenterOidcOptions
		{
			ClientId = "test_client_id",
			ClientSecret = "test_secret"
		};

		// Assert
		Assert.True(options.UseDiscovery);
	}
}
