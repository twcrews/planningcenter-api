using Crews.PlanningCenter.Api.DocParser;
using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("overrides.json", optional: true, reloadOnChange: false);
    })
    .ConfigureServices((context, services) =>
    {
        AppSettings settings = new();
        context.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

        services.Configure<AppSettings.PlanningCenterClientOptions>(context.Configuration
            .GetSection($"{nameof(AppSettings)}:{nameof(AppSettings.PlanningCenterClient)}"));
        services.Configure<AppSettings.DocumentationBuilderOptions>(context.Configuration
            .GetSection($"{nameof(AppSettings)}:{nameof(AppSettings.DocumentationBuilder)}"));
        services.Configure<DocumentationOverrides>(context.Configuration);

        services.AddHttpClient<IPlanningCenterClient, PlanningCenterClient>();

        services.AddTransient<IDocumentationBuilder, DocumentationBuilder>();
        services.AddTransient<Application>();
    })
    .Build();

Application app = host.Services.GetRequiredService<Application>();
await app.RunAsync();
