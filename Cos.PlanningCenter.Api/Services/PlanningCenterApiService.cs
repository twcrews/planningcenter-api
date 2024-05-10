using System.Text.Json;
using Cos.PlanningCenter.Api.Models;

namespace Cos.PlanningCenter.Api;

public class PlanningCenterApiService<T>(HttpClient httpClient) where T : class
{
	private readonly HttpClient _httpClient = httpClient;

	public Task<PlanningCenterRootCollectionObject<T>> GetAllAsync(string path) => SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
	{
		RequestUri = new(path),
		Method = HttpMethod.Get
	});

	public Task<PlanningCenterRootSingletonObject<T>> GetAsync(string path) => SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
	{
		RequestUri = new(path),
		Method = HttpMethod.Get
	});

	public Task<PlanningCenterRootCollectionObject<T>> PostAllAsync(string path, T content)
		=> SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
		{
			RequestUri = new(path),
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(content))
		});

	public Task<PlanningCenterRootSingletonObject<T>> PostAsync(string path, T content)
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = new(path),
			Method = HttpMethod.Post,
			Content = new StringContent(JsonSerializer.Serialize(content))
		});

	public Task<PlanningCenterRootCollectionObject<T>> PatchAllAsync(string path, T content)
		=> SendRequestAsync<PlanningCenterRootCollectionObject<T>>(new()
		{
			RequestUri = new(path),
			Method = HttpMethod.Patch,
			Content = new StringContent(JsonSerializer.Serialize(content))
		});

	public Task<PlanningCenterRootSingletonObject<T>> PatchAsync(string path, T content)
		=> SendRequestAsync<PlanningCenterRootSingletonObject<T>>(new()
		{
			RequestUri = new(path),
			Method = HttpMethod.Patch,
			Content = new StringContent(JsonSerializer.Serialize(content))
		});

	public Task DeleteAsync(string path) => _httpClient.SendAsync(new()
	{
		RequestUri = new(path),
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
