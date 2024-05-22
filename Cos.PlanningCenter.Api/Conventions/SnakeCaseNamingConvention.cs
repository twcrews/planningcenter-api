using Humanizer;
using JsonApiFramework.Conventions;

namespace Cos.PlanningCenter.Api.Conventions;

class SnakeCaseNamingConvention : INamingConvention
{
	public string Apply(string name) => string.IsNullOrWhiteSpace(name) ? name : name.Humanize().ToLower().Underscore();
}
