using System.Net;
using System.Text.Json;
using Cos.PlanningCenter.Api.Models;
using Cos.PlanningCenter.Api.Tests.Dummies;
using Cos.PlanningCenter.Api.Tests.Dummies.Serialized;
using RichardSzalay.MockHttp;

namespace Cos.PlanningCenter.Api.Tests;

public class PlanningCenterApiServiceTests
{
	private readonly MockHttpMessageHandler _handler;
	private readonly HttpClient _client;
	private readonly PlanningCenterApiService _subject;

	public PlanningCenterApiServiceTests()
	{
		_handler = new();
		_client = new(_handler)
		{
			BaseAddress = new("http://localhost/")
		};
		_subject = new(_client);

		_handler.When("http://localhost/collection").Respond("application/json", Serialized.DummyRootCollectionObject);
		_handler.When("http://localhost/singleton").Respond("application/json", Serialized.DummyRootSingleObject);
	}

	[Fact]
	public async void GetAllAsync_GetsExpectedObject()
	{
		PlanningCenterRootCollectionObject<DummyData> expected = Dummy.RootCollectionObject;
		PlanningCenterRootCollectionObject<DummyData> actual = await _subject.GetAllAsync<DummyData>("collection");

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async void GetAsync_GetsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expected = Dummy.RootSingleObject;
		PlanningCenterRootSingletonObject<DummyData> actual = await _subject.GetAsync<DummyData>("singleton");

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async void PostAllAsync_SendsExpectedObject()
	{
		PlanningCenterRootCollectionObject<DummyData> expectedPayload = new()
		{
			Data = Dummy.Dummies.Select(d => new PlanningCenterDataObject<DummyData>
			{
				Attributes = d
			})
		};
		_handler.Expect("http://localhost/collection")
			.WithJsonContent(expectedPayload)
			.Respond("application/json", Serialized.DummyRootCollectionObject);
			
		await _subject.PostAllAsync("collection", Dummy.Dummies);

		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async void PostAllAsync_GetsExpectedObject()
	{
		PlanningCenterRootCollectionObject<DummyData> expected = Dummy.RootCollectionObject;
		PlanningCenterRootCollectionObject<DummyData> actual = await _subject.PostAllAsync<DummyData>("collection", [ new() 
		{ 
			IntAttribute = 0,
			StringAttribute = "",
			BoolAttribute = true
		}]);

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async void PostAsync_SendsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expectedPayload = new()
		{
			Data = new PlanningCenterDataObject<DummyData>
			{
				Attributes = Dummy.Dummies.First()
			}
		};
		_handler.Expect("http://localhost/singleton")
			.WithJsonContent(expectedPayload)
			.Respond("application/json", Serialized.DummyRootSingleObject);
			
		await _subject.PostAsync("singleton", Dummy.Dummies.First());

		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async void PostAsync_GetsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expected = Dummy.RootSingleObject;
		PlanningCenterRootSingletonObject<DummyData> actual = await _subject.PostAsync<DummyData>("singleton", new() 
		{ 
			IntAttribute = 0,
			StringAttribute = "",
			BoolAttribute = true
		});

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async void PatchAsync_SendsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expectedPayload = new()
		{
			Data = new PlanningCenterDataObject<DummyData>
			{
				Attributes = Dummy.Dummies.First()
			}
		};
		_handler.Expect("http://localhost/singleton")
			.WithJsonContent(expectedPayload)
			.Respond("application/json", Serialized.DummyRootSingleObject);
			
		await _subject.PatchAsync("singleton", Dummy.Dummies.First());

		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async void PatchAsync_GetsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expected = Dummy.RootSingleObject;
		PlanningCenterRootSingletonObject<DummyData> actual = await _subject.PatchAsync<DummyData>("singleton", new() 
		{ 
			IntAttribute = 0,
			StringAttribute = "",
			BoolAttribute = true
		});

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async void DeleteAsync_UsesExpectedMethod()
	{
		_handler.Expect(HttpMethod.Delete, "http://localhost/singleton")
			.Respond(HttpStatusCode.NoContent);

		await _subject.DeleteAsync("singleton");
		_handler.VerifyNoOutstandingExpectation();
	}
}
