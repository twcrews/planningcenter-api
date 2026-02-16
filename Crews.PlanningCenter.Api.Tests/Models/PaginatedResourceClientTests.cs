using Crews.PlanningCenter.Api.Tests.Dummies;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PaginatedResourceClientTests
{
	[Fact]
	public void Take_SetsPerPageParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Take(25);

		// Assert
		Assert.Contains("per_page=25", client.Uri.ToString());
	}

	[Fact]
	public void Take_UpdatesUri()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test?per_page=10");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Take(50);

		// Assert
		Assert.Contains("per_page=50", client.Uri.ToString());
		Assert.DoesNotContain("per_page=10", client.Uri.ToString());
	}

	[Fact]
	public void Take_ReturnsClientInstance_ForChaining()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.Take(25);

		// Assert
		Assert.Same(client, result);
	}

	[Fact]
	public void Take_WithZero_SetsZero()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Take(0);

		// Assert
		Assert.Contains("per_page=0", client.Uri.ToString());
	}

	[Fact]
	public void Skip_SetsOffsetParameter()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Skip(100);

		// Assert
		Assert.Contains("offset=100", client.Uri.ToString());
	}

	[Fact]
	public void Skip_UpdatesUri()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test?offset=50");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Skip(200);

		// Assert
		Assert.Contains("offset=200", client.Uri.ToString());
		Assert.DoesNotContain("offset=50", client.Uri.ToString());
	}

	[Fact]
	public void Skip_ReturnsClientInstance_ForChaining()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.Skip(100);

		// Assert
		Assert.Same(client, result);
	}

	[Fact]
	public void Skip_WithZero_SetsZero()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		client.Skip(0);

		// Assert
		Assert.Contains("offset=0", client.Uri.ToString());
	}

	[Fact]
	public void TakeAndSkip_CanBeChained()
	{
		// Arrange
		var httpClient = new HttpClient();
		var uri = new Uri("https://example.com/test");
		var client = new TestPaginatedResourceClient(httpClient, uri);

		// Act
		var result = client.Take(25).Skip(50);

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
		client.Take(10).Skip(20).AddCustomParameter("filter", "active");

		// Assert
		var uriString = client.Uri.ToString();
		Assert.Contains("per_page=10", uriString);
		Assert.Contains("offset=20", uriString);
		Assert.Contains("filter=active", uriString);
	}
}
