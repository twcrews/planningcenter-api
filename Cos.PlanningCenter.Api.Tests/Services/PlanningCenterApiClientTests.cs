using System.Net;
using Cos.PlanningCenter.Api.Services;
using Cos.PlanningCenter.Api.Tests.Dummies.Serialized;
using RichardSzalay.MockHttp;

namespace Cos.PlanningCenter.Api.Tests;

public class PlanningCenterApiClientTests
{
	private readonly MockHttpMessageHandler _handler; 
	private readonly HttpClient _client; 
	private readonly PlanningCenterApiClient _subject; 
 
	private readonly Uri _errorUri; 
	private readonly Uri _emptyErrorUri; 
 
	public PlanningCenterApiClientTests() 
	{ 
		_handler = new(); 
		_client = new(_handler) 
		{ 
			BaseAddress = new("http://localhost/") 
		}; 
		_subject = new(_client); 
  
		_handler.When("http://localhost/error").Respond( 
			HttpStatusCode.Forbidden, "application/json", Serialized.DummyErrorObject); 
		_handler.When("http://localhost/emptyError").Respond(HttpStatusCode.Forbidden); 
 
		_errorUri = new("error", UriKind.Relative); 
		_emptyErrorUri = new("emptyError", UriKind.Relative); 
	} 
 
	[Fact] 
	public async Task ErrorResponseThrowsCorrectException() 
	{ 
		try 
		{ 
			await _subject.SendAsync(new() { RequestUri = _errorUri }); 
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
			await _subject.SendAsync(new() { RequestUri = _emptyErrorUri }); 
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
