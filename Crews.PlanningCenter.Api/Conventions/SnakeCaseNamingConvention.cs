using Humanizer;
using JsonApiFramework.Conventions;

namespace Crews.PlanningCenter.Api.Conventions;

class SnakeCaseNamingConvention : INamingConvention
{
	public string Apply(string name) => string.IsNullOrWhiteSpace(name) ? name : name.Humanize().ToLower().Underscore();
}
