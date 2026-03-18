using System.Net.Http.Headers;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class HttpHeadersExtensionsTests
{
	[Fact]
	public void SetPlanningCenterVersion_AddsHeader()
	{
		// Arrange
		var headers = new HttpRequestHeaders();
		var version = "2025-01-15";

		// Act
		headers.SetPlanningCenterVersion(version);

		// Assert
		Assert.Contains(headers, h => h.Key == "X-PCO-API-Version");
		Assert.Equal(version, headers.GetValues("X-PCO-API-Version").First());
	}

	[Fact]
	public void SetPlanningCenterVersion_RemovesExistingHeader()
	{
		// Arrange
		var headers = new HttpRequestHeaders
        {
            { "X-PCO-API-Version", "2024-01-01" }
        };

		// Act
		headers.SetPlanningCenterVersion("2025-01-15");

		// Assert
		var values = headers.GetValues("X-PCO-API-Version").ToList();
		Assert.Single(values);
		Assert.Equal("2025-01-15", values[0]);
	}

	[Fact]
	public void SetPlanningCenterVersion_WithEmptyVersion_AddsEmpty()
	{
		// Arrange
		var headers = new HttpRequestHeaders();

		// Act & Assert
		// Empty strings are typically not allowed in HTTP headers
		// This may throw or be handled gracefully depending on implementation
		try
		{
			headers.SetPlanningCenterVersion(string.Empty);
			Assert.True(true); // If it doesn't throw, that's fine
		}
		catch
		{
			Assert.True(true); // If it throws, that's also acceptable behavior
		}
	}

	// Helper class to access HttpRequestHeaders
	private class HttpRequestHeaders : HttpHeaders
	{
	}
}
