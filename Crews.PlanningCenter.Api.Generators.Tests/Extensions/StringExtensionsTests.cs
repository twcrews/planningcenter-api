using Crews.PlanningCenter.Api.Generators.Extensions;

namespace Crews.PlanningCenter.Api.Generators.Tests.Extensions;

public class StringExtensionsTests
{
    public class ToXmlSummary
    {
        [Fact]
        public void ShouldTrimWhitespace()
        {
            // Arrange
            string input = "  Test description  ";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("Test description", result);
        }

        [Fact]
        public void ShouldFixAmpersands()
        {
            // Arrange
            string input = "This & that";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("This &amp; that", result);
        }

        [Fact]
        public void ShouldPreserveExistingAmpersandEntities()
        {
            // Arrange
            string input = "This &amp; that";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("This &amp; that", result);
        }

        [Fact]
        public void ShouldFixTagBrackets()
        {
            // Arrange
            string input = "Use <div> tag";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("Use {div} tag", result);
        }

        [Fact]
        public void ShouldConvertMarkdownLinksToHtmlLinks()
        {
            // Arrange
            string input = "Check [this link](https://example.com) out";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("Check <a href=\"https://example.com\">this link</a> out", result);
        }

        [Fact]
        public void ShouldReplaceNewLinesWithBreakTags()
        {
            // Arrange
            string input = "Line 1\nLine 2\rLine 3";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("Line 1<br/>Line 2<br/>Line 3", result);
        }

        [Fact]
        public void ShouldConvertInlineCodeToCodeTags()
        {
            // Arrange
            string input = "Use the `code` function";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("Use the <c>code</c> function", result);
        }

        [Fact]
        public void ShouldHandleMultipleInlineCodeBlocks()
        {
            // Arrange
            string input = "Use `foo` and `bar` together";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("Use <c>foo</c> and <c>bar</c> together", result);
        }

        [Fact]
        public void ShouldHandleComplexDescription()
        {
            // Arrange
            string input = "  This & that uses `code` and has a [link](https://example.com).\nNew line here.  ";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("This &amp; that uses <c>code</c> and has a <a href=\"https://example.com\">link</a>.<br/>New line here.", result);
        }

        [Fact]
        public void ShouldHandleEmptyString()
        {
            // Arrange
            string input = "";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ShouldHandleWhitespaceOnlyString()
        {
            // Arrange
            string input = "   \n  \r  ";

            // Act
            string result = input.ToXmlSummary();

            // Assert
            Assert.Equal("", result);
        }
    }

    public class ToClrType
    {
        [Theory]
        [InlineData("string", "string")]
        [InlineData("STRING", "string")]
        [InlineData("String", "string")]
        [InlineData("primary_key", "string")]
        [InlineData("currency_abbreviation", "string")]
        public void ShouldConvertStringTypes(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("integer", "int")]
        [InlineData("INTEGER", "int")]
        public void ShouldConvertIntegerType(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("boolean", "bool")]
        [InlineData("BOOLEAN", "bool")]
        public void ShouldConvertBooleanType(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("float", "decimal")]
        [InlineData("FLOAT", "decimal")]
        public void ShouldConvertFloatType(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("date_time", "DateTime")]
        [InlineData("DATE_TIME", "DateTime")]
        public void ShouldConvertDateTimeType(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("date", "DateOnly")]
        [InlineData("DATE", "DateOnly")]
        public void ShouldConvertDateType(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("json", "JsonObject")]
        [InlineData("object", "JsonObject")]
        [InlineData("repeatable_schedule", "JsonObject")]
        [InlineData("JSON", "JsonObject")]
        public void ShouldConvertJsonObjectTypes(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("array", "JsonArray")]
        [InlineData("ARRAY", "JsonArray")]
        public void ShouldConvertArrayType(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("unknown", "JsonElement")]
        [InlineData("custom_type", "JsonElement")]
        [InlineData("", "JsonElement")]
        public void ShouldConvertUnknownTypesToJsonElement(string jsonType, string expected)
        {
            // Act
            string result = jsonType.ToClrType();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
