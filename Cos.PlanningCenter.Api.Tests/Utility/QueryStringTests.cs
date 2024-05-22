using Cos.PlanningCenter.Api.Utility;

namespace Cos.PlanningCenter.Api.Tests.Utility;

public class QueryStringTests
{
	[Fact]
	public void Constructor_ThrowsExceptionWithEmptyString()
	  => Assert.Throws<FormatException>(() => new QueryString(string.Empty));

    [Fact]
	public void Parameters_MalformedParameterAssignmentThrowsException()
	{
		QueryString subject = new("?key=key=value&a=b");
		Assert.Throws<FormatException>(() => subject.Parameters.ToList());
	}

	[Fact]
	public void Parameters_EmptyParameterKeyThrowsException()
	{
		QueryString subject = new("?=value&a=b");
		Assert.Throws<FormatException>(() => subject.Parameters.ToList());
	}

	[Fact]
	public void Constructor_ParsesStringCorrectly()
	{
		QueryString subject = new("?a=b&c=d,e");

		Assert.True(subject.Parameters.Count() == 2);
		Assert.True(subject.Parameters
			.Where(p => p.Key == "a" && p.Values.Count == 1 && p.Values.Contains("b"))
			.Count() == 1);
		Assert.True(subject.Parameters
			.Where(p => p.Key == "c" 
				&& p.Values.Count == 2 
				&& p.Values.Contains("d")
				&& p.Values.Contains("e"))
			.Count() == 1);
	}

	[Fact]
	public void Constructor_ParsesCustomDelimiters()
	{
		QueryString subject = new("+*&a$#b@!c$#d~=e/-")
		{
			BeginningDelimiter = "+*&",
			ParameterDelimiter = "@!",
			ParameterAssignmentDelimiter = "$#",
			ParameterValuesDelimiter = "~=",
			EndingDelimiter = "/-"
		};

		Assert.True(subject.Parameters.Count() == 2);
		Assert.True(subject.Parameters
			.Where(p => p.Key == "a" && p.Values.Count == 1 && p.Values.Contains("b"))
			.Count() == 1);
		Assert.True(subject.Parameters
			.Where(p => p.Key == "c" 
				&& p.Values.Count == 2 
				&& p.Values.Contains("d")
				&& p.Values.Contains("e"))
			.Count() == 1);
	}
}
