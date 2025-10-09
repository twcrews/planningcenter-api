using Crews.PlanningCenter.Auth.Models;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthScopeTests
{
	[Fact(DisplayName = "Calendar scope has correct value")]
	public void Calendar_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Calendar;

		// Assert
		Assert.Equal("calendar", value);
	}

	[Fact(DisplayName = "CheckIns scope has correct value")]
	public void CheckIns_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.CheckIns;

		// Assert
		Assert.Equal("check_ins", value);
	}

	[Fact(DisplayName = "Giving scope has correct value")]
	public void Giving_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Giving;

		// Assert
		Assert.Equal("giving", value);
	}

	[Fact(DisplayName = "Groups scope has correct value")]
	public void Groups_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Groups;

		// Assert
		Assert.Equal("groups", value);
	}

	[Fact(DisplayName = "People scope has correct value")]
	public void People_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.People;

		// Assert
		Assert.Equal("people", value);
	}

	[Fact(DisplayName = "Publishing scope has correct value")]
	public void Publishing_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Publishing;

		// Assert
		Assert.Equal("publishing", value);
	}

	[Fact(DisplayName = "Registrations scope has correct value")]
	public void Registrations_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Registrations;

		// Assert
		Assert.Equal("registrations", value);
	}

	[Fact(DisplayName = "Resources scope has correct value")]
	public void Resources_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Resources;

		// Assert
		Assert.Equal("resources", value);
	}

	[Fact(DisplayName = "Services scope has correct value")]
	public void Services_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.Services;

		// Assert
		Assert.Equal("services", value);
	}

	[Fact(DisplayName = "OIDC scope has correct value")]
	public void Oidc_HasCorrectValue()
	{
		// Act
		string value = PlanningCenterOAuthScope.OpenId;

		// Assert
		Assert.Equal("openid", value);
	}

	[Fact(DisplayName = "ToString returns correct value")]
	public void ToString_ReturnsCorrectValue()
	{
		// Arrange
		PlanningCenterOAuthScope scope = PlanningCenterOAuthScope.Calendar;

		// Act
		string value = scope.ToString();

		// Assert
		Assert.Equal("calendar", value);
	}

	[Fact(DisplayName = "Implicit string conversion works")]
	public void ImplicitStringConversion_Works()
	{
		// Arrange
		PlanningCenterOAuthScope scope = PlanningCenterOAuthScope.People;

		// Act
		string value = scope;

		// Assert
		Assert.Equal("people", value);
	}

	[Fact(DisplayName = "Implicit scope conversion from string works")]
	public void ImplicitScopeConversion_Works()
	{
		// Arrange
		string scopeString = "custom_scope";

		// Act
		PlanningCenterOAuthScope scope = scopeString;

		// Assert
		Assert.Equal("custom_scope", scope.ToString());
	}

	[Fact(DisplayName = "Scope equality works")]
	public void ScopeEquality_Works()
	{
		// Arrange
		PlanningCenterOAuthScope scope1 = PlanningCenterOAuthScope.Calendar;
		PlanningCenterOAuthScope scope2 = PlanningCenterOAuthScope.Calendar;
		PlanningCenterOAuthScope scope3 = "calendar";

		// Assert
		Assert.Equal(scope1, scope2);
		Assert.Equal(scope1, scope3);
	}

	[Fact(DisplayName = "Scope inequality works")]
	public void ScopeInequality_Works()
	{
		// Arrange
		PlanningCenterOAuthScope scope1 = PlanningCenterOAuthScope.Calendar;
		PlanningCenterOAuthScope scope2 = PlanningCenterOAuthScope.People;

		// Assert
		Assert.NotEqual(scope1, scope2);
	}
}
