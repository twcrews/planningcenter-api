using System;
using System.Text.Json;
using Crews.Extensions.Http;

namespace Crews.PlanningCenter.Api.Generators;

public class PlanningCenterApiReferenceService
{
	private readonly HttpClient _client;

	private static JsonException NullJsonElementException => new("Unexpected null value in JSON element");
	private static JsonException BadJsonHierarchyException
		=> new("The JSON string is properly formatted, but has an unexpected hierarchy");

	public PlanningCenterApiReferenceService(HttpClient client)
	{
		client.SafelySetBaseAddress(new("https://api.planningcenteronline.com/"));
		_client = client;
	}

	public async Task<IEnumerable<Type>> GetTopLevelResourceEntityTypesAsync(string product, string version)
	{
		HttpResponseMessage responseMessage = await _client.GetAsync($"{product}/v2/documentation/{version}");
		await using Stream content = await responseMessage.Content.ReadAsStreamAsync();
		JsonDocument document = await JsonDocument.ParseAsync(content);

		return document.RootElement
			.GetProperty("data")
			.GetProperty("relationships")
			.GetProperty("entry")
			.GetProperty("data")
			.GetProperty("relationships")
			.GetProperty("outbound_edges")
			.GetProperty("data")
			.EnumerateArray()
			.Select(element => element
				.GetProperty("relationships")
				.GetProperty("head")
				.GetProperty("data")
				.GetProperty("attributes")
				.GetProperty("name")
				.GetString())
			.Select(value => value ?? throw NullJsonElementException)
			.Select(value => PlanningCenterReflectionHelper.GetResourceEntityType(product, version, value));
	}
}
