using System.Net;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Tests.Dummies;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PaginatedResourceClientTests
{
	[Fact]
	public void PerPage_SetsPerPageParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.PerPage(25);

		// Assert
		Assert.Contains("per_page=25", client.Uri.ToString());
	}

	[Fact]
	public void PerPage_UpdatesUri()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test?per_page=10");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.PerPage(50);

		// Assert
		Assert.Contains("per_page=50", client.Uri.ToString());
		Assert.DoesNotContain("per_page=10", client.Uri.ToString());
	}

	[Fact]
	public void PerPage_ReturnsClientInstance_ForChaining()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.PerPage(25);

		// Assert
		Assert.Same(client, result);
	}

	[Fact]
	public void PerPage_WithZero_SetsZero()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.PerPage(0);

		// Assert
		Assert.Contains("per_page=0", client.Uri.ToString());
	}

	[Fact]
	public void Offset_SetsOffsetParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Offset(100);

		// Assert
		Assert.Contains("offset=100", client.Uri.ToString());
	}

	[Fact]
	public void Offset_UpdatesUri()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test?offset=50");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Offset(200);

		// Assert
		Assert.Contains("offset=200", client.Uri.ToString());
		Assert.DoesNotContain("offset=50", client.Uri.ToString());
	}

	[Fact]
	public void Offset_ReturnsClientInstance_ForChaining()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.Offset(100);

		// Assert
		Assert.Same(client, result);
	}

	[Fact]
	public void Offset_WithZero_SetsZero()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Offset(0);

		// Assert
		Assert.Contains("offset=0", client.Uri.ToString());
	}

	[Fact]
	public void TakeAndOffset_CanBeChained()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.PerPage(25).Offset(50);

		// Assert
		Assert.Same(client, result);
		var uriString = client.Uri.ToString();
		Assert.Contains("per_page=25", uriString);
		Assert.Contains("offset=50", uriString);
	}

	[Fact]
	public void TakeAndSkipAndCustomParameter_ChainsCorrectly()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.PerPage(10).Offset(20).AddCustomParameter("filter", "active");

		// Assert
		var uriString = client.Uri.ToString();
		Assert.Contains("per_page=10", uriString);
		Assert.Contains("offset=20", uriString);
		Assert.Contains("filter=active", uriString);
	}

	[Fact]
	public void Filter_SetsFilterParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Filter("active");

		// Assert
		Assert.Contains("filter=active", client.Uri.ToString());
	}

	[Fact]
	public void Filter_ReturnsClientInstance_ForChaining()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.Filter("active");

		// Assert
		Assert.Same(client, result);
	}

	[Fact]
	public void Filter_CanBeChainedWithOtherMethods()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Filter("active").PerPage(10).Offset(20);

		// Assert
		var uriString = client.Uri.ToString();
		Assert.Contains("filter=active", uriString);
		Assert.Contains("per_page=10", uriString);
		Assert.Contains("offset=20", uriString);
	}

	[Fact]
	public async Task GetAsync_SendsGetRequest_ToCorrectUri()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, "https://example.com/test")
			.Respond("application/json", Serialized.DummyRootCollectionObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var response = await client.GetAsync();

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public async Task GetAsync_WithSuccessResponse_ReturnsDeserializedData()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, "https://example.com/test")
			.Respond("application/json", Serialized.DummyRootCollectionObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var response = await client.GetAsync();

		// Assert
		Assert.NotNull(response.Data);
	}

	[Fact]
	public async Task GetAsync_WithErrorResponse_ThrowsJsonApiException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, "https://example.com/test")
			.Respond(HttpStatusCode.BadRequest, "application/json", Serialized.DummyErrorObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act & Assert
		await Assert.ThrowsAsync<JsonApiException>(() => client.GetAsync());
	}

	[Fact]
	public async Task GetAsync_WithCancellationToken_PassesToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, "https://example.com/test")
			.Respond(async () =>
			{
				await Task.Delay(1000);
				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(Serialized.DummyRootCollectionObject)
				};
			});

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);
		var cts = new CancellationTokenSource();
		cts.Cancel();

		// Act & Assert
		await Assert.ThrowsAnyAsync<OperationCanceledException>(() => client.GetAsync(cts.Token));
	}

	[Fact]
	public async Task PostAsync_SendsPostRequest_ToCorrectUri()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, "https://example.com/test")
			.Respond("application/json", Serialized.DummyRootSingleObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var response = await client.PostAsync(new() { Name = "John", Age = 30 });

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public async Task PostAsync_WithSuccessResponse_ReturnsDeserializedData()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, "https://example.com/test")
			.Respond("application/json", Serialized.DummyRootSingleObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var response = await client.PostAsync(new() { Name = "John", Age = 30 });

		// Assert
		Assert.NotNull(response.Data);
	}

	[Fact]
	public async Task PostAsync_WithErrorResponse_ThrowsJsonApiException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, "https://example.com/test")
			.Respond(HttpStatusCode.BadRequest, "application/json", Serialized.DummyErrorObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act & Assert
		await Assert.ThrowsAsync<JsonApiException>(() => client.PostAsync(new() { Name = "John", Age = 30 }));
	}

	[Fact]
	public async Task PostAsync_WithCancellationToken_PassesToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Post, "https://example.com/test")
			.Respond(async () =>
			{
				await Task.Delay(1000);
				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(Serialized.DummyRootSingleObject)
				};
			});

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);
		var cts = new CancellationTokenSource();
		cts.Cancel();

		// Act & Assert
		await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
			client.PostAsync(new() { Name = "John", Age = 30 }, cts.Token));
	}
}
