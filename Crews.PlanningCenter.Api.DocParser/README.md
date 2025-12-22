# Planning Center API Documentation Parser

This project is responsible for downloading and transpiling the Planning Center API documentation into a format that can be used as input in the code generator project.

This console application is designed to be executed manually as needed to synchronize this repository with the latest version of the Planning Center API documentation.

The files produced by this application will be automatically ingested at compile time by the code generator project as part of the [incremental source generation](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#syntaxvalueprovider) process.

## Configuration

The following custom configuration sections are supported:

- `AppSettings:OutputDirectory`: the output directory for documentation JSON files. Defaults to `./output`.
- `AppSettings:PlanningCenterClient:BaseAddress`: the `HttpClient.BaseAddress` to use when fetching documentation.
- `AppSettings:DocumentationBuilder:ConcurrentConnections`: the maximum number of concurrent requests allowed to the Planning Center API. **Please be respectful of Planning Center's servers when modifying this value.**