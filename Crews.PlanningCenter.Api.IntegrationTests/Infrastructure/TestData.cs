using System.Text.RegularExpressions;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Conventions that identify records created by integration tests, so that <see cref="TestDataSweeper"/> can find
/// them later without any local state.
/// </summary>
public static class TestData
{
	// Deliberately strict: anything matching this is eligible for deletion from the live account.
	private static readonly Regex TagPattern = new(
		@"^(?:fixture(?:-| person-)|inttest(?:[- ]|$)|tempmember inttest-)",
		RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

	private static readonly Regex IdPattern = new(
		"(?<![0-9a-f])[0-9a-f]{12}(?![0-9a-f])",
		RegexOptions.CultureInvariant);

	/// <summary>
	/// Creates a unique ID for naming test data. The first eight hex digits are the creation time in Unix seconds, so
	/// the age of a record can be determined from its name even if the resource has no <c>created_at</c> attribute.
	/// </summary>
	public static string NewId() => $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds():x8}{Guid.NewGuid():N}"[..12];

	/// <summary>Determines whether a value marks its record as test data.</summary>
	public static bool IsTagged(string? value) => value is not null && TagPattern.IsMatch(value);

	/// <summary>Gets the creation time embedded in a value by <see cref="NewId"/>, or null if there is none.</summary>
	public static DateTimeOffset? GetCreatedAt(string? value)
	{
		if (value is null) return null;

		var match = IdPattern.Match(value);
		return match.Success
			? DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(match.Value[..8], 16))
			: null;
	}
}
