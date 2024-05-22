namespace Crews.PlanningCenter.Api.Tests.Dummies.Serialized;

static partial class Serialized
{
	public const string DummyRootCollectionObject = """
	{
		"links": {
			"self": "https://www.test.com",
			"next": "https://www.test.com"
		},
		"data": [
			{
				"type": "DummyData",
				"id": "123abc",
				"attributes": {
					"int_attribute": 123,
					"string_attribute": "abc",
					"bool_attribute": true,
					"null_attribute": null
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
			{
				"type": "DummyData",
				"id": "456def",
				"attributes": {
					"int_attribute": 456,
					"string_attribute": "def",
					"bool_attribute": false,
					"null_attribute": null
				},
				"relationships": {
					"test_relationship": {
						"links": {
							"self": "https://www.test.com"
						},
						"data": {
							"type": "TestRelationshipType",
							"id": "5678efgh"
						}
					}
				},
				"links": {
					"self": "https://www.test.com"
				}
			}
		],
		"included": [
			{
				"type": "DummyData",
				"id": "12ab",
				"attributes": {
						"int_attribute": 789,
						"string_attribute": "ghi",
						"bool_attribute": true,
						"null_attribute": null
				}
			}
		],
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