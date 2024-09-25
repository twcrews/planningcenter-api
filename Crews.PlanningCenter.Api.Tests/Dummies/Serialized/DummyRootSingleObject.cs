namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyRootSingleObject = """
	{
		"links": {
			"self": "https://www.test.com",
			"next": "https://www.test.com"
		},
		"data": {
			"type": "DummyResource",
			"id": "123abc",
			"attributes": {
				"name": "Tommy",
				"age": 28
			},
			"relationships": {
				"test_relationship": {
					"links": {
						"self": "https://www.test.com"
					},
					"data": {
						"type": "TestRelationshipType",
						"id": "1234abcd"
					}
				}
			},
			"links": {
				"self": "https://www.test.com"
			}
		},
		"meta": {
			"total_count": 8,
			"count": 2,
			"next": {
				"offset": 4
			},
			"prev": {
				"offset": 0
			},
			"can_order_by": [
				"int_attribute",
				"string_attribute",
				"bool_attribute"
			],
			"can_query_by": [
				"int_attribute",
				"string_attribute",
				"bool_attribute"
			],
			"can_include": [
				"tags"
			],
			"can_filter": [
				"future",
				"all"
			],
			"parent": {
				"id": "555",
				"type": "Organization"
			}
		}
	}
	""";
}