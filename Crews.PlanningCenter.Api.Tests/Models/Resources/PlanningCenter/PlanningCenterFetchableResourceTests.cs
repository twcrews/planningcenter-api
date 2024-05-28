namespace Crews.PlanningCenter.Api.Tests;

public class PlanningCenterFetchableResourceTests
{
	[Fact]
	public void GuardUri_ThrowsExceptionOnNullUri()
	{
		DummyFetchableResource subject = new(null);
		Assert.Throws<NullReferenceException>(() => subject.AppendCustomParameters([]));
	}

	[Fact]
	public void AppendCustomParameters_ReturnsExpectedObject()
	{
		DummyFetchableResource subject = new(new("http://localhost/"));
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
		DummyFetchableResource subject = new(new("http://localhost/?a=b&c=d,e"));
		subject.ClearAllParameters();
		Assert.Equal("http://localhost/", subject.Uri.ToString());
	}

	[Fact]
	public void Include_ReturnsExpectedObject()
	{
		DummyFetchableResource subject = new(new("http://localhost/"));
		subject.Include(DummyEnum.First, DummyEnum.Second);
		Assert.Equal("http://localhost/?include=first_value,second_value", subject.Uri.ToString());
	}
}
