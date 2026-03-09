using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

/// <summary>
/// Concrete implementation of PaginatedResourceClient for testing the abstract base class.
/// Exposes protected methods publicly for test access.
/// </summary>
public class TestPaginatedResourceClient : PaginatedResourceClient<TestModel, TestResource, TestResourceCollectionResponse, TestResourceResponse>
{
	public TestPaginatedResourceClient(HttpClient httpClient, Uri uri)
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
	public new Task<TestResourceCollectionResponse> GetAsync(CancellationToken cancellationToken = default)
		=> base.GetAsync(cancellationToken);

	public new Task<TestResourceResponse> PostAsync(TestModel model, CancellationToken cancellationToken = default) => base.PostAsync(model, cancellationToken);

	/// <summary>
	/// Public wrapper for protected AddQueryParameter method.
	/// </summary>
	public TestPaginatedResourceClient AddQueryParameterPublic(string parameter, string value)
	{
		AddQueryParameter(parameter, value);
		return this;
	}

	/// <summary>
	/// Public wrapper for protected ReplaceQueryParameter method.
	/// </summary>
	public TestPaginatedResourceClient ReplaceQueryParameterPublic(string parameter, string value)
	{
		ReplaceQueryParameter(parameter, value);
		return this;
	}
}
