using Crews.PlanningCenter.Api.DocParser.Extensions;

namespace Crews.PlanningCenter.Api.DocParser.Tests.Extensions;

public class StringExtensionsTests
{
  [Theory(DisplayName = "ToPascalCase returns correct string for valid input")]
  [InlineData("helloWorld")]
  [InlineData("hello-world")]
  [InlineData("hello world")]
  [InlineData("hello_world")]
  [InlineData("HELLO_WORLD")]
  [InlineData("HelloWorld")]
  public void ToPascalCase_returns_valid_string(string input)
  {
    string expected = "HelloWorld";
    string actual = input.ToPascalCase();
    Assert.Equal(expected, actual);
  }

  [Theory(DisplayName = "ToPascalCase returns same string for empty or whitespace input")]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData("     ")]
  public void ToPascalCase_returns_same_string_for_empty_or_whitespace(string input)
  {
    string actual = input.ToPascalCase();
    Assert.Equal(input, actual);
  }
}
