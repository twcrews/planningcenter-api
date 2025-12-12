using Crews.PlanningCenter.Api.DocParser;
using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        AppSettings settings = new();
        context.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

        services.AddHttpClient<IPlanningCenterClient, PlanningCenterClient>(client =>
        {
            string? baseAddress = settings.PlanningCenterClient?.BaseAddress;
            if (!string.IsNullOrWhiteSpace(baseAddress)) client.BaseAddress = new Uri(baseAddress);
        });

        services.AddTransient<IDocumentationBuilder, DocumentationBuilder>();
        services.AddTransient<Application>();
    })
    .Build();

Application app = host.Services.GetRequiredService<Application>();
await app.RunAsync();
