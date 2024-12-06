﻿using Crews.PlanningCenter.Models;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

public enum DummyEnum
{
	[JsonApiName("first_value")]
	First,
	[JsonApiName("second_value")]
	Second,
	ValueWithoutAttribute
}