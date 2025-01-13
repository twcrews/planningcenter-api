using Crews.PlanningCenter.Api.Generators.Models;
using Crews.PlanningCenter.Api.Generators.Utilities;

namespace Crews.PlanningCenter.Api.Generators.Extensions;

public static class TypeExtensions
{
	public static bool HasIncludables(this PlanningCenterResource resource) => resource.HasParameter("Includable");
	public static bool HasOrderables(this PlanningCenterResource resource) => resource.HasParameter("Orderable");
	public static bool HasQueryables(this PlanningCenterResource resource) => resource.HasParameter("Queryable");

	private static bool HasParameter(this PlanningCenterResource resource, string parameterSuffix)
	{
		if (resource.EntityClrType.FullName == null) return false;
		string resourceNamespace = string.Join('.', resource.EntityClrType.FullName.Split('.')[..^2]);
		string parameterTypeName = $"{resourceNamespace}.Parameters.{resource.EntityClrType.Name}{parameterSuffix}";
		return TypeUtilities.FindTypeInAllAssemblies(parameterTypeName) != null;
	}
}
