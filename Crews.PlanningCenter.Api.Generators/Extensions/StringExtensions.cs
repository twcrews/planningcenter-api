using System.Text.RegularExpressions;

namespace Crews.PlanningCenter.Api.Generators.Extensions;

public static class StringExtensions
{
	public static string ToXmlDocSummary(this string target, int indentSpaces = 0)
	{
		string indent = indentSpaces > 0 ? new(' ', indentSpaces) : string.Empty;

		string content = string.Join('\n', target
			.Trim()
			.RecursivelyRemoveTripleNewLines()
			.FixAmpersands()
			.FixLinks()
			.FixInlineCode()
			.Split('\n', StringSplitOptions.TrimEntries)
			.Select(substring => $"{indent}/// {substring}"));

		return $"{indent}/// <summary>\n"
			+ content
			+ $"\n{indent}/// </summary>";
	}

	public static string ToSnakeCase(this string target) => target.Replace('-', '_');

	private static string RecursivelyRemoveTripleNewLines(this string target)
	{
		if (target.Contains("\n\n\n"))
		{
			return target.Replace("\n\n\n", "\n\n").RecursivelyRemoveTripleNewLines();
		}
		return target;
	}

	private static string FixAmpersands(this string target) => Regex.Replace(target, @"&(?!amp;)", "&amp;");

	private static string FixLinks(this string target) => Regex.Replace(target, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");

	private static string FixInlineCode(this string target) => Regex.Replace(target, @"`([^`]+)`", "<c>$1</c>");

	private static string FixTagBrackets(this string target) => target.Replace('<', '{').Replace('>', '}');
}