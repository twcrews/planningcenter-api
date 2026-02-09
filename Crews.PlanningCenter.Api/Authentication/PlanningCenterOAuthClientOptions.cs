namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Options for configuring the <see cref="PlanningCenterOAuthClient"/>.
/// </summary>
public class PlanningCenterOAuthClientOptions
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
	/// </summary>
	public required string RedirectUri { get; set; }
}
