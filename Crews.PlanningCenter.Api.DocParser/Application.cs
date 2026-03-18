using Crews.PlanningCenter.Api.DocParser.Configuration;
using Crews.PlanningCenter.Api.DocParser.Services;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Models.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.DocParser;

[ExcludeFromCodeCoverage(Justification = "This class is not easily testable, and its injections are thoroughly tested.")]
class Application(ILogger<Application> logger, IConfiguration configuration, IDocumentationBuilder documentationBuilder)
{
    public async Task RunAsync()
    {
        logger.LogDebug("Successfully configured and started documentation parser application");
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        AppSettings settings = new();
        configuration.GetSection(nameof(AppSettings)).Bind(settings);

        JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        logger.LogInformation("Fetching API documentation from Planning Center...");
        IEnumerable<Product> products = await documentationBuilder.BuildAllProductsAsync();

        string outputDir = settings.OutputDirectory ?? "output";
        logger.LogInformation("Writing documentation to output directory: {OutputDirectory}", outputDir);

        foreach (Product product in products)
        {
            string productDir = Path.Combine(outputDir, product.Name.ToString().ToPascalCase());
            DirectoryInfo directory = Directory.CreateDirectory(Path.Combine(outputDir, product.Name.ToString().ToPascalCase()));
            foreach (Api.Models.Version version in product.Versions)
            {
                string path = Path.Combine(directory.FullName, $"{version.Id}.json");
                logger.LogDebug("Writing documentation for product {Product} version {Version} to {Path}...", product.Name, version.Id, path);

                using FileStream stream = File.Create(path);
                await JsonSerializer.SerializeAsync(stream, version, serializerOptions);
                await stream.DisposeAsync();
            }
        }

        stopwatch.Stop();
        logger.LogDebug("Documentation parsing completed in {Elapsed}", stopwatch.Elapsed);
    }
}
