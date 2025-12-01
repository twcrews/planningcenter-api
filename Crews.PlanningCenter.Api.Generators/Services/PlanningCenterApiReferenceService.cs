using System.Text.Json;
using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Generators.Models;
using Crews.PlanningCenter.Api.Generators.Utilities;

namespace Crews.PlanningCenter.Api.Generators.Services;

public class PlanningCenterApiReferenceService
{
	private readonly HttpClient _client;

	private static JsonException NullJsonElementException => new("Unexpected null value in JSON element");
    private static JsonException BadJsonHierarchyException
        => new("The JSON string is properly formatted, but has an unexpected hierarchy");

    // Certain vertices on the API documentation are incorrectly typed.
    private static readonly Dictionary<string, Type> VertexTypeOverrides = new()
	{
		// https://developer.planning.center/docs/#/apps/services/2018-08-01/vertices/organization
		{ "services:2018-08-01:Organization:plans", typeof(PlanningCenter.Models.Services.V2018_08_01.Entities.Plan) },

		// https://developer.planning.center/docs/#/apps/services/2018-08-01/vertices/organization
		{ "services:2018-11-01:Organization:plans", typeof(PlanningCenter.Models.Services.V2018_11_01.Entities.Plan) }
	};

	public static IEnumerable<string> Products =>
 [
		"calendar",
		"check-ins",
		"giving",
		"groups",
		"people",
		"publishing",
		"services"
 ];

	public PlanningCenterApiReferenceService(HttpClient client)
	{
		client.SafelySetBaseAddress(new("https://api.planningcenteronline.com/"));
		_client = client;
	}
	
	public async Task<IEnumerable<string>> GetVersionsAsync(string product)
	{
		HttpResponseMessage response = await _client.GetAsync($"{product}/v2/documentation");
		await using Stream content = await response.Content.ReadAsStreamAsync();
		JsonDocument document = await JsonDocument.ParseAsync(content);

		IEnumerable<JsonElement> versionsElements = document.RootElement
			.GetProperty("data")
			.GetProperty("relationships")
			.GetProperty("versions")
			.GetProperty("data")
			.EnumerateArray();
			
		return versionsElements.Select(element => element
			.GetProperty("id")
			.GetString() ?? throw NullJsonElementException);
	}

	public async Task<IEnumerable<PlanningCenterResource>> GetResourcesAsync(string product, string version)
	{
		HttpResponseMessage responseMessage = await _client.GetAsync($"{product}/v2/documentation/{version}");
		await using Stream content = await responseMessage.Content.ReadAsStreamAsync();
		JsonDocument document = await JsonDocument.ParseAsync(content);

		List<PlanningCenterResource> resources = [];

		IEnumerable<JsonElement> resourceElements = document.RootElement
			.GetProperty("data")
			.GetProperty("relationships")
			.GetProperty("vertices")
			.GetProperty("data")
			.EnumerateArray();

		foreach (JsonElement resourceElement in resourceElements)
		{
			string vertexId = resourceElement
				.GetProperty("id")
				.GetString() ?? throw NullJsonElementException;

			HttpResponseMessage vertexResponse = await _client
				.GetAsync($"{product}/v2/documentation/{version}/vertices/{vertexId}");
			await using Stream vertexContent = await vertexResponse.Content.ReadAsStreamAsync();
			JsonDocument vertexDocument = await JsonDocument.ParseAsync(vertexContent);

			string formalName = vertexDocument.RootElement
				.GetProperty("data")
				.GetProperty("attributes")
				.GetProperty("name")
				.GetString() ?? throw NullJsonElementException;
			Type clrType = PlanningCenterReflectionUtility.GetResourceEntityType(product, version, formalName);
			
			IEnumerable<JsonElement> outboundElements = vertexDocument.RootElement
				.GetProperty("data")
				.GetProperty("relationships")
				.GetProperty("outbound_edges")
				.GetProperty("data")
				.EnumerateArray();

			JsonElement permissionsElement = vertexDocument.RootElement
				.GetProperty("data")
				.GetProperty("relationships")
				.GetProperty("permissions")
				.GetProperty("data")
				.GetProperty("attributes");
			bool canCreate = permissionsElement
				.GetProperty("can_create")
				.GetBoolean();
			bool canUpdate = permissionsElement
				.GetProperty("can_update")
				.GetBoolean();
			bool canDestroy = permissionsElement
				.GetProperty("can_destroy")
				.GetBoolean();

			List<PlanningCenterResourceVertex> resourceVertices = [];
			foreach (JsonElement outboundElement in outboundElements)
			{
				string vertexFormalName = outboundElement
					.GetProperty("relationships")
					.GetProperty("head")
					.GetProperty("data")
					.GetProperty("attributes")
					.GetProperty("name")
					.GetString() ?? throw NullJsonElementException;
				string vertexName = outboundElement
					.GetProperty("attributes")
					.GetProperty("name")
					.GetString() ?? throw NullJsonElementException;
				resourceVertices.Add(new()
				{
					Resource = new() 
					{ 
						EntityClrType = GetVertexType(product, version, formalName, vertexName, vertexFormalName)
					},
					Name = vertexName
				});
			}

			resources.Add(new()
			{
				EntityClrType = clrType,
				OutboundVertices = resourceVertices,
				CanCreate = canCreate,
				CanUpdate = canUpdate,
				CanDestroy = canDestroy
			});
		}
		return resources;
    }

