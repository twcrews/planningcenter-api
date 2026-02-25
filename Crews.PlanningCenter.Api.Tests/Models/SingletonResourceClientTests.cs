using System.Net;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Tests.Dummies;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;
using RichardSzalay.MockHttp;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class SingletonResourceClientTests
{
	[Fact]
	public void Constructor_WithValidParameters_CreatesClient()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("/test/resource", UriKind.Relative);

		// Act
		var client = new TestResourceClient(httpClient, uri);

		// Assert
		Assert.NotNull(client);
	}

	[Fact]
	public void Constructor_WithNullHttpClient_ThrowsArgumentNullException()
	{
		// Arrange
		var uri = new Uri("/test/resource", UriKind.Relative);

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			new TestResourceClient(null!, uri));
	}

	[Fact]
	public void Constructor_WithNullUri_ThrowsArgumentNullException()
	{
		// Arrange
		var httpClient = new HttpClient();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			new TestResourceClient(httpClient, null!));
	}

	[Fact]
	public void SetQueryParameter_AddsNewParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.SetQueryParameterPublic("filter", "active");

		// Assert
		Assert.Contains("filter=active", client.Uri.ToString());
	}

	[Fact]
	public void SetQueryParameter_UpdatesExistingParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test?filter=inactive");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.SetQueryParameterPublic("filter", "active");

		// Assert
		Assert.Contains("filter=active", client.Uri.ToString());
		Assert.DoesNotContain("inactive", client.Uri.ToString());
	}

	[Fact]
	public void SetQueryParameter_WithMultipleParameters_MaintainsAll()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.SetQueryParameterPublic("filter", "active");
		client.SetQueryParameterPublic("sort", "name");
		client.SetQueryParameterPublic("limit", "10");

		// Assert
		var uriString = client.Uri.ToString();
		Assert.Contains("filter=active", uriString);
		Assert.Contains("sort=name", uriString);
		Assert.Contains("limit=10", uriString);
	}

	[Fact]
	public void SetQueryParameter_ReturnsClientInstance_ForChaining()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		var result = client.SetQueryParameterPublic("filter", "active");

		// Assert
		Assert.Same(client, result);
	}

	[Fact]
	public void AddCustomParameter_AddsParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.AddCustomParameter("custom", "value");

		// Assert
		Assert.Contains("custom=value", client.Uri.ToString());
	}

	[Fact]
	public void ClearParameters_RemovesAllParameters()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test?filter=active&sort=name");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.ClearParameters();

		// Assert
		Assert.DoesNotContain("?", client.Uri.ToString());
		Assert.DoesNotContain("filter", client.Uri.ToString());
		Assert.DoesNotContain("sort", client.Uri.ToString());
	}

	[Fact]
	public void ClearParameters_PreservesBaseUri()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test/path?filter=active");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.ClearParameters();

		// Assert
		Assert.Contains("https://example.com/test/path", client.Uri.ToString());
	}

	[Fact]
	public void ClearParameters_WithNoParameters_DoesNothing()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		client.ClearParameters();

		// Assert
		Assert.Equal("https://example.com/test", client.Uri.ToString());
	}

	[Fact]
	public async Task GetAsync_SendsGetRequest_ToCorrectUri()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, "https://example.com/test/resource")
			.Respond("application/json", Serialized.DummyRootSingleObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test/resource");
		var client = new TestResourceClient(httpClient, uri);

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
			.Respond("application/json", Serialized.DummyRootSingleObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		var response = await client.GetAsync();

		// Assert
		Assert.NotNull(response.Data?.Attributes);
		Assert.Equal("Tommy", response.Data.Attributes.Name);
		Assert.Equal(28, response.Data.Attributes.Age);
	}

	[Fact]
	public async Task GetAsync_WithErrorResponse_ThrowsJsonApiException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Get, "https://example.com/test")
			.Respond(HttpStatusCode.Forbidden, "application/json", Serialized.DummyErrorObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

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
					Content = new StringContent(Serialized.DummyRootSingleObject)
				};
			});

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);
		var cts = new CancellationTokenSource();
		cts.Cancel();

		// Act & Assert
		await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
			client.GetAsync(cts.Token));
	}

	[Fact]
	public async Task PatchAsync_SendsPatchRequest_ToCorrectUri()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(new HttpMethod("PATCH"), "https://example.com/test")
			.Respond("application/json", Serialized.DummyRootSingleObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		var response = await client.PatchAsync(new() { Name = "John", Age = 30 });

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public async Task PatchAsync_WithSuccessResponse_ReturnsDeserializedData()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(new HttpMethod("PATCH"), "https://example.com/test")
			.Respond("application/json", Serialized.DummyRootSingleObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		var response = await client.PatchAsync(new() { Name = "John", Age = 30 });

		// Assert
		Assert.NotNull(response.Data);
	}

	[Fact]
	public async Task PatchAsync_WithErrorResponse_ThrowsJsonApiException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(new HttpMethod("PATCH"), "https://example.com/test")
			.Respond(HttpStatusCode.NotFound, "application/json", Serialized.DummyErrorObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act & Assert
		await Assert.ThrowsAsync<JsonApiException>(() => client.PatchAsync(new() { Name = "John", Age = 30 }));
	}

	[Fact]
	public async Task PatchAsync_WithCancellationToken_PassesToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(new HttpMethod("PATCH"), "https://example.com/test")
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
		var client = new TestResourceClient(httpClient, uri);
		var cts = new CancellationTokenSource();
		cts.Cancel();

		// Act & Assert
		await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
			client.PatchAsync(new() { Name = "John", Age = 30 }, cts.Token));
	}

	[Fact]
	public async Task DeleteAsync_SendsDeleteRequest_ToCorrectUri()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Delete, "https://example.com/test")
			.Respond(HttpStatusCode.NoContent);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act
		await client.DeleteAsync();

		// Assert - should complete without throwing
		Assert.True(true);
	}

	[Fact]
	public async Task DeleteAsync_WithSuccessResponse_Completes()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Delete, "https://example.com/test")
			.Respond(HttpStatusCode.OK);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act & Assert - should complete without exception
		await client.DeleteAsync();
	}

	[Fact]
	public async Task DeleteAsync_WithErrorResponse_ThrowsJsonApiException()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Delete, "https://example.com/test")
			.Respond(HttpStatusCode.Forbidden, "application/json", Serialized.DummyErrorObject);

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);

		// Act & Assert
		await Assert.ThrowsAsync<JsonApiException>(() => client.DeleteAsync());
	}

	[Fact]
	public async Task DeleteAsync_WithCancellationToken_PassesToken()
	{
		// Arrange
		var mockHttp = new MockHttpMessageHandler();
		mockHttp.When(HttpMethod.Delete, "https://example.com/test")
			.Respond(async () =>
			{
				await Task.Delay(1000);
				return new HttpResponseMessage(HttpStatusCode.NoContent);
			});

		var httpClient = mockHttp.ToHttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestResourceClient(httpClient, uri);
		var cts = new CancellationTokenSource();
		cts.Cancel();

		// Act & Assert
		await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
			client.DeleteAsync(cts.Token));
	}
}
