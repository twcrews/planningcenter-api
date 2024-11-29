using Crews.Extensions.Http.Utility;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Tests.Dummies;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.Models.Resources.PlanningCenter;

public class PlanningCenterPaginatedFetchableResourceTests
{
	[Fact]
	public void FilterBy_ReturnsExpectedObject()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.FilterBy(DummyEnum.First, DummyEnum.Second);
		Assert.Equal("http://localhost/?filter=first_value,second_value", subject.Uri.ToString());
	}

	[Fact(DisplayName = "FilterBy returns snake case enum name when JsonApiName attribute is missing")]
	public void FilterBy_ReturnsEnumNameByDefault()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.FilterBy(DummyEnum.ValueWithoutAttribute);
		Assert.Equal("http://localhost/?filter=value_without_attribute", subject.Uri.ToString());
	}

	[Fact]
	public void OrderBy_ReturnsExpectedObject()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.OrderBy(DummyEnum.First);
		Assert.Equal("http://localhost/?order=first_value", subject.Uri.ToString());
	}

	[Fact(DisplayName = "OrderBy returns snake case enum name when JsonApiName attribute is missing")]
	public void OrderBy_ReturnsEnumNameByDefault()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.OrderBy(DummyEnum.ValueWithoutAttribute);
		Assert.Equal("http://localhost/?order=value_without_attribute", subject.Uri.ToString());
	}

	[Fact(DisplayName = "OrderBy correctly adds reverse-order parameter when using descending order")]
	public void OrderBy_CorrectlyAddsDescendingOrderParameter()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.OrderBy(DummyEnum.First, Order.Descending);
		Assert.Equal("http://localhost/?order=-first_value", subject.Uri.ToString());
	}

	[Fact]
	public void Query_ReturnsExpectedObject()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.Query(new(DummyEnum.First, "test1"), new(DummyEnum.Second, "test2"));
		Assert.Equal("http://localhost/?where[first_value]=test1&where[second_value]=test2", subject.Uri.ToString());
	}

	[Fact(DisplayName = "Query returns snake case enum name when JsonApiName attribute is missing")]
	public void Query_ReturnsEnumNameByDefault()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/"), new());
		subject.Query(new KeyValuePair<DummyEnum, string>(DummyEnum.ValueWithoutAttribute, "test3"));
		Assert.Equal("http://localhost/?where[value_without_attribute]=test3", subject.Uri.ToString());
	}

	[Fact(DisplayName = "WithID successfully returns the singleton type for the resource")]
	public void WithID_GetsSingleton()
	{
		DummyPaginatedFetchableResource subject = new(new("http://localhost/resources/"), new());
		DummySingletonFetchableResource singleton = subject.WithID("1");
		Assert.Equal("http://localhost/resources/1", singleton.Uri.ToString());
	}

	[Theory(DisplayName = "GetAllAsync returns correct resource collection")]
	[InlineData(Serialized.DummyRootCollectionObject, 8)]
	[InlineData(Serialized.DummyRootCollectionNoMetaObject, 0)]
	public async Task GetAllAsync_GetsResourceCollection(string responseJson, int totalCount)
	{
		MockHttpMessageHandler handler = new();
		HttpClient client = new(handler);

		handler.When("http://localhost/resources").Respond("application/json", responseJson);

		DummyPaginatedFetchableResource subject = new(new("http://localhost/resources"), client);
		PaginatedResourceCollection<DummyResource> result = await subject.GetAllAsync();
		Assert.Equal(totalCount, result.TotalCount);
		Assert.Equal(28, result.Resources.First().Age);
	}

	[Fact(DisplayName = "GetAllAsync with custom count adds correct parameters to request")]
	public async Task GetAllAsync_RequestsCorrectCount()
	{
		MockHttpMessageHandler handler = new();
		HttpClient client = new(handler);
		handler.When("http://localhost/resources").Respond("application/json", Serialized.DummyRootCollectionObject);

		DummyPaginatedFetchableResource subject = new(new("http://localhost/resources"), client);
		await subject.GetAllAsync(5);

		QueryStringBuilder builder = new(subject.Uri.Query);
		Assert.Equal("5", builder.Parameters.Single(p => p.Key == "per_page").Values.Single());
	}

	[Fact(DisplayName = "GetAllAsync with custom count and offset adds correct parameters to request")]
	public async Task GetAllAsync_RequestsCorrectCountAndOffset()
	{
		MockHttpMessageHandler handler = new();
		HttpClient client = new(handler);
		handler.When("http://localhost/resources").Respond("application/json", Serialized.DummyRootCollectionObject);

		DummyPaginatedFetchableResource subject = new(new("http://localhost/resources"), client);
		await subject.GetAllAsync(5, 3);

		QueryStringBuilder builder = new(subject.Uri.Query);
		Assert.Equal("5", builder.Parameters.Single(p => p.Key == "per_page").Values.Single());
		Assert.Equal("3", builder.Parameters.Single(p => p.Key == "offset").Values.Single());
	}
}
