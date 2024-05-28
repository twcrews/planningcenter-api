namespace Crews.PlanningCenter.Api.Tests;

public class PlanningCenterPaginatedFetchableResourceTests
{
	[Fact]
	public void FilterBy_ReturnsExpectedObject()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"));
		subject.FilterBy(DummyEnum.First, DummyEnum.Second);
		Assert.Equal("http://localhost/?filter=first_value,second_value", subject.Uri.ToString());
	}

	[Fact]
	public void OrderBy_ReturnsExpectedObject()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"));
		subject.OrderBy(DummyEnum.First);
		Assert.Equal("http://localhost/?order=first_value", subject.Uri.ToString());
	}
	
	[Fact]
	public void Query_ReturnsExpectedObject()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"));
		subject.Query(new(DummyEnum.First, "test1"), new(DummyEnum.Second, "test2"));
		Assert.Equal("http://localhost/?where[first_value]=test1&where[second_value]=test2", subject.Uri.ToString());
	}
}
