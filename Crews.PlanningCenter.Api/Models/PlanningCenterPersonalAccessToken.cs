using System.Net.Http.Headers;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Represents a developer personal access token for the Planning Center API.
/// </summary>
public class PlanningCenterPersonalAccessToken
{
	/// <summary>
	/// The App ID component of the personal access token.
	/// </summary>
	public required string AppID { get; set; }

	/// <summary>
	/// The Secret component of the personal access token.
	/// </summary>
	public required string Secret { get; set; }

	/// <summary>
	/// Implicitly converts the current <see cref="PlanningCenterPersonalAccessToken"/> instance to an
	/// <see cref="AuthenticationHeaderValue"/> instance.
	/// </summary>
	/// <param name="token">The current instance.</param>
	public static implicit operator AuthenticationHeaderValue(PlanningCenterPersonalAccessToken token)
		=> new("Basic", $"{token.AppID}:{token.Secret}".Base64Encode());
}