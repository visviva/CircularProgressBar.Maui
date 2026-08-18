# Repository Guidelines

## Project Structure & Module Organization

`CircularProgressBar.Maui/` is the reusable control library. Its XAML control, code-behind, and
drawable live directly in that project. `CircularProgressBar.Maui.Demo/` is the sample application:
pages are in `Pages/`, presentation state is in `ViewModels/`, shared styling and assets are under
`Resources/`, and platform startup code is under `Platforms/`. The root
`CircularProgressBar.Maui.slnx` includes both projects. GitHub Actions workflows are in
`.github/workflows/`. 

Keep the shared MAUI package version in `Directory.Build.props`; both projects must use
`$(MauiVersion)` to prevent conflicting Windows PRI resources.

## Build, Test, and Development Commands

- `dotnet tool restore` installs the pinned Husky and CSharpier tools.
- `dotnet restore CircularProgressBar.Maui.slnx` restores all workloads and NuGet dependencies.
- `dotnet build CircularProgressBar.Maui.slnx` builds every target supported by the host OS.
- `dotnet build CircularProgressBar.Maui.Demo/CircularProgressBar.Maui.Demo.csproj -f net10.0-android --no-restore`
  matches the Android CI build.
- On Windows, replace the framework with `net10.0-windows10.0.19041.0`.
- `dotnet run --project CircularProgressBar.Maui.Demo -f <framework>` launches the demo locally.
- `dotnet csharpier check .` verifies formatting; `dotnet csharpier format .` applies it.

## Coding Style & Naming Conventions

Follow `.editorconfig`: four-space C# indentation, two-space XML/XAML indentation, LF endings, and a
110-column C# limit. Use file-scoped namespaces, explicit accessibility, braces for control flow,
and `readonly` fields where possible. Use PascalCase for types, methods, properties, and bindable
properties; use `_camelCase` for private fields. Keep XAML and its `.xaml.cs` code-behind together.
