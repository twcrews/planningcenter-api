using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

/// <summary>
/// Concrete implementation of PaginatedResourceClient for testing the abstract base class.
/// Exposes protected methods publicly for test access.
/// </summary>
public class TestPaginatedResourceClient : PaginatedResourceClient<TestResource, TestResource, TestResourceResponse>
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
	public new Task<TestResourceResponse> GetAsync(CancellationToken cancellationToken = default)
		=> base.GetAsync(cancellationToken);

	/// <summary>
	/// Public wrapper for protected SetQueryParameter method.
	/// </summary>
	public TestPaginatedResourceClient SetQueryParameterPublic(string parameter, string value)
	{
		SetQueryParameter(parameter, value);
		return this;
	}

	protected override Task<TestResourceResponse> DeserializeResponseAsync(
		HttpResponseMessage response,
		CancellationToken cancellationToken)
	{
		// Reuse the same deserialization logic
		var baseClient = new TestResourceClient(HttpClient, Uri);
		return baseClient.GetType()
			.GetMethod("DeserializeResponseAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
			.Invoke(baseClient, [response, cancellationToken]) as Task<TestResourceResponse>
			?? Task.FromResult(new TestResourceResponse { ResponseMessage = response });
	}
}
