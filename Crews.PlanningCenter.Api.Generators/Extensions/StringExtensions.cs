using System.Text.RegularExpressions;

namespace Crews.PlanningCenter.Api.Generators.Extensions;

public static class StringExtensions
{
	public static string ToXmlSummary(this string target)
	{
		return string.Join("\n", target
			.Trim()
			.FixAmpersands()
			.FixTagBrackets()
			.FixLinks()
			.ReplaceNewLines()
			.FixInlineCode());
	}

	public static string ToSnakeCase(this string target) => target.Replace('-', '_');


    public static string ToClrType(this string jsonType) => jsonType.ToLowerInvariant() switch
    {
        "string" or "primary_key" or "currency_abbreviation" => "string",
        "integer" => "int",
        "boolean" => "bool",
        "float" => "decimal",
        "date_time" => "DateTime",
        "date" => "DateOnly",
        "json" or "object" or "repeatable_schedule" => "JsonObject",
        "array" => "JsonArray",
        _ => "JsonElement"
    };

	private static string ReplaceNewLines(this string target) => Regex.Replace(target, @"[\r\n]", "<br/>");

	private static string FixAmpersands(this string target) => Regex.Replace(target, @"&(?!amp;)", "&amp;");

	private static string FixLinks(this string target) => Regex.Replace(target, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");

	private static string FixInlineCode(this string target) => Regex.Replace(target, @"`([^`]+)`", "<c>$1</c>");

    private static string FixTagBrackets(this string target) => target.Replace('<', '{').Replace('>', '}');
}