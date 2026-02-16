using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthScopeExtensionsTests
{
	[Fact]
	public void ToScopeString_WithNone_ReturnsEmptyString()
	{
		// Arrange
		var scope = PlanningCenterOAuthScope.None;

		// Act
		var result = scope.ToScopeString();

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Theory]
	[InlineData(PlanningCenterOAuthScope.Api, "api")]
	[InlineData(PlanningCenterOAuthScope.Calendar, "calendar")]
	[InlineData(PlanningCenterOAuthScope.CheckIns, "check_ins")]
	[InlineData(PlanningCenterOAuthScope.Giving, "giving")]
	[InlineData(PlanningCenterOAuthScope.Groups, "groups")]
	[InlineData(PlanningCenterOAuthScope.People, "people")]
	[InlineData(PlanningCenterOAuthScope.Publishing, "publishing")]
	[InlineData(PlanningCenterOAuthScope.Registrations, "registrations")]
	[InlineData(PlanningCenterOAuthScope.Services, "services")]
	[InlineData(PlanningCenterOAuthScope.OpenId, "openid")]
	public void ToScopeString_WithSingleScope_ReturnsCorrectName(PlanningCenterOAuthScope scope, string expected)
	{
		// Act
		var result = scope.ToScopeString();

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void ToScopeString_WithMultipleScopes_ReturnsSpaceSeparatedString()
	{
		// Arrange
		var scope = PlanningCenterOAuthScope.People | PlanningCenterOAuthScope.Groups;

		// Act
		var result = scope.ToScopeString();

		// Assert
		Assert.Contains("people", result);
		Assert.Contains("groups", result);
		Assert.Contains(" ", result);
	}

	[Fact]
	public void ToScopeString_WithAllScopes_ReturnsAllNames()
	{
		// Arrange
		var scope = PlanningCenterOAuthScope.Api
			| PlanningCenterOAuthScope.Calendar
			| PlanningCenterOAuthScope.CheckIns
			| PlanningCenterOAuthScope.Giving
			| PlanningCenterOAuthScope.Groups
			| PlanningCenterOAuthScope.People
			| PlanningCenterOAuthScope.Publishing
			| PlanningCenterOAuthScope.Registrations
			| PlanningCenterOAuthScope.Services
			| PlanningCenterOAuthScope.OpenId;

		// Act
		var result = scope.ToScopeString();

		// Assert
		Assert.Contains("api", result);
		Assert.Contains("calendar", result);
		Assert.Contains("check_ins", result);
		Assert.Contains("giving", result);
		Assert.Contains("groups", result);
		Assert.Contains("people", result);
		Assert.Contains("publishing", result);
		Assert.Contains("registrations", result);
		Assert.Contains("services", result);
		Assert.Contains("openid", result);
	}

	[Fact]
	public void ToScopeString_OrderIsConsistent()
	{
		// Arrange
		var scope1 = PlanningCenterOAuthScope.People | PlanningCenterOAuthScope.Groups;
		var scope2 = PlanningCenterOAuthScope.Groups | PlanningCenterOAuthScope.People;

		// Act
		var result1 = scope1.ToScopeString();
		var result2 = scope2.ToScopeString();

		// Assert
		Assert.Equal(result1, result2);
	}

	[Fact]
	public void ParseScopeString_WithNull_ReturnsNone()
	{
		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(null!);

		// Assert
		Assert.Equal(PlanningCenterOAuthScope.None, result);
	}

	[Fact]
	public void ParseScopeString_WithEmpty_ReturnsNone()
	{
		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(string.Empty);

		// Assert
		Assert.Equal(PlanningCenterOAuthScope.None, result);
	}

	[Fact]
	public void ParseScopeString_WithWhitespace_ReturnsNone()
	{
		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString("   ");

		// Assert
		Assert.Equal(PlanningCenterOAuthScope.None, result);
	}

	[Theory]
	[InlineData("api", PlanningCenterOAuthScope.Api)]
	[InlineData("calendar", PlanningCenterOAuthScope.Calendar)]
	[InlineData("check_ins", PlanningCenterOAuthScope.CheckIns)]
	[InlineData("giving", PlanningCenterOAuthScope.Giving)]
	[InlineData("groups", PlanningCenterOAuthScope.Groups)]
	[InlineData("people", PlanningCenterOAuthScope.People)]
	[InlineData("publishing", PlanningCenterOAuthScope.Publishing)]
	[InlineData("registrations", PlanningCenterOAuthScope.Registrations)]
	[InlineData("services", PlanningCenterOAuthScope.Services)]
	[InlineData("openid", PlanningCenterOAuthScope.OpenId)]
	public void ParseScopeString_WithSingleScope_ParsesCorrectly(string scopeString, PlanningCenterOAuthScope expected)
	{
		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void ParseScopeString_WithMultipleScopes_ParsesAllFlags()
	{
		// Arrange
		var scopeString = "people groups";

		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.People));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Groups));
		Assert.Equal(PlanningCenterOAuthScope.People | PlanningCenterOAuthScope.Groups, result);
	}

	[Fact]
	public void ParseScopeString_WithUnknownScope_IgnoresIt()
	{
		// Arrange
		var scopeString = "people unknown_scope groups";

		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.People));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Groups));
		Assert.Equal(PlanningCenterOAuthScope.People | PlanningCenterOAuthScope.Groups, result);
	}

	[Fact]
	public void ParseScopeString_WithMixedKnownAndUnknown_ParsesKnown()
	{
		// Arrange
		var scopeString = "api unknown1 calendar unknown2 people";

		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Api));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Calendar));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.People));
	}

	[Fact]
	public void ParseScopeString_RoundTrip_PreservesScopes()
	{
		// Arrange
		var originalScope = PlanningCenterOAuthScope.Api
			| PlanningCenterOAuthScope.People
			| PlanningCenterOAuthScope.Groups
			| PlanningCenterOAuthScope.OpenId;

		// Act
		var scopeString = originalScope.ToScopeString();
		var parsedScope = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.Equal(originalScope, parsedScope);
	}

	[Fact]
	public void ParseScopeString_WithAllScopes_ParsesAllFlags()
	{
		// Arrange
		var scopeString = "api calendar check_ins giving groups people publishing registrations services openid";

		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Api));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Calendar));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.CheckIns));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Giving));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Groups));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.People));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Publishing));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Registrations));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Services));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.OpenId));
	}

	[Fact]
	public void ParseScopeString_WithExtraSpaces_HandlesCorrectly()
	{
		// Arrange
		var scopeString = "  people   groups  ";

		// Act
		var result = PlanningCenterOAuthScopeExtensions.ParseScopeString(scopeString);

		// Assert
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.People));
		Assert.True(result.HasFlag(PlanningCenterOAuthScope.Groups));
	}
}
