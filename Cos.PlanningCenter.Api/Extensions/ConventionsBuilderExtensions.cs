using Cos.PlanningCenter.Api.Conventions;
using JsonApiFramework.Conventions;

namespace Cos.PlanningCenter.Api.Extensions;

static class ConventionsBuilderExtensions
{
	public static INamingConventionsBuilder AddSnakeCaseNamingConvention(this INamingConventionsBuilder builder)
	{
		builder.AddCustomNamingConvention(new SnakeCaseNamingConvention());
		return builder;
	}
}