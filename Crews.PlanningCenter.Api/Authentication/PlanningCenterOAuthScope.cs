namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Represents the available OAuth 2.0 scopes for the Planning Center API.
/// </summary>
[Flags]
public enum PlanningCenterOAuthScope
{
	/// <summary>
	/// No scopes selected.
	/// </summary>
	None = 0,

	/// <summary>
	/// Access to the API product.
	/// </summary>
	Api = 1 << 0,

	/// <summary>
	/// Access to the Calendar product.
	/// </summary>
	Calendar = 1 << 1,

	/// <summary>
	/// Access to the Check-Ins product.
	/// </summary>
	CheckIns = 1 << 2,

	/// <summary>
	/// Access to the Giving product.
	/// </summary>
	Giving = 1 << 3,

	/// <summary>
	/// Access to the Groups product.
	/// </summary>
	Groups = 1 << 4,

	/// <summary>
	/// Access to the People product.
	/// </summary>
	People = 1 << 5,

	/// <summary>
	/// Access to the Publishing product.
	/// </summary>
	Publishing = 1 << 6,

	/// <summary>
	/// Access to the Registrations product.
	/// </summary>
	Registrations = 1 << 7,

	/// <summary>
	/// Access to the Services product.
	/// </summary>
	Services = 1 << 8,

	/// <summary>
	/// OpenID Connect scope for authentication and identity claims.
	/// Required for OIDC flows to receive an ID token.
	/// </summary>
	OpenId = 1 << 9
}
