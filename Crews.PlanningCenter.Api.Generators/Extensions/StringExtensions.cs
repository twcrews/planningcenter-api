using System.Text.RegularExpressions;

namespace Crews.PlanningCenter.Api.Generators.Extensions;

public static class StringExtensions
{
	public static string ToXmlSummary(this string target) => target
		.Trim()
		.CapitalizeFirstLetter()
		.ReplaceAmpersands()
		.ReplaceTagBrackets()
		.ReplaceLinks()
		.ReplaceNewLines()
		.ReplaceInlineCode();

	private static string CapitalizeFirstLetter(this string target) 
		=> target.Length == 0 ? target : char.ToUpper(target[0]) + target.Substring(1);
	private static string ReplaceAmpersands(this string target) => Regex.Replace(target, @"&(?!amp;)", "&amp;");
	private static string ReplaceTagBrackets(this string target) => target.Replace("<", "&lt;").Replace(">", "&gt;");
	private static string ReplaceLinks(this string target) 
		=> Regex.Replace(target, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");
	private static string ReplaceNewLines(this string target) => Regex.Replace(target, @"[\r\n]", "<br/>");
	private static string ReplaceInlineCode(this string target) => Regex.Replace(target, @"`([^`]+)`", "<c>$1</c>");
}
