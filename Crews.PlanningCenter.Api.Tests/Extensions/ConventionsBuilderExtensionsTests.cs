using Crews.PlanningCenter.Api.Conventions;
using Crews.PlanningCenter.Api.Extensions;
using JsonApiFramework.Conventions;
using NSubstitute;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class ConventionsBuilderExtensionsTests
{
	[Fact(DisplayName = "AddSnakeCaseNamingConvention adds correct convention to builder object")]
	public void AddSnakeCaseNamingConventionAddsConvention()
	{
		INamingConventionsBuilder builder = Substitute.For<INamingConventionsBuilder>();
		builder.AddSnakeCaseNamingConvention();
		builder.Received().AddCustomNamingConvention(Arg.Any<SnakeCaseNamingConvention>());
	}
}
