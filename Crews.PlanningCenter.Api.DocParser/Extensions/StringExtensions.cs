using Humanizer;

namespace Crews.PlanningCenter.Api.DocParser.Extensions;

public static class StringExtensions
{
    public static bool IsPlural(this string target) => !target.Equals(target.Singularize());

	public static string ToClrType(this string jsonType) => jsonType.ToLowerInvariant() switch
	{
		"string" or "primary_key" or "currency_abbreviation" => "string",
		"integer" => "int",
		"boolean" => "bool",
		"float" => "decimal",
		"date_time" => "System.DateTime",
		"date" => "System.DateOnly",
		"json" or "object" or "repeatable_schedule" => "System.Text.Json.Nodes.JsonObject",
		"array" => "System.Text.Json.Nodes.JsonArray",
		_ => "System.Text.Json.JsonElement"
	};
}
