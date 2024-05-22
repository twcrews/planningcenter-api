using Crews.PlanningCenter.Api.Conventions;
using JsonApiFramework.Conventions;

namespace Crews.PlanningCenter.Api.Extensions;

static class ConventionsBuilderExtensions
{
	public static INamingConventionsBuilder AddSnakeCaseNamingConvention(this INamingConventionsBuilder builder)
	{
		builder.AddCustomNamingConvention(new SnakeCaseNamingConvention());
		return builder;
	}
}