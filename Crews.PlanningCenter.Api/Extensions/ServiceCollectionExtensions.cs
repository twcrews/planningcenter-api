using Crews.PlanningCenter.Api.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.Extensions;

/// <summary>
/// A collection of extension methods for dependency injection via service collections.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string HttpClientName = "PlanningCenterApi";

    /// <summary>
    /// Registers Planning Center API clients with the given service collection for dependency injection.
    /// Configures an <see cref="HttpClient"/> that forwards the OIDC bearer token from the current
    /// HTTP context — intended for use alongside <c>AddPlanningCenterAuthentication()</c>.
    /// </summary>
    /// <param name="services">The service collection to register against.</param>
    /// <returns>The same service collection instance.</returns>
    public static IServiceCollection AddPlanningCenterApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<PlanningCenterTokenHandler>();

        services.AddHttpClient(HttpClientName, client =>
        {
            client.BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl + "/");
        })
        .AddHttpMessageHandler<PlanningCenterTokenHandler>();

        return services.AddClientsFromFactory(HttpClientName);
    }

    /// <summary>
    /// Registers Planning Center API clients with the given service collection for dependency injection,
    /// using an <see cref="HttpClient"/> resolved from <see cref="IHttpClientFactory"/>
    /// by the given name.
    /// </summary>
    /// <param name="services">The service collection to register against.</param>
    /// <param name="httpClientName">
    /// The name of the <see cref="HttpClient"/> to resolve from
    /// <see cref="IHttpClientFactory"/>.
    /// </param>
    /// <returns>The same service collection instance.</returns>
    public static IServiceCollection AddPlanningCenterApi(
        this IServiceCollection services,
        string httpClientName)
        => services.AddClientsFromFactory(httpClientName);

    private static IServiceCollection AddClientsFromFactory(
        this IServiceCollection services,
        string httpClientName)
    {
        return services
            .AddScoped(sp => new CalendarClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new CheckInsClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new GivingClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new GroupsClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new PeopleClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new PublishingClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new RegistrationsClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)))
            .AddScoped(sp => new ServicesClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)));
    }
}
