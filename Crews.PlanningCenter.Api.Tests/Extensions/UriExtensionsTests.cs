using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class UriExtensionsTests
{
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
