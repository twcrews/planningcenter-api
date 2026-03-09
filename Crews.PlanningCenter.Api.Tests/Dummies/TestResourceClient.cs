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
public record TestResource : JsonApiResource<TestModel> { }

/// <summary>
/// Test response wrapper for resource client testing.
/// </summary>
public class TestResourceResponse : ResourceResponse<TestResource> { }

public class TestResourceCollectionResponse : ResourceResponse<IEnumerable<TestResource>> { }

/// <summary>
/// Concrete implementation of ResourceClient for testing the abstract base class.
/// Exposes protected methods publicly for test access.
/// </summary>
public class TestResourceClient : SingletonResourceClient<TestModel, TestResource, TestResourceResponse>
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
	/// Public wrapper for protected AddQueryParameter method.
	/// </summary>
	public TestResourceClient AddQueryParameterPublic(string parameter, string value)
	{
		AddQueryParameter(parameter, value);
		return this;
	}

	/// <summary>
	/// Public wrapper for protected ReplaceQueryParameter method.
	/// </summary>
	public TestResourceClient ReplaceQueryParameterPublic(string parameter, string value)
	{
		ReplaceQueryParameter(parameter, value);
		return this;
	}
}
