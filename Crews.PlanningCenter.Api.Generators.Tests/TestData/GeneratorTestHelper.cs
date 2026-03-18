using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;

namespace Crews.PlanningCenter.Api.Generators.Tests.TestData;

/// <summary>
/// Helper class for testing source generators.
/// </summary>
public static class GeneratorTestHelper
{
    /// <summary>
    /// Creates a compilation with the specified source code and additional files.
    /// </summary>
    public static (Compilation Compilation, ImmutableArray<AdditionalText> AdditionalFiles) CreateCompilation(
        string source,
        params (string FileName, string Content)[] additionalFiles)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        var compilation = CSharpCompilation.Create(
            "TestAssembly",
            [syntaxTree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var additionalTexts = additionalFiles
            .Select(file => (AdditionalText)new TestAdditionalText(file.FileName, file.Content))
            .ToImmutableArray();

        return (compilation, additionalTexts);
    }

    /// <summary>
    /// Runs a source generator and returns the generated sources.
    /// </summary>
    public static GeneratorDriverRunResult RunGenerator(
        string generatorTypeName,
        Compilation compilation,
        ImmutableArray<AdditionalText> additionalFiles)
    {
        // Load the generator assembly
        var generatorAssembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "Crews.PlanningCenter.Api.Generators");

        if (generatorAssembly == null)
            throw new InvalidOperationException("Could not find Generators assembly");

        // Get the generator type
        var generatorType = generatorAssembly.GetType($"Crews.PlanningCenter.Api.Generators.{generatorTypeName}");
        if (generatorType == null)
            throw new InvalidOperationException($"Could not find generator type {generatorTypeName}");

        // Create an instance of the generator
        var generator = (IIncrementalGenerator)Activator.CreateInstance(generatorType)!;

        var driver = CSharpGeneratorDriver.Create(generator)
            .AddAdditionalTexts(additionalFiles);

        driver = (CSharpGeneratorDriver)driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out _,
            out _);

        return driver.GetRunResult();
    }

    /// <summary>
    /// Gets a specific generated source by file name hint.
    /// </summary>
    public static string? GetGeneratedSource(GeneratorDriverRunResult result, string fileNameHint)
    {
        var generatedSource = result.GeneratedTrees
            .FirstOrDefault(tree => tree.FilePath.EndsWith(fileNameHint));

        return generatedSource?.GetText().ToString();
    }

    /// <summary>
    /// Asserts that the generated source contains the expected text.
    /// </summary>
    public static void AssertContains(string? source, params string[] expectedTexts)
    {
        Assert.NotNull(source);

        foreach (var expectedText in expectedTexts)
        {
            Assert.Contains(expectedText, source);
        }
    }

    /// <summary>
    /// Asserts that the generated source does not contain the unexpected text.
    /// </summary>
    public static void AssertDoesNotContain(string? source, params string[] unexpectedTexts)
    {
        Assert.NotNull(source);

        foreach (var unexpectedText in unexpectedTexts)
        {
            Assert.DoesNotContain(unexpectedText, source);
        }
    }
}

/// <summary>
/// Test implementation of AdditionalText for source generator testing.
/// </summary>
internal class TestAdditionalText : AdditionalText
{
    private readonly string _text;

    public TestAdditionalText(string path, string text)
    {
        Path = path;
        _text = text;
    }

    public override string Path { get; }

    public override SourceText GetText(CancellationToken cancellationToken = default)
    {
        return SourceText.From(_text);
    }
}
