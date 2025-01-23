namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyCalendarOrganizationObject = """
	{
		"data": {
			"type": "Organization",
			"id": "123",
			"attributes": {
				"calendar_starts_on": "Sunday",
				"date_format": "%m/%d/%Y",
				"name": "TestOrg",
				"time_zone": "America/Chicago",
				"twenty_four_hour_time": false
			},
			"links": {}
		},
		"included": [],
		"meta": {}
	}
	""";
}