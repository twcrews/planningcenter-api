using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Models.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Generators;

static class IncrementalGeneratorContextExtensions
{
    public static IncrementalValuesProvider<(ProductDefinition Product, Models.Version? Version)> GetVersionsProvider(
        this IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<AdditionalText> jsonFiles = context.AdditionalTextsProvider
            .Where(static file => file.Path.EndsWith(".json"));

        return jsonFiles.Select(static (file, cancellationToken) =>
        {
            string productName = Path.GetFileName(Path.GetDirectoryName(file.Path));
            ProductDefinition productDefinition = ProductDefinition.All
                .First(p => productName.Equals(p.ToString().ToPascalCase()));
            string version = Path.GetFileNameWithoutExtension(file.Path);
            SourceText? text = file.GetText(cancellationToken);

            if (text is null) return (productDefinition, null);

            return (productDefinition, JsonSerializer.Deserialize<Models.Version>(text.ToString()));
        });
    }
}
