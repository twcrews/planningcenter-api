using System.Text.Json;
using System.Text.Json.Serialization;
using Cos.PlanningCenter.Api.Models;

namespace Cos.PlanningCenter.Api.Services;

/// <summary>
/// A low-level service used for interacting with the Planning Center API.
/// </summary>
public class PlanningCenterApiClient(HttpClient httpClient)
{
	private readonly HttpClient _httpClient = httpClient;
	private readonly JsonSerializerOptions _serializerOptions = new()
	{
		NumberHandling = JsonNumberHandling.AllowReadingFromString
	};

	/// <inheritdoc />
	public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
	{
		HttpResponseMessage responseMessage = await _httpClient.SendAsync(request);
		if (!responseMessage.IsSuccessStatusCode)
		{
			string content = await responseMessage.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(content))
			{
				throw new HttpRequestException("The HTTP request failed "
					+ $"({(int)responseMessage.StatusCode} {responseMessage.StatusCode}).");
			}
			PlanningCenterErrorResponse errorResponse = JsonSerializer.Deserialize<PlanningCenterErrorResponse>(
				await responseMessage.Content.ReadAsStringAsync(), _serializerOptions) 
				?? throw new HttpRequestException("The HTTP request failed "
					+ $"({(int)responseMessage.StatusCode} {responseMessage.StatusCode}). "
					+ "A response body was present, but could not be deserialized.");
			throw new HttpRequestException(errorResponse.Errors
				.Select(e => $"{e.HttpStatusCode} {e.Title}: {e.Details} {(e.Metadata?.Description != null 
					? $"({e.Metadata.Description}) " : "")}{(e.ErrorCode != null ? $"[{e.ErrorCode}]" : "")}")
				.Aggregate((a, b) => $"{a}\n\n{b}"));
		}
		return responseMessage;
	}
}