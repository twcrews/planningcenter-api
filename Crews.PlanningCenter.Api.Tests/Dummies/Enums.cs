using Crews.PlanningCenter.Api.Attributes;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

public enum DummyEnum
{
	[JsonApiName("first_value")]
	First,
	[JsonApiName("second_value")]
	Second
}