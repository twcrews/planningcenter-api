namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyInvalidObject = """
	{
		"invalid": "I am a valid JSON object, but not a valid JSON:API document!"
	}
	""";

	public const string DummyErrorObject = """
	{
		"errors": [
			{
				"id": "123",
				"links": {
					"self": "http://me.com/",
					"error": "http://error.co"
				},
				"status": "403",
				"title": "Forbidden",
				"detail": "You do not have access to this resource",
				"code": "sample_error_code",
				"source": {
					"header": "auth"
				},
				"meta": {
					"description": "This is a sample description."
				}
			}
		]
	}
	""";

	public const string DummyErrorCollectionObject = """
	{
		"errors": [
			{
				"id": "123",
				"links": {
					"self": "http://me.com/",
					"error": "http://error.co"
				},
				"status": "403",
				"title": "Forbidden",
				"detail": "You do not have access to this resource",
				"code": "sample_error_code",
				"source": {
					"header": "auth"
				},
				"meta": {
					"description": "This is a sample description."
				}
			},
			{
				"status": "500",
				"title": "Internal Server Error",
				"detail": "Something broke!",
				"code": "sample_error_code",
				"source": {
					"pointer": "what's a pointer"
				},
				"meta": {
					"description": "This is a sample description."
				}
			},
			{
				"status": "401",
				"title": "Unauthorized",
				"detail": "Go authenticate yourself",
				"code": "sample_error_code",
				"source": {
					"parameter": "what's a parameter"
				}
			},
			{
				"status": "300",
				"title": "What even is a 300",
				"source": {
					"test": "invalid property name"
				}
			},
			{
				"source": {
					"pointer": [
						{
							"invalid": "source"
						}
					]
				}
			},
			{
				"status": "700",
				"source": {}
			}
		]
	}
	""";
}
