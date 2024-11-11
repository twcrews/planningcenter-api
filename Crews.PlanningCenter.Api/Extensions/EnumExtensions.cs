using Crews.PlanningCenter.Api.Attributes;
using Crews.PlanningCenter.Api.Conventions;

namespace Crews.PlanningCenter.Api.Extensions;

static class EnumExtensions
{
	public static string GetJsonApiName<TEnum>(this TEnum source)
	{
		Type enumType = typeof(TEnum);
		System.Reflection.MemberInfo[] memberInfos = enumType.GetMember(source!.ToString()!);
		System.Reflection.MemberInfo? enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
		object[] valueAttributes = enumValueMemberInfo!.GetCustomAttributes(typeof(JsonApiNameAttribute), false);

		if (valueAttributes.Length == 0)
		{
			return new SnakeCaseNamingConvention().Apply(enumValueMemberInfo!.Name);
		}
		return ((JsonApiNameAttribute)valueAttributes[0]).Name;
	}
}
