namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Extension methods for <see cref="PlanningCenterOAuthScope"/>.
/// </summary>
public static class PlanningCenterOAuthScopeExtensions
{
	private static readonly Dictionary<PlanningCenterOAuthScope, string> ScopeNames = new()
	{
		{ PlanningCenterOAuthScope.Api, "api" },
		{ PlanningCenterOAuthScope.Calendar, "calendar" },
		{ PlanningCenterOAuthScope.CheckIns, "check_ins" },
		{ PlanningCenterOAuthScope.Giving, "giving" },
		{ PlanningCenterOAuthScope.Groups, "groups" },
		{ PlanningCenterOAuthScope.People, "people" },
		{ PlanningCenterOAuthScope.Publishing, "publishing" },
		{ PlanningCenterOAuthScope.Registrations, "registrations" },
		{ PlanningCenterOAuthScope.Services, "services" },
		{ PlanningCenterOAuthScope.OpenId, "openid" }
	};

	/// <summary>
	/// Converts the scope flags to a space-separated string suitable for OAuth requests.
	/// </summary>
	/// <param name="scopes">The scopes to convert.</param>
	/// <returns>A space-separated string of scope names.</returns>
	public static string ToScopeString(this PlanningCenterOAuthScope scopes)
	{
		if (scopes == PlanningCenterOAuthScope.None)
		{
			return string.Empty;
		}

		var scopeList = new List<string>();

		foreach (var (scope, name) in ScopeNames)
		{
			if (scopes.HasFlag(scope))
			{
				scopeList.Add(name);
			}
		}

		return string.Join(' ', scopeList);
	}

	/// <summary>
	/// Parses a space-separated scope string into <see cref="PlanningCenterOAuthScope"/> flags.
	/// </summary>
	/// <param name="scopeString">The space-separated scope string.</param>
	/// <returns>The parsed scope flags.</returns>
	public static PlanningCenterOAuthScope ParseScopeString(string scopeString)
	{
		if (string.IsNullOrWhiteSpace(scopeString))
		{
			return PlanningCenterOAuthScope.None;
		}

		var result = PlanningCenterOAuthScope.None;
		var scopeNames = scopeString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

		foreach (var scopeName in scopeNames)
		{
			var matchingScope = ScopeNames.FirstOrDefault(kvp => kvp.Value == scopeName).Key;
			if (matchingScope != PlanningCenterOAuthScope.None)
			{
				result |= matchingScope;
			}
		}

		return result;
	}
}
