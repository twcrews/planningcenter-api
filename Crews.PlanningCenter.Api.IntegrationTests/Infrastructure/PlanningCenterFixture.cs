using System.Net.Http.Headers;
using Crews.PlanningCenter.Api.Authentication;
using Microsoft.Extensions.Configuration;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

public class PlanningCenterFixture : IAsyncLifetime
{
	public HttpClient HttpClient { get; private set; } = null!;

	public Task InitializeAsync()
	{
		var configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true)
			.AddEnvironmentVariables()
			.AddUserSecrets<PlanningCenterFixture>(optional: true)
			.Build();

		var appId = configuration["PlanningCenter:AppId"]
			?? throw new InvalidOperationException(
				"PlanningCenter:AppId not configured. Set via user-secrets or environment variables.");
		var secret = configuration["PlanningCenter:Secret"]
			?? throw new InvalidOperationException(
				"PlanningCenter:Secret not configured. Set via user-secrets or environment variables.");

		PlanningCenterPersonalAccessToken token = new()
		{
			AppId = appId,
			Secret = secret
		};

		HttpClient = new HttpClient
		{
			BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
		};
		HttpClient.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));
		HttpClient.DefaultRequestHeaders.Authorization = token;

		return Task.CompletedTask;
	}

	public Task DisposeAsync()
	{
		HttpClient?.Dispose();
		return Task.CompletedTask;
	}
}
