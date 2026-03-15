# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [4.0.0] - 2026-03-14

### Added

- **Breaking change:** Replace custom OAuth 2.0 implementation with standard ASP.NET Core OpenID Connect (OIDC).
  - Add `AddPlanningCenterAuthentication()` extension method on `AuthenticationBuilder`, replacing `AddPlanningCenterOAuth()`.
  - Reads configuration from `appsettings.json` under the `"PlanningCenter"` section (`Authority`, `ClientId`, `ClientSecret`, `Scopes`).
  - `ClientId` and `ClientSecret` are required; an exception is thrown at startup if they are missing.
  - Scopes default to `["openid", "people"]`.
  - Four overloads are provided: parameterless, sign-in scheme only, configure-options only, and both.
- Add new constants to `PlanningCenterAuthenticationDefaults`:
  - `AuthorizationEndpoint`, `TokenEndpoint`, `UserInfoEndpoint`, `DiscoveryEndpoint`
  - `RecommendedPrompt` (`"select_account"`), `LoginPrompt` (`"login"`)
  - `AccessTokenLifetimeSeconds` (7200), `IdTokenLifetimeSeconds` (3600), `RefreshTokenLifetimeDays` (90)
- Add strongly-typed, auto-generated resource and client classes for all supported Planning Center products and API versions.
  - Generated at compile time from JSON definition files via incremental source generators.
  - Classes are organized by product and version namespace (e.g., `Crews.PlanningCenter.Api.People.V2025_11_10`).
- Add JSON converters: `BoolFromStringConverter`, `StringFromNumberConverter`, `TimeSpanFromSecondsConverter`.
- Add `Crews.Web.JsonApiClient` as a dependency (replaces `JsonApiFramework.Client`).

### Changed

- **Breaking change:** Migrate from custom OAuth handler to standard OpenID Connect.
  - `PlanningCenterOAuthDefaults`, `PlanningCenterOAuthHandler`, `PlanningCenterOAuthOptions`, `PlanningCenterOAuthScope`, and `PlanningCenterClaimsTransformation` have been removed.
  - Replace calls to `AddPlanningCenterOAuth()` with `AddPlanningCenterAuthentication()`.
  - Update `appsettings.json`: configuration is now under `"PlanningCenter"` rather than `"Authentication:PlanningCenter:ClaimsTransformation"`.
- **Breaking change:** Remove `AddPlanningCenterApi()` DI registration, `IPlanningCenterApiService`, `PlanningCenterApiService`, and `PlanningCenterApiOptions`.
  - Consumers must now configure `HttpClient` directly and instantiate generated client classes as needed. See `README.md` for updated usage examples.
- **Breaking change:** Remove hand-written root client classes (`CalendarClient`, `CheckInsClient`, `GivingClient`, `GroupsClient`, `PeopleClient`, `PublishingClient`, `ServicesClient`).
  - These are superseded by the auto-generated clients in each product's versioned namespace.
- **Breaking change:** Remove `JsonApiError`, `JsonApiMetadata`, and related response model classes.
  - Responses are now represented by `ResourceResponse<T>` with `Data`, `ResponseBody`, and `ResponseMessage` properties.
- **Breaking change:** Remove `ConventionsBuilderExtensions.AddSnakeCaseNamingConvention()` and the `SnakeCaseNamingConvention` class.
- **Breaking change:** Remove `Crews.PlanningCenter.Models` external package dependency; models are now included directly.
- Update `PlanningCenterAuthenticationDefaults.ConfigurationSection` value to `"Authentication:PlanningCenter"` (was `"Authentication:PlanningCenter:ClaimsTransformation"`).
- Change license from MIT to MIT (no functional change — license file updated to correct prior omission).
- Generated resource types are now `record` types.

## [3.0.0] - 2025-10-09

### Added

- **Breaking change:** Add `PlanningCenterAuthenticationHandler` as a delegating handler for API client requests.
  - The injected `PlanningCenterClient` will now automatically include authorization headers with each request.
  - If a personal access token is configured, it is used, and other auth configuration (OAuth/OIDC) is ignored.
  - If a personal access token is not configured, the current HTTP context's `access_token` is retrieved.
  - If `access_token` cannot be retrieved, the request is attempted without setting an authorization header.
- Add `Resources` scope to predefined OAuth scope names. 
  - This is an undocumented Planning Center API product.
  - The library does not include models or other types to support this scope.
- Add `Microsoft.AspNetCore.Authentication.OpenIdConnect` package dependency.
- Add `ClaimsPrincipal` extension methods for easy access to Planning Center claims values.
  - Example usage: `User.GetOrganizationId()` is the equivalent of `User.FindFirst("organization_id")?.Value`.

