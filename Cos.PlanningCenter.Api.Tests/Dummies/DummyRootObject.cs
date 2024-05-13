using System.Text.Json.Serialization;
using Cos.PlanningCenter.Api.Models;

namespace Cos.PlanningCenter.Api.Tests.Dummies;

static partial class Dummy
{
	public static PlanningCenterRootCollectionObject<DummyData> RootCollectionObject
	{
		get
		{
			PlanningCenterRootObject baseObject = GetRootObject();
			return new()
			{
				Data = _dummyData,
				Links = baseObject.Links,
				Included = baseObject.Included,
				Metadata = baseObject.Metadata
			};
		}
	}

	public static PlanningCenterRootSingletonObject<DummyData> RootSingleObject
	{
		get
		{
			PlanningCenterRootObject baseObject = GetRootObject();
			return new()
			{
				Data = _dummyData.First(),
				Links = baseObject.Links,
				Included = baseObject.Included,
				Metadata = baseObject.Metadata
			};
		}
	}

	private static readonly IEnumerable<PlanningCenterDataObject<DummyData>> _dummyData = [
		new()
		{
			Type = "DummyData",
			ID = "123abc",
			Attributes = Dummies.First(),
			Relationships = new Dictionary<string, PlanningCenterRelationship>
			{
				{
					"test_relationship", new()
					{
						Links = new Dictionary<string, Uri>()
						{
							{ "self", new("https://www.test.com") }
						},
						Data = new()
						{
							Type = "TestRelationshipType",
							ID = "1234abcd"
						}
					}
				}
			},
			Links = new Dictionary<string, Uri>()
			{
				{ "self", new("https://www.test.com") }
			}
		},
		new()
		{
			Type = "DummyData",
			ID = "456def",
			Attributes = Dummies.ElementAt(1),
			Relationships = new Dictionary<string, PlanningCenterRelationship>
			{
				{
					"test_relationship", new()
					{
						Links = new Dictionary<string, Uri>()
						{
							{ "self", new("https://www.test.com") }
						},
						Data = new()
						{
							Type = "TestRelationshipType",
							ID = "5678efgh"
						}
					}
				}
			},
			Links = new Dictionary<string, Uri>
			{
				{ "self", new("https://www.test.com") }
			}
		}
	];

	static PlanningCenterRootObject GetRootObject()
		=> new()
	{
		Links = new Dictionary<string, Uri>()
		{
			{ "self", new("https://www.test.com") },
			{ "next", new("https://www.test.com") }
		},
		Included = new List<PlanningCenterDataObject<DummyData>>
		{
			new()
			{
				Type = "DummyData",
				ID = "12ab",
				Attributes = new()
				{
					IntAttribute = 789,
					StringAttribute = "ghi",
					BoolAttribute = true
				}
			}
		},
		Metadata = new()
		{
			TotalCount = 8,
			Count = 2,
			NextPage = new() { Offset = 4 },
			PreviousPage = new() { Offset = 0 },
			OrderableAttributes = [
				"int_attribute",
				"string_attribute",
				"bool_attribute"
			],
			QueriableAttributes = [
				"int_attribute",
				"string_attribute",
				"bool_attribute"
			],
			IncludableProperties = [
				"tags"
			],
			FilterableProperties = [
				"future",
				"all"
			],
			Parent = new()
			{
				ID = "555",
				Type = "Organization"
			}
		}
	};

	public static IEnumerable<DummyData> Dummies => [
		new()
		{
			IntAttribute = 123,
			StringAttribute = "abc",
			BoolAttribute = true
		},
		new()
		{
			IntAttribute = 456,
			StringAttribute = "def",
			BoolAttribute = false
		},
	];
}

class DummyData
{
	[JsonPropertyName("int_attribute")]
	public required int IntAttribute { get; init; }

	[JsonPropertyName("string_attribute")]
	public required string StringAttribute { get; init; }

	[JsonPropertyName("bool_attribute")]
	public required bool BoolAttribute { get; init; }

	[JsonPropertyName("null_attribute")]
	public string? NullAttribute { get; init; }
}