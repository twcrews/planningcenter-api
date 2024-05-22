using Crews.PlanningCenter.Api.Utility;

namespace Crews.PlanningCenter.Api.Tests.Utility;

public class QueryStringBuilderTests
{
	[Fact]
	public void QueryStringBuilder_GeneratesCorrectQueryString()
	{
		QueryStringBuilder subject = new()
		{
			BeginningDelimiter = "!@",
			ParameterDelimiter = "#$%",
			ParameterAssignmentDelimiter = "^&",
			ParameterValuesDelimiter = "*(",
			EndingDelimiter = ")_",
			Parameters = [
				new()
				{
					Key = "a",
					Values = ["b"]
				},
				new()
				{
					Key = "c",
					Values = ["d", "e"]
				}
			]
		};

		Assert.Equal("!@a^&b#$%c^&d*(e)_", subject.QueryString.ToString());
	}
}
