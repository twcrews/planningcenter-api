using System.Text.Json;
using Cos.PlanningCenter.Api.Models;

namespace Cos.PlanningCenter.Api;

public class PlanningCenterApiService(HttpClient httpClient)
{
	private readonly HttpClient _httpClient = httpClient;

	public Task<PlanningCenterRootCollectionObject<T>> GetAllAsync<T>(string path) where T : class 
		=> SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
		{
			RequestUri = new(path, UriKind.RelativeOrAbsolute),
			Method = HttpMethod.Get
		});

	public Task<PlanningCenterRootSingletonObject<T>> GetAsync<T>(string path) where T : class 
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = new(path, UriKind.RelativeOrAbsolute),
			Method = HttpMethod.Get
		});

	public Task<PlanningCenterRootCollectionObject<T>> PostAllAsync<T>(string path, IEnumerable<T> content) where T : class 
		=> SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
		{
			RequestUri = new(path, UriKind.RelativeOrAbsolute),
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(new PlanningCenterRootCollectionObject<T>
			{
				Data = content.Select(d => new PlanningCenterDataObject<T>
				{
					Attributes = d
				})
			}))
		});

	public Task<PlanningCenterRootSingletonObject<T>> PostAsync<T>(string path, T content) where T : class 
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = new(path, UriKind.RelativeOrAbsolute),
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(new PlanningCenterRootSingletonObject<T>
			{
				Data = new PlanningCenterDataObject<T>
				{
					Attributes = content
				}
			}))
		});

	public Task<PlanningCenterRootSingletonObject<T>> PatchAsync<T>(string path, T content) where T : class 
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = new(path, UriKind.RelativeOrAbsolute),
			Method = HttpMethod.Patch,
			Content = new StringContent(JsonSerializer.Serialize(new PlanningCenterRootSingletonObject<T>
			{
				Data = new PlanningCenterDataObject<T>
				{
					Attributes = content
				}
			}))
		});

	public Task DeleteAsync(string path) => _httpClient.SendAsync(new()
	{
		RequestUri = new(path, UriKind.RelativeOrAbsolute),
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
				.Select(e => $"{e.Status} {e.Title}: {e.Details} [{e.Code}]")
				.Aggregate((a, b) => $"{a}\n\n{b}"));
		}
		return JsonSerializer.Deserialize<U>(content)
			?? throw new NullReferenceException("The deserialized response object was null.");
	}
}
