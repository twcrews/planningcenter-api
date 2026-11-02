using System.Text.Json.Nodes;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Deletes test data abandoned by earlier runs. Fixture and test cleanup cannot run if the process is killed (a CI
/// cancellation, a session timeout, a crash), so every run starts by removing whatever previous runs left behind.
/// </summary>
/// <remarks>
/// Only records older than <see cref="StaleAfter"/> are deleted so that concurrent runs, such as CI jobs for different
/// pull requests, can't delete each other's data. A failure here never fails the run; the record is retried next time.
/// </remarks>
public static class TestDataSweeper
{
	/// <summary>Must exceed the longest possible run. The CI job's limit is 30 minutes.</summary>
	public static readonly TimeSpan StaleAfter = TimeSpan.FromHours(1);

	/// <summary>The URL used by every webhook subscription the tests create.</summary>
	public const string WebhookUrl = "https://example.com/webhook-test";

	// Ordered so that dependent records are deleted before the records they depend on.
	private static readonly string[] Collections =
	[
		"calendar/v2/report_templates",
		"calendar/v2/tags",
		"calendar/v2/tag_groups",
		"calendar/v2/resources",
		"calendar/v2/resource_folders",
		"giving/v2/in_kind_donations",
		"giving/v2/batches",
		"giving/v2/batch_groups",
		"giving/v2/labels",
		"giving/v2/pledge_campaigns",
		"giving/v2/payment_sources",
		"giving/v2/funds",
		"people/v2/field_definitions",
		"people/v2/tabs",
		"people/v2/campuses",
		"people/v2/note_categories",
		"people/v2/workflows",
		"people/v2/workflow_categories",
		"people/v2/households",
		"people/v2/people",
		"people/v2/name_prefixes",
		"people/v2/name_suffixes",
		"people/v2/marital_statuses",
		"people/v2/inactive_reasons",
		"people/v2/school_options",
		"people/v2/form_categories",
		"people/v2/list_categories",
		"people/v2/reports",
		"services/v2/folders",
		"services/v2/songs",
		"services/v2/service_types",
	];

	// Test data that lives under the first record of a pre-existing parent collection.
	private static readonly (string Parent, string Child)[] ChildCollections =
	[
		("calendar/v2/events", "event_connections"),
		("services/v2/people", "blockouts"),
	];

	private static readonly object _lock = new();
	private static Task? _sweep;

	/// <summary>Runs the sweep once per test process. All callers wait for the same sweep.</summary>
	public static Task EnsureSweptAsync(HttpClient httpClient)
	{
		lock (_lock)
		{
			return _sweep ??= SweepAsync(httpClient);
		}
	}

	/// <summary>Deletes every record in a collection that matches a predicate. Failures are ignored.</summary>
	public static async Task DeleteWhereAsync(
		HttpClient httpClient, string collectionPath, Func<JsonNode, bool> predicate)
	{
		string? next = $"{collectionPath}?per_page=100";

		// Deleting while paging would shift later records forward, so read every page first.
		List<string> links = [];
		while (next is not null)
		{
			var response = await httpClient.GetAsync(next);
			if (!response.IsSuccessStatusCode) break;

			var document = JsonNode.Parse(await response.Content.ReadAsStringAsync());
			foreach (var record in document?["data"]?.AsArray() ?? [])
			{
				if (record is not null
					&& predicate(record)
					&& record["links"]?["self"]?.GetValue<string>() is string self)
					links.Add(self);
			}

			next = document?["links"]?["next"]?.GetValue<string>();
		}

		foreach (var link in links)
			await httpClient.DeleteAsync(link);
	}

	/// <summary>Determines whether a record is test data abandoned by an earlier run.</summary>
	public static bool IsStaleTestData(JsonNode record, DateTimeOffset now)
	{
		var values = StringAttributes(record).ToList();
		var tagged = values.Where(TestData.IsTagged).ToList();

		return tagged.Count > 0 && IsStale(tagged.Select(TestData.GetCreatedAt).FirstOrDefault(t => t is not null), record, now);
	}

	private static async Task SweepAsync(HttpClient httpClient)
	{
		var now = DateTimeOffset.UtcNow;

		foreach (var collection in Collections)
			await TrySweepAsync(httpClient, collection, record => IsStaleTestData(record, now));

		foreach (var (parent, child) in ChildCollections)
		{
			try
			{
				if (await CollectionReadHelper.GetFirstIdAsync(httpClient, parent) is string parentId)
					await TrySweepAsync(httpClient, $"{parent}/{parentId}/{child}", record => IsStaleTestData(record, now));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Test data sweep of {parent}/{{id}}/{child} failed: {ex.Message}");
			}
		}

		// Webhook subscription names must be valid event names, so they're identified by their URL instead of a tag.
		await TrySweepAsync(httpClient, "webhooks/v2/subscriptions", record =>
			record["attributes"]?["url"]?.GetValue<string>() == WebhookUrl && IsStale(null, record, now));
	}

	private static async Task TrySweepAsync(HttpClient httpClient, string collection, Func<JsonNode, bool> predicate)
	{
		try
		{
			await DeleteWhereAsync(httpClient, collection, predicate);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine($"Test data sweep of {collection} failed: {ex.Message}");
		}
	}

	// A record with no known creation time predates timestamped IDs, so it can't belong to a run in progress.
	private static bool IsStale(DateTimeOffset? createdAt, JsonNode record, DateTimeOffset now)
	{
		createdAt ??= record["attributes"]?["created_at"]?.GetValue<string>() is string value
			&& DateTimeOffset.TryParse(value, out var parsed) ? parsed : null;

		return createdAt is null || now - createdAt >= StaleAfter;
	}

	private static IEnumerable<string> StringAttributes(JsonNode record)
	{
		if (record["attributes"] is not JsonObject attributes) yield break;

		foreach (var (_, value) in attributes)
		{
			if (value is JsonValue jsonValue && jsonValue.TryGetValue<string>(out var text))
				yield return text;
		}
	}
}
