using Crews.PlanningCenter.Api.Attributes;

namespace Crews.PlanningCenter.Api.Extensions;

static class EnumExtensions
{
	public static string GetJsonApiName<TEnum>(this TEnum source)
	{
		Type enumType = typeof(TEnum);
		System.Reflection.MemberInfo[] memberInfos = enumType.GetMember(source!.ToString()!);
		System.Reflection.MemberInfo? enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
		object[] valueAttributes = enumValueMemberInfo!.GetCustomAttributes(typeof(JsonApiNameAttribute), false);
		return ((JsonApiNameAttribute)valueAttributes[0]).Name;
	}
}
