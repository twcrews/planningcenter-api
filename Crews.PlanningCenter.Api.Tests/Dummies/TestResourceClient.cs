using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Models;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

public record TestModel
{	
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	[JsonPropertyName("age")]
	public int Age { get; init; }
}

/// <summary>
/// Test model for resource client testing.
/// </summary>
public record TestResource : JsonApiResource
{
	[JsonPropertyName("attributes")]
	public new TestModel? Attributes { get; init; }
}

/// <summary>
/// Test response wrapper for resource client testing.
/// </summary>
public class TestResourceResponse : ResourceResponse<TestResource>
{
}

/// <summary>
/// Concrete implementation of ResourceClient for testing the abstract base class.
/// Exposes protected methods publicly for test access.
/// </summary>
public class TestResourceClient : ResourceClient<TestModel, TestResource, TestResourceResponse>
{
	public TestResourceClient(HttpClient httpClient, Uri uri)
		: base(httpClient, uri)
	{
	}

	/// <summary>
	/// Public accessor for the protected Uri property.
	/// </summary>
	public new Uri Uri => base.Uri;

	/// <summary>
	/// Public wrapper for protected GetAsync method.
	/// </summary>
	public new Task<TestResourceResponse> GetAsync(CancellationToken cancellationToken = default)
		=> base.GetAsync(cancellationToken);

	/// <summary>
	/// Public wrapper for protected PostAsync method.
	/// </summary>
	public new Task<TestResourceResponse> PostAsync(TestModel model, CancellationToken cancellationToken = default)
		=> base.PostAsync(model, cancellationToken);

	/// <summary>
	/// Public wrapper for protected PatchAsync method.
	/// </summary>
	public new Task<TestResourceResponse> PatchAsync(TestModel model, CancellationToken cancellationToken = default)
		=> base.PatchAsync(model, cancellationToken);

	/// <summary>
	/// Public wrapper for protected DeleteAsync method.
	/// </summary>
	public new Task DeleteAsync(CancellationToken cancellationToken = default)
		=> base.DeleteAsync(cancellationToken);

	/// <summary>
	/// Public wrapper for protected SetQueryParameter method.
	/// </summary>
	public TestResourceClient SetQueryParameterPublic(string parameter, string value)
	{
		SetQueryParameter(parameter, value);
		return this;
	}

	protected override async Task<TestResourceResponse> DeserializeResponseAsync(
		HttpResponseMessage response,
		CancellationToken cancellationToken)
	{
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new JsonApiException(content, e);
        }
        
        JsonApiDocument? document = await response.Content.ReadFromJsonAsync<JsonApiDocument>(cancellationToken);
        if (document is null) return new() { ResponseMessage = response };
        
        TestResourceResponse result = new() { ResponseMessage = response, ResponseBody = document };
        if (document.Data is null) return result;
        
        string? dataString = document!.Data!.ToString();
        if (dataString is null) return result;
        
        TestResource? data = JsonSerializer.Deserialize<TestResource>(dataString);
        if (data is null) return result;
        
        return new TestResourceResponse
        {
            Data = data,
            ResponseBody = document,
            ResponseMessage = response
        };
	}
}
