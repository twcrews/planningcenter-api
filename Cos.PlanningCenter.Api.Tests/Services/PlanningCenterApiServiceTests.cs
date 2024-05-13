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

	private readonly Uri _collectionUri;
	private readonly Uri _singletonUri;
	private readonly Uri _errorUri;
	private readonly Uri _emptyErrorUri;

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
		_handler.When("http://localhost/error").Respond(
			HttpStatusCode.Forbidden, "application/json", Serialized.DummyErrorObject);
		_handler.When("http://localhost/emptyError").Respond(HttpStatusCode.Forbidden);

		_collectionUri = new("collection", UriKind.Relative);
		_singletonUri = new("singleton", UriKind.Relative);
		_errorUri = new("error", UriKind.Relative);
		_emptyErrorUri = new("emptyError", UriKind.Relative);
	}

	[Fact]
	public async Task GetAllAsync_GetsExpectedObject()
	{
		PlanningCenterRootCollectionObject<DummyData> expected = Dummy.RootCollectionObject;
		PlanningCenterRootCollectionObject<DummyData> actual = await _subject.GetAllAsync<DummyData>(_collectionUri);

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async Task GetAsync_GetsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expected = Dummy.RootSingleObject;
		PlanningCenterRootSingletonObject<DummyData> actual = await _subject.GetAsync<DummyData>(_singletonUri);

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async Task PostAllAsync_SendsExpectedObject()
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
			
		await _subject.PostAllAsync(_collectionUri, Dummy.Dummies);

		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async Task PostAllAsync_GetsExpectedObject()
	{
		PlanningCenterRootCollectionObject<DummyData> expected = Dummy.RootCollectionObject;
		PlanningCenterRootCollectionObject<DummyData> actual = await _subject.PostAllAsync<DummyData>(_collectionUri, [ 
		new() 
		{ 
			IntAttribute = 0,
			StringAttribute = "",
			BoolAttribute = true
		}]);

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async Task PostAsync_SendsExpectedObject()
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
			
		await _subject.PostAsync(_singletonUri, Dummy.Dummies.First());

		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async Task PostAsync_GetsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expected = Dummy.RootSingleObject;
		PlanningCenterRootSingletonObject<DummyData> actual = await _subject.PostAsync<DummyData>(_singletonUri, new() 
		{ 
			IntAttribute = 0,
			StringAttribute = "",
			BoolAttribute = true
		});

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async Task PatchAsync_SendsExpectedObject()
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
			
		await _subject.PatchAsync(_singletonUri, Dummy.Dummies.First());

		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async Task PatchAsync_GetsExpectedObject()
	{
		PlanningCenterRootSingletonObject<DummyData> expected = Dummy.RootSingleObject;
		PlanningCenterRootSingletonObject<DummyData> actual = await _subject.PatchAsync<DummyData>(_singletonUri, new() 
		{ 
			IntAttribute = 0,
			StringAttribute = "",
			BoolAttribute = true
		});

		Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
	}

	[Fact]
	public async Task DeleteAsync_UsesExpectedMethod()
	{
		_handler.Expect(HttpMethod.Delete, "http://localhost/singleton")
			.Respond(HttpStatusCode.NoContent);

		await _subject.DeleteAsync(_singletonUri);
		_handler.VerifyNoOutstandingExpectation();
	}

	[Fact]
	public async Task ErrorResponseThrowsCorrectException()
	{
		try
		{
			await _subject.DeleteAsync(_errorUri);
			Assert.Fail("No exception was thrown, but one was expected.");
		}
		catch (HttpRequestException exception)
		{
			Assert.Contains("403", exception.Message);
			Assert.Contains("Forbidden", exception.Message);
			Assert.Contains("You do not have access to this resource", exception.Message);
			Assert.Contains("sample_error_code", exception.Message);
			Assert.Contains("This is a sample description.", exception.Message);
		}
		catch
		{
			throw;
		}
	}

	[Fact]
	public async Task EmptyErrorResponseThrowsCorrectException()
	{
		try
		{
			await _subject.DeleteAsync(_emptyErrorUri);
			Assert.Fail("No exception was thrown, but one was expected.");
		}
		catch (HttpRequestException exception)
		{
			Assert.Equal("The HTTP request failed (403 Forbidden).", exception.Message);
		}
		catch
		{
			throw;
		}
	}
}
