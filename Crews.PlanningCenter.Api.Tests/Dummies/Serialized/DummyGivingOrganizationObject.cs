namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyGivingOrganizationObject = """
	{
		"data": {
			"type": "Organization",
			"id": "123",
			"attributes": {
				"name": "TestOrg",
				"time_zone": "America/Chicago"
			},
			"links": {}
		},
		"included": [],
		"meta": {}
	}
	""";
}