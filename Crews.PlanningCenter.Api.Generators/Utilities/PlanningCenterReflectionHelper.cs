using System.Reflection;
using Crews.Extensions.Primitives;
using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Entities;

namespace Crews.PlanningCenter.Api.Generators.Utilities;

public static class PlanningCenterReflectionUtility
{
	public static Assembly ModelsAssembly => Assembly.GetAssembly(typeof(Attachment))!;
	public static Type GetResourceEntityType(string product, string version, string resource) 
		=> ModelsAssembly
		.GetTypes()
		.Single(type => type.IsMatchingEntity(product, version, resource));

	private static string FormatVersionAsNamespace(string version) => $"V{version}".Replace('-', '_');
	private static bool IsMatchingEntity(this Type type, string product, string version, string resource) =>
		type.FullName != null &&
		type.FullName.Contains(product.ToPascalCase(), StringComparison.OrdinalIgnoreCase) &&
		type.FullName.Contains(FormatVersionAsNamespace(version)) &&
		type.FullName.Contains("Entities") &&
		string.Equals(type.Name, resource, StringComparison.OrdinalIgnoreCase);
}
