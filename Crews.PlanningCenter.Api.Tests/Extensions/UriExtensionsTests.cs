using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.Utility;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class UriExtensionsTests
{
	[Theory(DisplayName = "SetQueryString correctly appends or replaces query string")]
	[InlineData("http://a.bc", "?d=e", "http://a.bc?d=e")]
	[InlineData("http://a.bc/", "?d=e", "http://a.bc/?d=e")]
	[InlineData("http://a.bc", "?d=e&f=g", "http://a.bc?d=e&f=g")]
	[InlineData("http://a.bc/", "?d=e&f=g", "http://a.bc/?d=e&f=g")]
	[InlineData("http://a.bc/d", "?e=f", "http://a.bc/d?e=f")]
	[InlineData("http://a.bc/d/", "?e=f", "http://a.bc/d/?e=f")]
	[InlineData("http://a.bc/d", "?e=f&g=h", "http://a.bc/d?e=f&g=h")]
	[InlineData("http://a.bc/d/", "?e=f&g=h", "http://a.bc/d/?e=f&g=h")]
	[InlineData("http://a.bc?d=e", "?f=g", "http://a.bc?f=g")]
	[InlineData("http://a.bc/?d=e", "?f=g", "http://a.bc/?f=g")]
	[InlineData("http://a.bc?d=e&f=g", "?h=i&j=k", "http://a.bc?h=i&j=k")]
	[InlineData("http://a.bc/?d=e&f=g", "?h=i&j=k", "http://a.bc/?h=i&j=k")]
	[InlineData("http://a.bc/d?e=f", "?g=h", "http://a.bc/d?g=h")]
	[InlineData("http://a.bc/d/?e=f", "?g=h", "http://a.bc/d/?g=h")]
	[InlineData("http://a.bc/d?e=f&g=h", "?i=j&k=l", "http://a.bc/d?i=j&k=l")]
	[InlineData("http://a.bc/d/?e=f&g=h", "?i=j&k=l", "http://a.bc/d/?i=j&k=l")]
	public void SetQueryString_SetsQueryString(string root, string queryString, string expected)
	{
		Uri subject = new(root);
		QueryString query = new(queryString);
		subject = subject.SetQueryString(query);

		Assert.Equal(new Uri(expected), subject);
	}

	[Theory(DisplayName = "ClearQueryString correctly removes query string")]
	[InlineData("http://a.bc", "?d=e")]
	[InlineData("http://a.bc/", "?d=e")]
	[InlineData("http://a.bc", "?d=e&f=g")]
	[InlineData("http://a.bc/", "?d=e&f=g")]
	[InlineData("http://a.bc/d", "?e=f")]
	[InlineData("http://a.bc/d/", "?e=f")]
	[InlineData("http://a.bc/d", "?e=f&g=h")]
	[InlineData("http://a.bc/d/", "?e=f&g=h")]
	[InlineData("http://a.bc", "")]
	[InlineData("http://a.bc/", "")]
	[InlineData("http://a.bc/d", "")]
	[InlineData("http://a.bc/d/", "")]
	public void ClearQueryString_ClearsQueryString(string expected, string queryString)
	{
		Uri subject = new(expected);

		if (!string.IsNullOrWhiteSpace(queryString))
		{
			QueryString query = new(queryString);
			subject = subject.SetQueryString(query);
			Assert.NotEqual(new Uri(expected), subject);
		}

		subject = subject.ClearQueryString();
		Assert.Equal(new Uri(expected), subject);
	}

	[Theory(DisplayName = "SafelyAppendPath correctly combines URI with subpath")]
	[InlineData("http://a.bc", "d/e/f")]
	[InlineData("http://a.bc", "/d/e/f")]
	[InlineData("http://a.bc/", "d/e/f")]
	[InlineData("http://a.bc/", "/d/e/f")]
	[InlineData("http://a.bc/d", "e/f")]
	[InlineData("http://a.bc/d", "/e/f")]
	[InlineData("http://a.bc/d/", "e/f")]
	[InlineData("http://a.bc/d/", "/e/f")]
	public void SafelyAppendPath_CorrectlyAppendsPath(string root, string path)
	{
		Uri expected = new("http://a.bc/d/e/f");

		Uri subject = new(root);
		subject = subject.SafelyAppendPath(path);

		Assert.Equal(expected, subject);
	}
}
