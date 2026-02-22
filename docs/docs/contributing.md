# Contributing

Guidelines for contributing to the Planning Center API client library.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/your-username/planningcenter-api.git
   cd planningcenter-api
   ```
3. **Create a branch** for your changes:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Development Setup

### Prerequisites

- .NET 8.0 SDK or later
- .NET 10.0 SDK (for DocParser)
- A code editor (Visual Studio, VS Code, or Rider)
- Git

### Building the Solution

```bash
dotnet build
```

### Running Tests

Run all tests:
```bash
dotnet test
```

Run tests with coverage:
```bash
dotnet test --settings Crews.PlanningCenter.Api.Tests/.runsettings --collect:"XPlat Code Coverage"
```

Run specific test project:
```bash
dotnet test Crews.PlanningCenter.Api.Tests
dotnet test Crews.PlanningCenter.Api.DocParser.Tests
dotnet test Crews.PlanningCenter.Api.Generators.Tests
```

## Making Changes

### Code Style

- Follow C# coding conventions
- Use file-scoped namespaces
- Use record types where appropriate
- Add XML documentation comments for public APIs
- Keep lines under 120 characters when practical

### Testing

- Write unit tests for new features
- Ensure all tests pass before submitting
- Aim for high test coverage on new code
- Test edge cases and error conditions

### Documentation

- Update XML comments for code changes
- Add examples to documentation files
- Update README.md if needed
- Regenerate DocFX documentation if API changes

## Submitting Changes

### Commit Messages

Write clear, descriptive commit messages:

```
Add support for filtering query parameters

- Implement query parameter builder
- Add unit tests for filtering
- Update documentation with examples
```

### Pull Requests

1. **Push your branch** to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```

2. **Create a pull request** on GitHub

3. **Fill out the PR template** with:
   - Description of changes
   - Related issues (if any)
   - Testing performed
   - Breaking changes (if any)

4. **Wait for review** - maintainers will review your PR

## Areas for Contribution

### High Priority

- Additional test coverage
- Documentation improvements
- Bug fixes
- Performance optimizations

### Feature Ideas

- Enhanced query parameter support
- Caching strategies
- Retry policies and resilience patterns
- Additional authentication methods
- Webhook support

### Code Generation

- Improvements to DocParser logic
- Source generator enhancements
- Better error handling in generation
- Support for new Planning Center API features

## Reporting Issues

When reporting bugs or issues:

1. **Search existing issues** first
2. **Provide clear reproduction steps**
3. **Include error messages and stack traces**
4. **Specify .NET version and OS**
5. **Include sample code** if possible

## Code Review Process

All submissions require review. We use GitHub pull requests for this purpose.

Reviewers will check:
- Code quality and style
- Test coverage
- Documentation
- Breaking changes
- Performance impact

## License

By contributing, you agree that your contributions will be licensed under the same license as the project.

## Questions?

If you have questions about contributing:
- Open a discussion on GitHub
- Ask in an existing issue
- Reach out to maintainers

Thank you for contributing!
