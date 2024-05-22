using Cos.PlanningCenter.Api.Models.Resources.PlanningCenter;

namespace Cos.PlanningCenter.Api.Tests;

public class PlanningCenterFetchableResourceTests
{
	class TestFetchableResource(Uri? uri) : PlanningCenterFetchableResource(uri!) { }

	[Fact]
	public void GuardUri_ThrowsExceptionOnNullUri()
	{
		TestFetchableResource subject = new(null);
		Assert.Throws<NullReferenceException>(() => subject.AppendCustomParameters([]));
	}

	[Fact]
	public void AppendCustomParameters_ReturnsExpectedObject()
	{
		TestFetchableResource subject = new(new("http://localhost/"));
		subject.AppendCustomParameters([
			new()
			{
				Key = "a",
				Values = ["b", "c"]
			}
		]);
		Assert.Equal("http://localhost/?a=b,c", subject.Uri.ToString());
	}

	[Fact]
	public void ClearAllParameters_RemovesQueryString()
	{
		TestFetchableResource subject = new(new("http://localhost/?a=b&c=d,e"));
		subject.ClearAllParameters();
		Assert.Equal("http://localhost/", subject.Uri.ToString());
	}
}
