using System.Text.Json;
using System.Text.Json.Serialization;
using Cos.PlanningCenter.Api.Models;

namespace Cos.PlanningCenter.Api;

/// <summary>
/// A low-level service used for interacting with the Planning Center API.
/// </summary>
public class PlanningCenterApiService(HttpClient httpClient)
{
	private readonly HttpClient _httpClient = httpClient;
	private readonly JsonSerializerOptions _serializerOptions = new()
	{
		NumberHandling = JsonNumberHandling.AllowReadingFromString
	};

    /// <summary>
    /// Fetches a collection of Planning Center data objects of the specific type.
    /// </summary>
    /// <param name="path">The URI of the API request.</param>
    /// <typeparam name="T">The type of data object.</typeparam>
    public Task<PlanningCenterRootCollectionObject<T>> GetAllAsync<T>(Uri path) where T : class 
		=> SendRequestWithResponseAsync<PlanningCenterRootCollectionObject<T>>(new()
		{
			RequestUri = path,
			Method = HttpMethod.Get
		});

	/// <summary>
	/// Fetches one Planning Center data object of the specific type.
	/// </summary>
	/// <param name="path">The URI of the API request.</param>
	/// <typeparam name="T">The type of data object.</typeparam>
	public Task<PlanningCenterRootSingletonObject<T>> GetAsync<T>(Uri path) where T : class 
		=> SendRequestWithResponseAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = path,
			Method = HttpMethod.Get
		});

	/// <summary>
	/// Posts a collection of Planning Center data objects of the specified type.
	/// </summary>
	/// <param name="path">The URI of the API request.</param>
	/// <param name="content">The collection of data objects to post.</param>
	/// <typeparam name="T">The type of data object.</typeparam>
	public Task<PlanningCenterRootCollectionObject<T>> PostAllAsync<T>(Uri path, IEnumerable<T> content) where T : class 
		=> SendRequestWithResponseAsync<PlanningCenterRootCollectionObject<T>>(new()
		{
			RequestUri = path,
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(new PlanningCenterRootCollectionObject<T>
			{
				Data = content.Select(d => new PlanningCenterDataObject<T>
				{
					Attributes = d
				})
			}))
		});

	/// <summary>
	/// Posts one Planning Center data object of the specified type.
	/// </summary>
	/// <param name="path">The URI of the API request.</param>
	/// <param name="content">The data object to post.</param>
	/// <typeparam name="T">The type of data object.</typeparam>
	public Task<PlanningCenterRootSingletonObject<T>> PostAsync<T>(Uri path, T content) where T : class 
		=> SendRequestWithResponseAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = path,
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(new PlanningCenterRootSingletonObject<T>
			{
				Data = new PlanningCenterDataObject<T>
				{
					Attributes = content
				}
			}))
		});

	/// <summary>
	/// Modifies one Planning Center data object of the specified type.
	/// </summary>
	/// <param name="path">The URI of the API request.</param>
	/// <param name="content">The data object with its modifications.</param>
	/// <typeparam name="T">The type of data object.</typeparam>
	public Task<PlanningCenterRootSingletonObject<T>> PatchAsync<T>(Uri path, T content) where T : class 
		=> SendRequestWithResponseAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = path,
			Method = HttpMethod.Patch,
			Content = new StringContent(JsonSerializer.Serialize(new PlanningCenterRootSingletonObject<T>
			{
				Data = new PlanningCenterDataObject<T>
				{
					Attributes = content
				}
			}))
		});

	/// <summary>
	/// Deletes one Planning Center data object.
	/// </summary>
	/// <param name="path">The URI of the API request.</param>
	public Task DeleteAsync(Uri path) => SendRequestAsync(new()
	{
		RequestUri = path,
		Method = HttpMethod.Delete
	});

	private async Task<U> SendRequestWithResponseAsync<U>(HttpRequestMessage request)
	{
		HttpResponseMessage response = await SendRequestAsync(request);
		return JsonSerializer.Deserialize<U>(await response.Content.ReadAsStringAsync(), _serializerOptions)
			?? throw new NullReferenceException("The deserialized response object was null.");
	}

	private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
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
