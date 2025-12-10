# Planning Center API Documentation Parser

This project is responsible for downloading and transpiling the Planning Center API documentation into a format that can be used as input in the code generator project.

This console application is designed to be executed manually as needed to synchronize this repository with the latest version of the Planning Center API documentation.

The files produced by this application will be automatically ingested at compile time by the code generator project as part of the [incremental source generation](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#syntaxvalueprovider) process.