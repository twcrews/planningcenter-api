using Crews.PlanningCenter.Api.Models.Extensions;

namespace Crews.PlanningCenter.Api.Models.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("hello world", "HelloWorld")]
    [InlineData("this is a test", "ThisIsATest")]
    [InlineData("camelCase", "CamelCase")]
    [InlineData("PascalCase", "PascalCase")]
    [InlineData("snake_case", "SnakeCase")]
    [InlineData("kebab-case", "KebabCase")]
    [InlineData("multiple   spaces", "MultipleSpaces")]
    [InlineData(" leading and trailing spaces ", "LeadingAndTrailingSpaces")]
    [InlineData("ALLCAPS", "Allcaps")]
    [InlineData("ab", "Ab")]
    [InlineData("AB.CDE", "Ab.Cde")]
    [InlineData("123numbers", "123Numbers")]
    [InlineData("special_characters!@#", "SpecialCharacters!@#")]
    [InlineData("hello.world", "Hello.World")]
    [InlineData("2fast2furious", "2Fast2Furious")]
    [InlineData("mixedCASE with123 numbers and_special-characters", "MixedCaseWith123NumbersAndSpecialCharacters")]
    [InlineData("", "")]
    public void ToPascalCase_Correctly_Converts_String(string input, string expected) 
        => Assert.Equal(expected, input.ToPascalCase());
}