### Changed

- **Breaking change:** Rename `PlanningCenterOAuthDefaults` to `PlanningCenterAuthenticationDefaults`.
  - Properties have been updated to suit OIDC flows with auto-discovery.
- **

## [2.0.0] - 2025-09-24

### Added

- **Breaking change:** Add 
  [User Agent](https://developer.planning.center/docs/#/overview/authentication%23specifying-user-agent) header 
  configuration to `PlanningCenterApiOptions`. A default value is provided.
- Add more claims to the claims principal created by the OAuth authentication handler. 
	- All attributes of the `Person` resource are now included as claims. 
	- Non-standard claims use the `urn:planningcenter:*` prefix.
- Add `GetRelated<T>()` method to fetchable singleton resources, allowing custom resource type fetching for forward 
  compatibility.
- Add more detailed configuration instructions and examples to `README.md`.
- Add configuration support for claims transformation in the `AddPlanningCenterOAuth` extension methods via the
  `Authentication:PlanningCenter:ClaimsTransformation` configuration section.

### Fixed

- **Breaking change:** Fix an issue where the `AddPlanningCenterApi()` extension method ignored the provided 
  `HttpClientName` option in `PlanningCenterApiOptions`.
- **Breaking change:** Fix a casing typo in the default configuration section name of `PlanningCenterApiOptions`.
- **Breaking change:** Fix an issue where `People` related resource properties were singleton types due to a 
  pluralization ambiguity.
- Fix `README.md` falsely claiming that the `PeopleClient` class contains a `Me` property.
- Fix an issue where the `AddPlanningCenterOAuth` extension methods might ignore configuration options.
- Fix an issue where the `AddPlanningCenterOAuth` extension methods might not correctly register the OAuth claims 
  transformation class.

### Changed

- **Breaking change:** Rename some claim types to better reflect their contents.
- **Breaking change:** Rename `PlanningCenterPersonalAccessToken.AppID` property to `AppId` for consistency with 
  framework naming conventions.
- **Breaking change:** Change the behavior of all client classes to use the default Planning Center API base URL instead
  of throwing an exception if none is provided.
- **Breaking change:** Move `AddPlanningCenterOAuth` extension methods to the `DependencyInjection` namespace for 
  simplicity.
- **Breaking change:** Change `AddPlanningCenterOAuth` extension methods to use the 
  `Authentication:PlanningCenter:ClientId` and `Authentication:PlanningCenter:ClientSecret` configuration sections by 
  default.
- **Breaking change:** Upgraded some dependencies by major versions.
- Make personal access token optional in `PlanningCenterApiOptions` to account for OAuth.

## [1.2.0] - 2025-08-27

### Added

- Add OAuth support.

### Changed

- Regenerate all code based on current documentation.

## [1.1.0] - 2025-01-23

### Added

- Added Dependency Injection.

### Changed

- Updated `README.md` to reflect addition of DI.

### Fixed

- Fixed an issue where some resources were not properly registered in document contexts due to naming ambiguity.

## [1.0.2] - 2025-01-15

### Fixed

- Fixed an issue where resources could not be constructed, causing a crash on fetches.

## [1.0.1] - 2025-01-13

### Changed

- Commit generated files directly due to GitHub Actions incompatibilities.

## [1.0.0] - 2025-01-13

First official stable release!

### Added

- Add generated resource models and client services.

### Changed

- Refactor several infrastructure items.
- Update `README.md`.

## [1.0.0-preview.14] - 2024-12-06

### Changed

- Add `Crews.PlanningCenter.Models` as a dependency, transferring ownership of `JsonApiNameAttribute`.

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

[4.0.0]: https://github.com/twcrews/planningcenter-api/compare/2.0.0...4.0.0
[3.0.0]: https://github.com/twcrews/planningcenter-api/compare/2.0.0...3.0.0
[2.0.0]: https://github.com/twcrews/planningcenter-api/compare/1.2.0...2.0.0
[1.2.0]: https://github.com/twcrews/planningcenter-api/compare/1.1.0...1.2.0
[1.1.0]: https://github.com/twcrews/planningcenter-api/compare/1.0.2...1.1.0
[1.0.2]: https://github.com/twcrews/planningcenter-api/compare/1.0.1...1.0.2
[1.0.1]: https://github.com/twcrews/planningcenter-api/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.14...1.0.0
[1.0.0-preview.14]: https://github.com/twcrews/planningcenter-api/compare/1.0.0-preview.13...1.0.0-preview.14
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