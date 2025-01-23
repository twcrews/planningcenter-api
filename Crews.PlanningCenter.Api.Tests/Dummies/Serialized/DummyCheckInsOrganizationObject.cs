namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyCheckInsOrganizationObject = """
	{
		"data": {
			"type": "Organization",
			"id": "123",
			"attributes": {
				"avatar_url": "http://test.abc/avatar.png",
				"created_at": "2025-01-01T00:00:00Z",
				"daily_check_ins": 100,
				"date_format_pattern": "%-m/%-d",
				"name": "TestOrg",
				"time_zone": "Central Time (US & Canada)",
				"time_zone_olson": "America/Chicago",
				"updated_at": "2025-01-01T01:01:01Z"
			},
			"links": {}
		},
		"included": [],
		"meta": {}
	}
	""";
}