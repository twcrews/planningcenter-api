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

[Theory(DisplayName = "QueryStringBuilder constructor correctly sets parameters")]
[InlineData("?a=1&b=2")]
[InlineData("?a=1,2&b=3,4")]
public void QueryStringBuilder_SetsParameters(string queryString)
{
	QueryString input = new(queryString);
	string[] parameterStrings = queryString.Trim('?').Split('&');

	IEnumerable<QueryString.Parameter> parameters = parameterStrings
		.Select(parameterString => 
		{
			string[] parameterStringParts = parameterString.Split('=');
			return new QueryString.Parameter
			{
				Key = parameterStringParts[0],
				Values = [.. parameterStringParts[1].Split(',')]
			};
		});

	QueryStringBuilder builder = new(input);

	Assert.Equivalent(parameters, builder.Parameters);
	}
}