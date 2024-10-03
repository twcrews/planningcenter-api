# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0-preview.4] - 2024-10-03

### Added

- Add extension method `Uri.SafelyAppendPath` to allow for deterministic URI path building.

## [1.0.0-preview.3] - 2024-09-25

### Added

- Add `.runsettings` file for unit tests.
- Add unit tests to ensure 100% code coverage.
- Add properties to `Error` class (formerly `PlanningCenterError`) to meet JSON:API specification.
- Add library information, installation instructions, and usage examples to `README.md`.

### Changed

- Modify this changelog file to more closely reflect [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) 
  recommendations.
- Rename `PlanningCenterError` to `Error` since this class now aligns with the JSON:API specification rather than
  Planning Center specifically.
- Change namespaces of several classes to match file directory structure.
- Use `Newtonsoft.Json` attributes rather than their `System.Text.Json` counterparts in `PlanningCenterMetadata` for 
  compatibility with the JSON:API Framework.
- Change several resource function return types to be nullable.
- Refactored several `PlanningCenterFetchableResource` subclasses for easier implementation.
- Renamed `QueryString.QueryStringParameter` class to `QueryString.Parameter`.

### Removed

- Removed `PlanningCenterApiClient` in favor of allowing dependents to use their own `HttpClient` configurations.

### Fixed

- Fix `PlanningCenterDocumentContext` to allow parameterless constructor and build JSON:API Framework conventions 
  correctly.

## [1.0.0-preview.2] - 2024-05-22

### Added

- Add function definition `PlanningCenterSingletonFetchableResource WithID(string)` to 
	`PlanningCenterPaginatedFetchableResource` for retrieving individual resources from a collection.
- Add `README.md` as NuGet `PackageReadmeFile`.

## [1.0.0-preview.1] - 2024-05-13

Initial release.

[1.0.0-preview.4]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.3...1.0.0-preview.4
[1.0.0-preview.3]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.2...1.0.0-preview.3
[1.0.0-preview.2]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.1...1.0.0-preview.2
[1.0.0-preview.1]: https://github.com/twcrews/planningcenter-api/releases/tag/1.0.0-preview.1