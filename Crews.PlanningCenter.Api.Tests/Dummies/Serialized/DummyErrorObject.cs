namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyErrorObject = """
	{
		"errors": [
			{
				"status": "403",
				"title": "Forbidden",
				"detail": "You do not have access to this resource",
				"code": "sample_error_code",
				"meta": {
					"description": "This is a sample description."
				}
			}
		]
	}
	""";
}
