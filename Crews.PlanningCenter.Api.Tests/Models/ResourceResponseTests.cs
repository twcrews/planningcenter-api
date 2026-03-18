using Crews.PlanningCenter.Api.Tests.Dummies;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class ResourceResponseTests
{
	[Fact]
	public void Properties_CanBeInitialized()
	{
		// Arrange
		var model = new TestModel { Name = "Test", Age = 25 };
		var httpResponse = new HttpResponseMessage();

		// Act
		var response = new TestResourceResponse
		{
			Data = new() { Attributes = model, Type = "Test"},
			Document = null,
			ResponseMessage = httpResponse
		};

		// Assert
		Assert.Equal(model, response.Data.Attributes);
		Assert.Null(response.Document);
		Assert.Equal(httpResponse, response.ResponseMessage);
	}

	[Fact]
	public void Data_CanBeNull()
	{
		// Arrange
		var httpResponse = new HttpResponseMessage();

		// Act
		var response = new TestResourceResponse
		{
			Data = null,
			Document = null,
			ResponseMessage = httpResponse
		};

		// Assert
		Assert.Null(response.Data);
	}

	[Fact]
	public void Document_CanBeNull()
	{
		// Arrange
		var httpResponse = new HttpResponseMessage();

		// Act
		var response = new TestResourceResponse
		{
			Data = null,
			Document = null,
			ResponseMessage = httpResponse
		};

		// Assert
		Assert.Null(response.Document);
	}

	[Fact]
	public void ResponseMessage_IsRequired()
	{
		// Arrange & Act & Assert
		// This test verifies that ResponseMessage is required by the compiler
		// The following would not compile without ResponseMessage:
		// var response = new TestResourceResponse { Data = null };

		var httpResponse = new HttpResponseMessage();
		var response = new TestResourceResponse
		{
			ResponseMessage = httpResponse
		};

		Assert.NotNull(response.ResponseMessage);
	}
}
