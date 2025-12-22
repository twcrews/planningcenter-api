# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a multi-project .NET solution. The primary project in the solution (`Crews.PlanningCenter.Api`) is a client library for the Planning Center API, distributed as a NuGet Package. It supports all versions of each Planning Center product.

## Project Structure

The solution contains six projects:

1. **Crews.PlanningCenter.Api** (.NET 8.0): Main library project
2. **Crews.PlanningCenter.Api.DocParser** (.NET 10.0): Console application for downloading and parsing API documentation
3. **Crews.PlanningCenter.Api.Generators** (.NET Standard 2.0): Roslyn incremental source generator for code generation
4. **Crews.PlanningCenter.Api.Tests** (.NET 8.0): Unit tests for the main library
5. **Crews.PlanningCenter.Api.DocParser.Tests** (.NET 10.0): Unit tests for the doc parser
6. **Crews.PlanningCenter.Api.Generators.Tests** (.NET Standard 2.0): Unit tests for the source generators

**Current Development State:** The main API project has been cleared of generated code to prepare for the new source generator implementation. After the generators are fully implemented, client and resource classes will be automatically generated at compile time.

## Build and Test Commands

### Building
```bash
dotnet build
```

### Running Tests
```bash
# Run all tests
dotnet test

# Run tests for a specific project
dotnet test Crews.PlanningCenter.Api.Tests
dotnet test Crews.PlanningCenter.Api.DocParser.Tests
dotnet test Crews.PlanningCenter.Api.Generators.Tests

# Run tests with code coverage (uses .runsettings configuration)
dotnet test --settings Crews.PlanningCenter.Api.Tests/.runsettings --collect:"XPlat Code Coverage"

# Run a specific test
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"
```

### Cleaning
```bash
dotnet clean
```

### Running DocParser
```bash
# Run the documentation parser to update API definitions
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

The DocParser downloads the latest API documentation from Planning Center and saves it to `Crews.PlanningCenter.Api/Definitions/`. Run this manually when you need to synchronize with Planning Center API updates.

## Architecture

### Code Generation

The project uses a two-phase code generation approach:

#### Phase 1: Documentation Parser (`Crews.PlanningCenter.Api.DocParser`)
A console application that downloads and transpiles Planning Center API documentation into JSON definition files. This tool should be run manually when synchronizing with the latest Planning Center API updates.

**Running the DocParser:**
```bash
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

The DocParser:
- Fetches API metadata from `https://api.planningcenteronline.com/` for all supported products
- Transpiles the documentation into a structured JSON format
- Outputs definition files to `Crews.PlanningCenter.Api/Definitions/{Product}/{Version}.json`
- Supports all Planning Center products: Calendar, Check-Ins, Giving, Groups, People, Publishing, Registrations, Services

**Configuration:** The DocParser can be configured via `appsettings.json`:
```json
{
  "AppSettings": {
    "OutputDirectory": "../../../../Crews.PlanningCenter.Api/Definitions",
    "PlanningCenterClient": {
      "BaseAddress": "https://api.planningcenteronline.com/"
    }
  }
}
```

See the [DocParser README](Crews.PlanningCenter.Api.DocParser/README.md) for detailed documentation.

#### Phase 2: Incremental Source Generation (`Crews.PlanningCenter.Api.Generators`)
Contains utilities and services for generating strongly-typed client code from the JSON definition files. The generator uses [incremental source generation](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#syntaxvalueprovider) to efficiently process the definition files at compile time.

**Definition Files Location:**
- Path: `Crews.PlanningCenter.Api/Definitions/`
- Structure: `{Product}/{Version}.json`
- Example: `Definitions/People/2025-07-17.json`

**Note:** The source generator is configured in the main library project via:
- ProjectReference to the Generators project with `OutputItemType="Analyzer"`
- AdditionalFiles include for `Definitions/**/*.json`
- EmitCompilerGeneratedFiles is enabled to output generated code to `obj/Generated`

Client and resource classes will be auto-generated at compile time and include a header comment indicating they are generated code.

### Tests

#### DocParser Tests (`Crews.PlanningCenter.Api.DocParser.Tests`)
Unit tests for the documentation parser console application. Tests the parsing and transpilation logic for Planning Center API documentation.

#### Generator Tests (`Crews.PlanningCenter.Api.Generators.Tests`)
Unit tests for the incremental source generator. Tests the code generation logic that creates client and resource classes from JSON definition files.

## Important Patterns

### Adding New API Products
When Planning Center adds a new product API:
1. Add the new product to `ProductDefinition.All` in [ProductDefinition.cs](Crews.PlanningCenter.Api.DocParser/Models/ProductDefinition.cs)
2. Run the DocParser to download the new product's API definitions:
   ```bash
   cd Crews.PlanningCenter.Api.DocParser
   dotnet run
   ```
3. Rebuild the solution to trigger incremental source generation of the new client code

### Updating API Versions
When Planning Center releases new API versions:
1. Run the DocParser to download the updated API definitions:
   ```bash
   cd Crews.PlanningCenter.Api.DocParser
   dotnet run
   ```
2. New definition files will be added to `Crews.PlanningCenter.Api/Definitions/{Product}/`
3. Update the `LatestVersion` property in the affected client classes to point to the new version
4. Rebuild the solution to trigger incremental source generation

API versions use underscored date format (e.g., `V2025_07_17`).