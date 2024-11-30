# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0-preview.13] - 2024-11-30

### Changed

- Upgrade dependencies. No functional changes.

## [1.0.0-preview.12] - 2024-11-29

### Changed

- Switch to NuGet packages for some internal extension methods. No functional changes.

## [1.0.0-preview.11] - 2024-11-11

### Added

- Add support for specifying ascending or descending order in `IOrderable` resources.

## [1.0.0-preview.10] - 2024-11-11

### Changed

- Refactor methods for `IFilterable`, `IIncludable`, `IOrderable`, and `IQueryable` resources so that using a `TEnum`
  value without a `JsonApiNameAttribute` will parse that value's name as a snake-case string rather than throwing
  an exception.
- Refactor `JsonApiNameAttribute` to disallow using multiple instances per element.

## [1.0.0-preview.9] - 2024-11-09

### Changed

- Refactor API naming additions introduced in Preview 8 to use the new `INamedApiResource` interface, effectively making 
  the change opt-in and non-breaking.
- Split `GetAssociated<TResource>()` method introduced in Preview 8 into two methods: `GetAssociated` and 
  `GetNamedAssociated`.

## [1.0.0-preview.8] - 2024-11-09

### Added

- Add `ApiName` abstract property to fetchable resources, forcing concrete resource types to specify their name as
  defined in their API.
- Add `GetAssociated<TResource>()` protected method to fetchable resources, allowing easy instantiation of associated 
  resource types when creating chainable methods.

### Fixed

- Fix inconsistent verb tenses in this changelog.

## [1.0.0-preview.7] - 2024-10-15

### Changed

- Enable use of non-public constructors in resource types.

### Fixed

- Fix broken link in changelog for version `1.0.0-preview.6`.

## [1.0.0-preview.6] - 2024-10-14

### Added

- Add `PlanningCenterPersonalAccessToken` POCO class, which is implicitly convertible to `AuthenticationHeaderValue`.

## [1.0.0-preview.5] - 2024-10-03

### Fixed

- Change `UriExtensions` to be `public` instead of `internal`.

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
- Refactor several `PlanningCenterFetchableResource` subclasses for easier implementation.
- Renamed `QueryString.QueryStringParameter` class to `QueryString.Parameter`.

### Removed

- Remove `PlanningCenterApiClient` in favor of allowing dependents to use their own `HttpClient` configurations.

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

[1.0.0-preview.13]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.12...1.0.0-preview.13
[1.0.0-preview.12]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.11...1.0.0-preview.12
[1.0.0-preview.11]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.10...1.0.0-preview.11
[1.0.0-preview.10]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.9...1.0.0-preview.10
[1.0.0-preview.9]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.8...1.0.0-preview.9
[1.0.0-preview.8]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.7...1.0.0-preview.8
[1.0.0-preview.7]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.6...1.0.0-preview.7
[1.0.0-preview.6]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.5...1.0.0-preview.6
[1.0.0-preview.5]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.4...1.0.0-preview.5
[1.0.0-preview.4]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.3...1.0.0-preview.4
[1.0.0-preview.3]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.2...1.0.0-preview.3
[1.0.0-preview.2]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.1...1.0.0-preview.2
[1.0.0-preview.1]: https://github.com/twcrews/planningcenter-api/releases/tag/1.0.0-preview.1