using System.CodeDom.Compiler;
using Crews.PlanningCenter.Api.Generators.Extensions;
using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Models.Extensions;
using Microsoft.CodeAnalysis;

namespace Crews.PlanningCenter.Api.Generators;

[Generator]
class PlanningCenterResourceClientsGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<(ProductDefinition Product, Version? version)> products = context
            .GetVersionsProvider();

        
    }

    private void GenerateChildNavigation(IndentedTextWriter writer, ResourceChild child)
    {
        string memberName = child.Name.ToPascalCase();

        string summary = child.Description is null
            ? $"Related {memberName}."
            : child.Description;
        if (child.IsDeprecated) summary = $"DEPRECATED: {summary}";
        summary = summary.ToXmlSummary();

        string type = child.Type.ToPascalCase() + "Client";
        if (child.IsCollection) type = "Paginated" + type;

        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// {summary}");
        writer.WriteLine("/// </summary>");
        writer.WriteLine($"public {type} {child.Name.ToPascalCase()} => new(httpClient, new(uri, \"{child.Name}/\"));");
        writer.WriteLine();
    }

    private void GenerateIncludable(IndentedTextWriter writer, ResourceIncludable includable, string resourceClrType)
    {
        string summary = includable.Description ?? $"Include related {includable.Value.ToPascalCase()}.";
        if (includable.CanAssignOnCreate) summary += "\nCan also be used when creating this resource type.";
        if (includable.CanAssignOnUpdate) summary += "\nCan also be used when updating this resource type.";
        summary = summary.ToXmlSummary();

        string methodName = "Include" + includable.Value.ToPascalCase();

        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// {summary}");
        writer.WriteLine("/// </summary>");
        writer.WriteLine($"public {resourceClrType} {methodName}() => SetQueryParameter({includable.Parameter}, {includable.Value});");
    }

    private void GenerateOrderable(IndentedTextWriter writer, ResourceOrderable orderable, string resourceClrType)
    {
        string summary = orderable.Description ?? $"Order by {orderable.Value.ToPascalCase()}.";
        summary = summary.ToXmlSummary();

        string methodName = "OrderBy" + orderable.Value.ToPascalCase();

        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// {summary}");
        writer.WriteLine("/// </summary>");
        writer.WriteLine($"public {resourceClrType} {methodName}() => SetQueryParameter({orderable.Parameter}, {orderable.Value});");
    }

    private void GenerateQueryable(IndentedTextWriter writer, ResourceQueryable queryable, string resourceClrType)
    {
        string methodName = "Where" + queryable.Name.ToPascalCase();

        string summary = queryable.Description ?? $"Query by {queryable.Name.ToPascalCase()}.";
        if (queryable.Example is not null)
        {
            string exampleValue = queryable.Example.Split('=')[1];
            string example = $"Example: {methodName}(\"{exampleValue}\")";
            summary += $"\n{example}";
        }
        summary = summary.ToXmlSummary();

        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// {summary}");
        writer.WriteLine("/// </summary>");
        writer.WriteLine($"public {resourceClrType} {methodName}(string value) => SetQueryParameter({queryable.Parameter}, value);");
    }

    private void GenerateCollectionFetchMethod(IndentedTextWriter writer, Resource resource, string resourceClrType)
    {
        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// Fetches a collection of <see cref=\"{resourceClrType}\"/> resources asynchronously.");
        writer.WriteLine("/// </summary>");
        writer.WriteLine("/// <param name=\"cancellationToken\">A token to monitor for cancellation requests.</param>");
        writer.WriteLine($"/// <returns>A task representing the asynchronous operation, containing a collection of <see cref=\"{resourceClrType}\"/> resources.</returns>");
        writer.WriteLine("/// <exception cref=\"JsonApiException\">Thrown when the HTTP response indicates a failure status code.</exception>");
        writer.WriteLine($"public new Task<IEnumerable<{resourceClrType}>> GetAsync(CancellationToken cancellationToken = default) => base.GetAsync<IEnumerable<{resourceClrType}>>(cancellationToken);");
        writer.WriteLine();
    }

    private void GenerateEndpointMethods(IndentedTextWriter writer, Resource resource, string resourceClrType)
    {
        writer.WriteLine("/// <summary>");
        writer.WriteLine($"/// Fetches the <see cref=\"{resourceClrType}\"/> resource asynchronously.");
        writer.WriteLine("/// </summary>");
        writer.WriteLine("/// <param name=\"cancellationToken\">A token to monitor for cancellation requests.</param>");
        writer.WriteLine($"/// <returns>A task representing the asynchronous operation, containing the <see cref=\"{resourceClrType}\"/> resource.</returns>");
        writer.WriteLine("/// <exception cref=\"JsonApiException\">Thrown when the HTTP response indicates a failure status code.</exception>");
        writer.WriteLine($"public new Task<{resourceClrType}?> GetAsync(CancellationToken cancellationToken = default) => base.GetAsync<{resourceClrType}>(cancellationToken);");
        writer.WriteLine();
       
        if (resource.Postable)
        {
            writer.WriteLine("/// <summary>");
            writer.WriteLine($"/// Creates a new <see cref=\"{resourceClrType}\"/> resource asynchronously.");
            writer.WriteLine("/// </summary>");
            writer.WriteLine("/// <param name=\"request\">The resource data to be sent in the POST request.</param>");
            writer.WriteLine("/// <param name=\"cancellationToken\">A token to monitor for cancellation requests.</param>");
            writer.WriteLine($"/// <returns>A task representing the asynchronous operation, containing the created <see cref=\"{resourceClrType}\"/> resource.</returns>");
            writer.WriteLine("/// <exception cref=\"JsonApiException\">Thrown when the HTTP response indicates a failure status code.</exception>");
            writer.WriteLine($"public Task<{resourceClrType}?> PostAsync({resourceClrType} request, CancellationToken cancellationToken = default) => base.PostAsync<{resourceClrType}>(request, cancellationToken);");
            writer.WriteLine();
        }

        if (resource.Patchable)
        {
            writer.WriteLine("/// <summary>");
            writer.WriteLine($"/// Updates an existing <see cref=\"{resourceClrType}\"/> resource asynchronously.");
            writer.WriteLine("/// </summary>");
            writer.WriteLine("/// <param name=\"request\">The resource data to be sent in the patch request.</param>");
            writer.WriteLine("/// <param name=\"cancellationToken\">A token to monitor for cancellation requests.</param>");
            writer.WriteLine($"/// <returns>A task representing the asynchronous operation, containing the updated <see cref=\"{resourceClrType}\"/> resource.</returns>");
            writer.WriteLine("/// <exception cref=\"JsonApiException\">Thrown when the HTTP response indicates a failure status code.</exception>");
            writer.WriteLine($"public Task<{resourceClrType}?> PatchAsync({resourceClrType} request, CancellationToken cancellationToken = default) => base.PatchAsync<{resourceClrType}>(request, cancellationToken);");
            writer.WriteLine();
        }

        if (resource.Deletable)
        {
            writer.WriteLine("/// <summary>");
            writer.WriteLine($"/// Deletes the <see cref=\"{resourceClrType}\"/> resource asynchronously.");
            writer.WriteLine("/// </summary>");
            writer.WriteLine("/// <param name=\"cancellationToken\">A token to monitor for cancellation requests.</param>");
            writer.WriteLine("/// <returns>A task representing the asynchronous delete operation.</returns>");
            writer.WriteLine("/// <exception cref=\"JsonApiException\">Thrown when the HTTP response indicates a failure status code.</exception>");
            writer.WriteLine("public Task DeleteAsync(CancellationToken cancellationToken = default) => base.DeleteAsync(cancellationToken);");
            writer.WriteLine();
        }
    }
}
