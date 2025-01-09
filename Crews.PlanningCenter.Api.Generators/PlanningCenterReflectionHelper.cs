using System.Reflection;
using Crews.Extensions.Primitives;
using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Entities;

namespace Crews.PlanningCenter.Api.Generators;

public static class PlanningCenterReflectionHelper
{
	private static readonly List<string> _topLevelExclusions = 
	[ 
		"Crews.PlanningCenter.Models.Services.V2018_11_01.Entities.Organization",
		"Crews.PlanningCenter.Models.Services.V2018_08_01.Entities.Organization",
	];

	public static Assembly ModelsAssembly => Assembly.GetAssembly(typeof(Attachment))!;
	public static Type GetResourceEntityType(string product, string version, string resource) => ModelsAssembly
		.GetTypes()
		.Single(type => type.IsMatchingEntity(product, version, resource));

	private static string FormatVersionAsNamespace(string version) => $"V{version}".Replace('-', '_');
	private static bool IsMatchingEntity(this Type type, string product, string version, string resource) =>
		type.FullName != null &&
		(!_topLevelExclusions.Contains(type.FullName)) &&
		type.FullName.Contains(product.ToPascalCase(), StringComparison.OrdinalIgnoreCase) &&
		type.FullName.Contains(FormatVersionAsNamespace(version)) &&
		type.FullName.Contains("Entities") &&
		string.Equals(type.Name, resource, StringComparison.OrdinalIgnoreCase);
}
