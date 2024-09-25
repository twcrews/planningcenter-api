using System.Net;
using Crews.PlanningCenter.Api.Tests.Dummies;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;
using JsonApiFramework.JsonApi;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.Models.Resources.PlanningCenter;

public class PlanningCenterSingletonFetchableResourceTests
{
	private readonly MockHttpMessageHandler _handler;
	private readonly HttpClient _client;

	public PlanningCenterSingletonFetchableResourceTests()
	{
		_handler = new();
		_client = new(_handler);
		_handler.When("http://localhost/")
			.Respond("application/json", Serialized.DummyRootSingleObject);
	}

	[Fact(DisplayName = "GetAsync fetches correct object")]
	public async Task GetAsync_GetsExpectedObject()
	{
		DummySingletonFetchableResource subject = new(new("http://localhost/"), _client);
		DummyResource? resource = await subject.GetAsync();
		Assert.Equal("123abc", resource?.ID);
		Assert.Equal("Tommy", resource?.Name);
		Assert.Equal(28, resource?.Age);
	}

	[Fact(DisplayName = "PostAsync sends correct request content")]
	public Task PostAsync_PostsCorrectContent()
	{
		DummySingletonFetchableResource subject = new(new("http://localhost/post"), _client);
		DummyResource resource = new()
		{
			ID = "123abc",
			Name = "Tommy",
			Age = 28
		};

		using (DummyContext context = new())
		{
			Document document = context.NewDocument()
				.Resource(resource)
				.ResourceEnd()
				.WriteDocument();

			string json = document.ToJson();

			_handler.Expect(HttpMethod.Post, "http://localhost/post")
				.WithContent(json)
				.Respond(HttpStatusCode.OK);
		}

		return subject.PostAsync(resource);
	}

	[Fact(DisplayName = "PatchAsync sends correct request content")]
	public Task PatchAsync_PatchesCorrectContent()
	{
		DummySingletonFetchableResource subject = new(new("http://localhost/patch"), _client);
		DummyResource resource = new()
		{
			ID = "123abc",
			Name = "Tommy",
			Age = 28
		};

		using (DummyContext context = new())
		{
			Document document = context.NewDocument()
				.Resource(resource)
				.ResourceEnd()
				.WriteDocument();

			string json = document.ToJson();

			_handler.Expect(HttpMethod.Patch, "http://localhost/patch")
				.WithContent(json)
				.Respond(HttpStatusCode.OK);
		}

		return subject.PatchAsync(resource);
	}

	[Fact(DisplayName = "DeleteAsync sends correct HTTP method")]
	public Task DeleteAsync_UsesDeleteMethod()
	{
		DummySingletonFetchableResource subject = new(new("http://localhost/delete"), _client);
		_handler.Expect(HttpMethod.Delete, "http://localhost/delete")
			.Respond(HttpStatusCode.OK);
		return subject.DeleteAsync();
	}
}
