using System.Text.Json;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class JsonApiExceptionTests
{
	[Fact]
	public void Constructor_WithValidErrorJson_ParsesErrors()
	{
		// Arrange
		var innerException = new HttpRequestException("Request failed");

		// Act
		var exception = new JsonApiException(Serialized.DummyErrorObject, innerException);

		// Assert
		Assert.NotEmpty(exception.Errors);
		Assert.Single(exception.Errors);
	}

	[Fact]
	public void Constructor_WithMultipleErrors_ParsesAll()
	{
		// Arrange
		var innerException = new HttpRequestException("Request failed");

		// Act
		var exception = new JsonApiException(Serialized.DummyErrorCollectionObject, innerException);

		// Assert
		Assert.NotEmpty(exception.Errors);
		Assert.Equal(3, exception.Errors.Length);
	}

	[Fact]
	public void Constructor_WithInnerException_StoresInner()
	{
		// Arrange
		var innerException = new HttpRequestException("Original error message");

		// Act
		var exception = new JsonApiException(Serialized.DummyErrorObject, innerException);

		// Assert
		Assert.Equal(innerException, exception.InnerException);
		Assert.Equal("Original error message", exception.Message);
	}

	[Fact]
	public void Constructor_WithNullInnerException_HandlesGracefully()
	{
		// Act
		var exception = new JsonApiException(Serialized.DummyErrorObject, null);

		// Assert
		Assert.Null(exception.InnerException);
		Assert.NotEmpty(exception.Errors);
	}

	[Fact]
	public void Constructor_WithNoErrors_CreatesEmptyArray()
	{
		// Arrange
		var innerException = new HttpRequestException("Request failed");
		var jsonWithoutErrors = """{"data": null}""";

		// Act
		var exception = new JsonApiException(jsonWithoutErrors, innerException);

		// Assert
		Assert.Empty(exception.Errors);
	}

	[Fact]
	public void Errors_WithValidJsonApiErrors_DeserializesCorrectly()
	{
		// Arrange
		var innerException = new HttpRequestException("Request failed");

		// Act
		var exception = new JsonApiException(Serialized.DummyErrorObject, innerException);

		// Assert
		var error = exception.Errors[0];
		Assert.Equal("123", error.Id);
		Assert.Equal("403", error.Status);
		Assert.Equal("Forbidden", error.Title);
		Assert.Equal("You do not have access to this resource", error.Detail);
		Assert.Equal("sample_error_code", error.Code);
	}

	[Fact]
	public void Errors_ContainsExpectedErrorDetails()
	{
		// Arrange
		var innerException = new HttpRequestException("Request failed");

		// Act
		var exception = new JsonApiException(Serialized.DummyErrorCollectionObject, innerException);

		// Assert
		Assert.Contains(exception.Errors, e => e.Status == "403" && e.Title == "Forbidden");
		Assert.Contains(exception.Errors, e => e.Status == "500" && e.Title == "Internal Server Error");
		Assert.Contains(exception.Errors, e => e.Status == "401" && e.Title == "Unauthorized");
	}
}
