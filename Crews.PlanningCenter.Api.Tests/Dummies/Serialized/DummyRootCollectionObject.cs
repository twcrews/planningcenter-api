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
				"type": "DummyResource",
				"id": "123abc",
				"attributes": {
					"age": 28,
					"name": "Tommy"
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
				"type": "DummyResource",
				"id": "456def",
				"attributes": {
					"age": 26,
					"name": "Bri"
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
				"type": "DummyResource",
				"id": "12ab",
				"attributes": {
					"age": 0,
					"name": "Eli"
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


	public const string DummyRootCollectionNoMetaObject = """
	{
		"links": {
			"self": "https://www.test.com",
			"next": "https://www.test.com"
		},
		"data": [
			{
				"type": "DummyResource",
				"id": "123abc",
				"attributes": {
					"age": 28,
					"name": "Tommy"
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
				"type": "DummyResource",
				"id": "456def",
				"attributes": {
					"age": 26,
					"name": "Bri"
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
				"type": "DummyResource",
				"id": "12ab",
				"attributes": {
					"age": 0,
					"name": "Eli"
				}
			}
		]
	}
	""";
}