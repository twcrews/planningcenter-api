using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Options for configuring Planning Center OIDC authentication.
/// </summary>
public class PlanningCenterOidcOptions
{
	/// <summary>
	/// Gets or sets the OAuth client ID for your application.
	/// </summary>
	public required string ClientId { get; set; }

	/// <summary>
	/// Gets or sets the OAuth client secret for your application.
	/// </summary>
	public required string ClientSecret { get; set; }

	/// <summary>
	/// Gets or sets the redirect URI where users will be redirected after authorization.
	/// If not specified, the default callback path will be used.
	/// </summary>
	public string? RedirectUri { get; set; }

	/// <summary>
	/// Gets or sets the scopes to request during authentication.
	/// The <see cref="PlanningCenterOAuthScope.OpenId"/> scope is automatically included.
	/// </summary>
	public PlanningCenterOAuthScope Scopes { get; set; } = PlanningCenterOAuthScope.OpenId;

	/// <summary>
	/// Gets or sets the prompt parameter for the authorization request.
	/// Defaults to "select_account" to allow users to verify their account.
	/// </summary>
	public string Prompt { get; set; } = PlanningCenterAuthenticationDefaults.RecommendedPrompt;

	/// <summary>
	/// Gets or sets a value indicating whether to save tokens in the authentication properties.
	/// Defaults to true.
	/// </summary>
	public bool SaveTokens { get; set; } = true;

	/// <summary>
	/// Gets or sets a value indicating whether to use the OIDC discovery endpoint
	/// to automatically configure endpoints and keys.
	/// Defaults to true.
	/// </summary>
	public bool UseDiscovery { get; set; } = true;

	/// <summary>
	/// Applies these options to an <see cref="OpenIdConnectOptions"/> instance.
	/// </summary>
	/// <param name="oidcOptions">The OpenID Connect options to configure.</param>
	public void ApplyTo(OpenIdConnectOptions oidcOptions)
	{
		ArgumentNullException.ThrowIfNull(oidcOptions);

		oidcOptions.ClientId = ClientId;
		oidcOptions.ClientSecret = ClientSecret;
		oidcOptions.ResponseType = "code";
		oidcOptions.SaveTokens = SaveTokens;

		// Ensure openid scope is included
		var scopes = Scopes;
		if (!scopes.HasFlag(PlanningCenterOAuthScope.OpenId))
		{
			scopes |= PlanningCenterOAuthScope.OpenId;
		}

		// Clear default scopes and add our scopes
		oidcOptions.Scope.Clear();
		foreach (var scope in scopes.ToScopeString().Split(' ', StringSplitOptions.RemoveEmptyEntries))
		{
			oidcOptions.Scope.Add(scope);
		}

		if (UseDiscovery)
		{
			oidcOptions.Authority = PlanningCenterAuthenticationDefaults.BaseUrl;
			oidcOptions.MetadataAddress = PlanningCenterAuthenticationDefaults.DiscoveryEndpoint;
		}
		else
		{
			// Manual configuration
			oidcOptions.Authority = PlanningCenterAuthenticationDefaults.BaseUrl;
			oidcOptions.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration
			{
				AuthorizationEndpoint = PlanningCenterAuthenticationDefaults.AuthorizationEndpoint,
				TokenEndpoint = PlanningCenterAuthenticationDefaults.TokenEndpoint,
				UserInfoEndpoint = PlanningCenterAuthenticationDefaults.UserInfoEndpoint,
				Issuer = PlanningCenterAuthenticationDefaults.BaseUrl
			};
		}

		// Set redirect URI if specified
		if (!string.IsNullOrWhiteSpace(RedirectUri))
		{
			oidcOptions.CallbackPath = new Uri(RedirectUri).AbsolutePath;
		}

		// Add prompt parameter
		if (!string.IsNullOrWhiteSpace(Prompt))
		{
			oidcOptions.Events ??= new OpenIdConnectEvents();
			var existingOnRedirectToIdentityProvider = oidcOptions.Events.OnRedirectToIdentityProvider;
			oidcOptions.Events.OnRedirectToIdentityProvider = context =>
			{
				context.ProtocolMessage.SetParameter("prompt", Prompt);
				return existingOnRedirectToIdentityProvider?.Invoke(context) ?? Task.CompletedTask;
			};
		}
	}
}
