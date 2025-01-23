namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyPublishingOrganizationObject = """
	{
		"data": {
			"type": "Organization",
			"id": "123",
			"attributes": {
				"name": "TestOrg",
				"subdomain": "test"
			},
			"links": {}
		},
		"included": [],
		"meta": {}
	}
	""";
}