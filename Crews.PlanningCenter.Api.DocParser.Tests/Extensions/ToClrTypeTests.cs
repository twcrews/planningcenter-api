using Crews.PlanningCenter.Api.DocParser.Extensions;

namespace Crews.PlanningCenter.Api.DocParser.Tests.Extensions;

public class ToClrTypeTests
{
    [Theory(DisplayName = "string types map to string")]
    [InlineData("string")]
    [InlineData("primary_key")]
    [InlineData("currency_abbreviation")]
    public void ToClrType_StringTypes_ReturnsString(string jsonType)
        => Assert.Equal("string", jsonType.ToClrType());

    [Fact(DisplayName = "integer maps to int")]
    public void ToClrType_Integer_ReturnsInt()
        => Assert.Equal("int", "integer".ToClrType());

    [Fact(DisplayName = "boolean maps to bool")]
    public void ToClrType_Boolean_ReturnsBool()
        => Assert.Equal("bool", "boolean".ToClrType());

    [Fact(DisplayName = "float maps to decimal")]
    public void ToClrType_Float_ReturnsDecimal()
        => Assert.Equal("decimal", "float".ToClrType());

    [Fact(DisplayName = "date_time maps to System.DateTime")]
    public void ToClrType_DateTime_ReturnsSystemDateTime()
        => Assert.Equal("System.DateTime", "date_time".ToClrType());

    [Fact(DisplayName = "date maps to System.DateOnly")]
    public void ToClrType_Date_ReturnsSystemDateOnly()
        => Assert.Equal("System.DateOnly", "date".ToClrType());

    [Theory(DisplayName = "json/object/repeatable_schedule map to JsonObject")]
    [InlineData("json")]
    [InlineData("object")]
    [InlineData("repeatable_schedule")]
    public void ToClrType_ObjectTypes_ReturnsJsonObject(string jsonType)
        => Assert.Equal("System.Text.Json.Nodes.JsonObject", jsonType.ToClrType());

    [Fact(DisplayName = "array maps to JsonArray")]
    public void ToClrType_Array_ReturnsJsonArray()
        => Assert.Equal("System.Text.Json.Nodes.JsonArray", "array".ToClrType());

    [Theory(DisplayName = "unrecognized types fall back to JsonElement")]
    [InlineData("unknown")]
    [InlineData("")]
    [InlineData("guid")]
    public void ToClrType_UnknownType_ReturnsJsonElement(string jsonType)
        => Assert.Equal("System.Text.Json.JsonElement", jsonType.ToClrType());

    [Theory(DisplayName = "type matching is case-insensitive")]
    [InlineData("Boolean")]
    [InlineData("BOOLEAN")]
    [InlineData("Float")]
    [InlineData("DATE_TIME")]
    [InlineData("Array")]
    public void ToClrType_IsCaseInsensitive(string jsonType)
        => Assert.NotEqual("System.Text.Json.JsonElement", jsonType.ToClrType());
}