    public async Task<IEnumerable<PlanningCenterResourceAttributeInfo>> GetAttributesInfoAsync(
        string product, string version, string resource)
    {
        JsonDocument document = await GetResourceDocumentAsync(product, version, resource);

        if (document.RootElement.TryGetProperty("data", out JsonElement data) &&
            data.TryGetProperty("relationships", out JsonElement relationships) &&
            relationships.TryGetProperty("attributes", out JsonElement attributes) &&
            attributes.TryGetProperty("data", out JsonElement attributeData))
        {
            List<JsonElement> attributeElements = [.. attributeData.EnumerateArray()];
            return attributeElements.Select(e =>
            {
                if (e.TryGetProperty("attributes", out JsonElement elementAttributes) &&
                    elementAttributes.TryGetProperty("name", out JsonElement nameElement) &&
                    elementAttributes.TryGetProperty("type_annotation", out JsonElement typeAnnotation) &&
                    typeAnnotation.TryGetProperty("name", out JsonElement typeNameElement))
                {
                    JsonElement descriptionElement = elementAttributes.GetProperty("description");
                    string description = descriptionElement.GetString() ??
                        "Planning Center does not provide a description for this attribute.";

                    string name = nameElement.GetString() ?? throw NullJsonElementException;

                    return new PlanningCenterResourceAttributeInfo
                    {
                        Name = name,
                        Description = description,
                        Type = typeNameElement.GetString() ?? throw NullJsonElementException,
                        ClrTypeName = GetClrTypeName(typeNameElement.GetString() ?? throw NullJsonElementException, name)
                    };
                }
                throw BadJsonHierarchyException;
            });
        }
        throw BadJsonHierarchyException;
    }

    private async Task<JsonDocument> GetResourceDocumentAsync(string product, string version, string resource)
    {
        HttpResponseMessage response = await _client.GetAsync($"{product}/v2/documentation/{version}/vertices/{resource}");
        await using Stream content = await response.Content.ReadAsStreamAsync();
        return await JsonDocument.ParseAsync(content);
    }

    private static string GetClrTypeName(string typeName, string name)
    {
        // Some Organization object docs incorrectly type the date format attribute as an int.
        if (typeName == "integer" && name == "date_format") return "string";
        return GetClrTypeName(typeName);
    }

    private static string GetClrTypeName(string typeName) => typeName switch
    {
        "string" or "primary_key" or "currency_abbreviation" => "string",
        "date_time" => "DateTime",
        "integer" => "int",
        "boolean" => "bool",
        "float" => "double",
        "array" => "IEnumerable<JsonElement>",
        "date" => "DateTime",
        _ => "JsonElement",
    };

    private static Type GetVertexType(
		string product, string version, string resourceFormalName, string vertexName, string vertexFormalName)
	{
		Type? overridingType = VertexTypeOverrides.SingleOrDefault(typeOverride =>
			typeOverride.Key == $"{product}:{version}:{resourceFormalName}:{vertexName}").Value;
		return overridingType ?? PlanningCenterReflectionUtility.GetResourceEntityType(product, version, vertexFormalName);
	}
}
