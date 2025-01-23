namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyPeopleOrganizationObject = """
	{
		"data": {
			"type": "Organization",
			"id": "123",
			"attributes": {
				"avatar_url": "http://test.abc/avatar.png",
				"contact_website": "http://test.abc",
				"country_code": "US",
				"created_at": "2025-01-01T00:00:00Z",
				"date_format": "%m/%d/%Y",
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