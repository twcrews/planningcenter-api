using System.Text.Json;
using Cos.PlanningCenter.Api.Models;

namespace Cos.PlanningCenter.Api;

/// <summary>
/// A low-level service used for interacting with the Planning Center API.
/// </summary>
public class PlanningCenterApiService(HttpClient httpClient)
{
	private readonly HttpClient _httpClient = httpClient;

	/// <summary>
	/// Fetches a collection of Planning Center data objects of the specific type.
	/// </summary>
	/// <param name="path">The URI of the API request.</param>
	/// <typeparam name="T">The type of data object.</typeparam>
	public Task<PlanningCenterRootCollectionObject<T>> GetAllAsync<T>(Uri path) where T : class 
		=> SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
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
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
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
		=> SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
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
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
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
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
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
	public Task DeleteAsync(Uri path) => _httpClient.SendAsync(new()
	{
		RequestUri = path,
		Method = HttpMethod.Delete
	});

	private async Task<U> SendRequestAsync<U>(HttpRequestMessage request)
	{
		HttpResponseMessage responseMessage = await _httpClient.SendAsync(request);
		string content = await responseMessage.Content.ReadAsStringAsync();
		if (!responseMessage.IsSuccessStatusCode)
		{
			PlanningCenterErrorResponse errorResponse = JsonSerializer.Deserialize<PlanningCenterErrorResponse>(content)
				?? throw new HttpRequestException($"The HTTP request failed with code {responseMessage.StatusCode}.");
			throw new HttpRequestException(errorResponse.Errors
				.Select(e => $"{e.HttpStatusCode} {e.Title}: {e.Details} [{e.ErrorCode}]")
				.Aggregate((a, b) => $"{a}\n\n{b}"));
		}
		return JsonSerializer.Deserialize<U>(content)
			?? throw new NullReferenceException("The deserialized response object was null.");
	}
}
