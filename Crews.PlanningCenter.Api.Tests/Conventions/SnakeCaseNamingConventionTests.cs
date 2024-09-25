using Crews.PlanningCenter.Api.Conventions;
using JsonApiFramework.Conventions;

namespace Crews.PlanningCenter.Api.Tests.Conventions;

public class SnakeCaseNamingConventionTests
{
	[Theory(DisplayName = "Convention correctly applies to formattable string")]
	[InlineData("TotalCount")]
	[InlineData("totalCount")]
	[InlineData("total-count")]
	[InlineData("Total count")]
	public void ConventionAppliesCorrectly(string testString)
	{
		string expected = "total_count";
		INamingConvention convention = new SnakeCaseNamingConvention();

		Assert.Equal(expected, convention.Apply(testString));
	}
}
