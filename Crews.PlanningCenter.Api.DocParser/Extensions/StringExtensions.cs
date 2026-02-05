using Humanizer;

namespace Crews.PlanningCenter.Api.DocParser.Extensions;

public static class StringExtensions
{
    public static bool IsPlural(this string target) => !target.Equals(target.Singularize());
}
