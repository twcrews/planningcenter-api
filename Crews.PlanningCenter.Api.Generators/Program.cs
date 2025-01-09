// This is intended to be used as a testing sandbox for the generator project.

using Crews.PlanningCenter.Api.Generators;

PlanningCenterApiReferenceService service = new(new());
IEnumerable<Type> types = await service.GetTopLevelResourceEntityTypesAsync("services", "2018-08-01");
foreach (Type type in types) Console.WriteLine(type.FullName);