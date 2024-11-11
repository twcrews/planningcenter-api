using System.Net;
using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Tests.Dummies;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;
using JsonApiFramework;
using JsonApiFramework.JsonApi;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.Models.Resources.PlanningCenter;

public class PlanningCenterFetchableResourceTests
{
	private readonly MockHttpMessageHandler _handler;
	private readonly HttpClient _client;
	private readonly DummyFetchableResource _subject;

	public PlanningCenterFetchableResourceTests()
	{
		_handler = new();
		_client = new(_handler);
		_subject = new(new("http://localhost/"), _client);

		_handler.When("http://localhost/")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyRootCollectionObject);
		_handler.When("http://localhost/error")
			.Respond(HttpStatusCode.Forbidden, "application/json", Serialized.DummyErrorObject);
		_handler.When("http://localhost/errorCollection")
			.Respond(HttpStatusCode.InternalServerError, "application/json", Serialized.DummyErrorCollectionObject);
		_handler.When("http://localhost/invalid")
			.Respond(HttpStatusCode.OK, "application/json", Serialized.DummyInvalidObject);
	}

	[Fact]
	public void AppendCustomParameters_ReturnsExpectedObject()
	{
		_subject.AppendCustomParameters([
			new()
			{
					Key = "a",
					Values = ["b", "c"]
			}
		]);
		Assert.Equal("http://localhost/?a=b,c", _subject.Uri.ToString());
	}

	[Fact]
	public void ClearAllParameters_RemovesQueryString()
	{
		_subject.ClearAllParameters();
		Assert.Equal("http://localhost/", _subject.Uri.ToString());
	}

	[Fact]
	public void Include_ReturnsExpectedObject()
	{
		_subject.Include(DummyEnum.First, DummyEnum.Second);
		Assert.Equal("http://localhost/?include=first_value,second_value", _subject.Uri.ToString());
	}

	[Fact(DisplayName = "Include returns snake case enum name when JsonApiName attribute is missing")]
	public void Include_ReturnsEnumNameByDefault()
	{
		_subject.Include(DummyEnum.ValueWithoutAttribute);
		Assert.Equal("http://localhost/?include=value_without_attribute", _subject.Uri.ToString());
	}

	[Fact(DisplayName = "AddParameters throws exception when attempting to add duplicate")]
	public void AddParameters_ThrowsOnDuplicate()
	{
		_subject.AddParameters("key", "value1", "value2");
		Assert.Throws<ArgumentException>(() => _subject.AddParameters("key", "value3"));
	}

	[Fact(DisplayName = "FetchDocumentAsync returns Document object")]
	public async Task FetchDocumentAsync_ReturnsExpectedObject()
	{
		Document? document = await _subject.FetchDocumentAsync(new() { RequestUri = new("http://localhost/") });
		if (document == null) Assert.Fail("Document object was null.");

		using DummyContext context = new(document);

		Meta documentMeta = context.GetDocumentMeta();
		PlanningCenterMetadata metaData = documentMeta.GetData<PlanningCenterMetadata>();
		
		Links documentLinks = context.GetDocumentLinks();
		Link selfLink = documentLinks.Single(l => l.Key == "self").Value;
		IEnumerable<DummyResource> collection = context.GetResourceCollection<DummyResource>();
		DummyResource firstResource = collection.Single(r => r.ID == "123abc");

		Assert.Equal(DocumentType.ResourceCollectionDocument, context.GetDocumentType());
		Assert.Equal("555", metaData.Parent?.Id);
		Assert.Equal("https://www.test.com", selfLink.HRef);
		Assert.Equal(28, firstResource.Age);
		Assert.Equal("Tommy", firstResource.Name);
	}

	[Fact(DisplayName = "FetchDocumentAsync throws correct exception when document contains single error")]
	public Task FetchDocumentAsync_SingleErrorThrowsHttpRequestException()
		=> Assert.ThrowsAsync<HttpRequestException>(() => _subject.FetchDocumentAsync(new() 
		{ 
			RequestUri = new("http://localhost/error") 
		}));

	[Fact(DisplayName = "FetchDocumentAsync throws correct exception when document contains multiple errors")]
	public Task FetchDocumentAsync_MultipleErrorsThrowAggregateException()
		=> Assert.ThrowsAsync<AggregateException>(() => _subject.FetchDocumentAsync(new() 
		{ 
			RequestUri = new("http://localhost/errorCollection") 
		}));

	[Fact(DisplayName = "FetchDocumentAsync throws correct exception when document is not in a valid format")]
	public Task FetchDocumentAsync_InvalidDocumentThrowsFormatException()
		=> Assert.ThrowsAsync<FormatException>(() => _subject.FetchDocumentAsync(new()
		{
			RequestUri = new("http://localhost/invalid")
		}));

	[Fact(DisplayName = "GetNamedAssociated returns new resource instance with correct path appended to URI")]
	public void GetNamedAssociated_ReturnsResourceWithCorrectUri()
	{
		Uri expectedUri = _subject.Uri.SafelyAppendPath(DummySingletonFetchableResource.ApiName);
		DummySingletonFetchableResource associated = _subject.Dummy;

		Assert.Equal(expectedUri, associated.Uri);
	}

	[Fact(DisplayName = "GetAssociated returns new resource instance with custom path appended to URI")]
	public void GetAssociated_ReturnsResourceWithCustomUri()
	{
		_subject.DummiesName = "dummies";
		Uri expectedUri = _subject.Uri.SafelyAppendPath(_subject.DummiesName);
		DummyPaginatedFetchableResource associated = _subject.Dummies;

		Assert.Equal(expectedUri, associated.Uri);
	}
}
