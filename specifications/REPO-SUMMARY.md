# WITSMLstudio Desktop – Repo Summary & Onboarding

Use this to quickly understand the repo, set up the environment, and build/run the app.

Forked from
https://github.com/pds-group/witsml-studio
and
https://github.com/pds-technology/witsml
^submodule for ext/witsml

---

## TL;DR

- WPF desktop app targeting .NET Framework 4.5.2.
- Solution file: `src/PDS.WITSMLstudio.Desktop.sln`.
- Requires Windows + Visual Studio (WPF + .NET 4.x dev tools) to build/run the UI.
- Critical: initialize the `ext/witsml` git submodule before building.

```bash
# Clone (recommended)
git clone --recurse-submodules <repo-url>

# If already cloned
git submodule update --init --recursive
```

---

## What this repo is

A Windows desktop application for exploring and testing WITSML/ETP servers.
- UI is WPF + Caliburn.Micro.
- Protocol and domain code comes from `ext/witsml` (Energistics DevKit and related libs).
- Plugins add specific capabilities (ETP browser, WITSML browser, Data replay, Object inspector).

See `README.md` for original project description and links.

---

## Repo layout (high level)

```
/                         # Root
  README.md
  LICENSE.txt
  NOTICE.txt
  .gitmodules             # Declares ext/witsml submodule

/specifications/          # Local docs for scope & migration
  SCOPE.md
  high-level-migration.md
  REPO-SUMMARY.md         # This file

/src/                     # Solution + projects
  PDS.WITSMLstudio.Desktop.sln
  Desktop/                # WPF host app (Startup project)
  Desktop.Core/           # Shared UI + VM + behaviors + services
  Desktop.IntegrationTest/# Integration tests
  Desktop.UnitTest/       # Unit tests (MSTest)
  Desktop.Plugins.DataReplay/     # Simulates data streaming
  Desktop.Plugins.EtpBrowser/     # ETP protocol browser
  Desktop.Plugins.ObjectInspector/# WITSML object inspector + schema views
  Desktop.Plugins.WitsmlBrowser/  # WITSML (SOAP) browser
  External/               # Shared assembly info, etc.

/ext/
  witsml/                 # Git submodule (must be initialized)
    # Contains Energistics DevKit, ETP messages, DataAccess, etc.
    # Referenced as project dependencies from csproj files
```

---

## Projects and how they relate

- __`Desktop`__ (`src/Desktop/Desktop.csproj`)
  - WPF shell and app entry (`App.xaml`, `Bootstrapper.cs`).
  - Depends on `Desktop.Core`, plugins, and several `ext/witsml` projects.
- __`Desktop.Core`__ (`src/Desktop.Core/Desktop.Core.csproj`)
  - Reusable MVVM components, behaviors, converters, runtime services, view models.
  - Hosts core views like `ShellView`, `ConnectionView`, `DataGridView`, etc.
- __Plugins__
  - `Desktop.Plugins.WitsmlBrowser`: WITSML over SOAP (uses `ext/witsml` DataAccess/Core/Framework).
  - `Desktop.Plugins.EtpBrowser`: ETP messaging over WebSocket (uses `ext/witsml` ETP/DevKit).
  - `Desktop.Plugins.DataReplay`: Simulates/replays data flows.
  - `Desktop.Plugins.ObjectInspector`: Visualizes objects with Energistics schemas.
- __Tests__
  - `Desktop.UnitTest` (MSTest) and `Desktop.IntegrationTest` reference both `src` projects and `ext/witsml` projects.

Key third-party dependencies (via `packages.config`): Caliburn.Micro, log4net, Xceed WPF Toolkit, AvalonEdit, WebSocket4Net/SuperWebSocket, Newtonsoft.Json.

---

## ext/witsml Analysis (Critical for Migration)

The `ext/witsml` submodule contains **5 major .NET Framework 4.5.2 projects** with deep dependencies:

### Core Projects
- **`Core`** (96 files) - WITSML/ETP protocol implementations, data readers, SOAP clients
- **`Framework`** (23 files) - MEF composition container, dependency injection
- **`Framework.Web`** (18 files) - Web-specific configuration and security
- **`Store.Core`** (428 files) - WITSML data store implementation
- **`Store.IntegrationTest`** (257 files) - Integration tests

### Critical Dependencies (Migration Blockers)
From `Core.csproj` analysis:
- **Legacy WebSocket**: `SuperWebSocket` (0.9.0.2), `WebSocket4Net` (0.15.2)
- **Legacy Serialization**: `Apache.Avro` (1.7.7.2), custom binary protocols
- **Framework-only**: `System.Web.Services` (SOAP), `System.ComponentModel.Composition` (MEF)
- **Deep External References**: 
  - `ext/devkit-c/source/DevKitGenerator/DataAccess.*` (Energistics DevKit)
  - `ext/etp-devkit/ext/etp.net/src/ETP.Messages` (ETP protocol messages)

### Migration Complexity Assessment
- **High Risk**: ETP protocol stack (WebSocket + Avro + binary messaging)
- **Medium Risk**: WITSML SOAP clients (can regenerate for .NET 8)
- **Low Risk**: Data models and business logic (mostly POCO classes)

### Submodule Dependencies (Nested)
The `ext/witsml` itself has submodules:
- `ext/devkit-c` - Energistics C# DevKit (WITSML/PRODML/RESQML schemas)
- `ext/etp-devkit` - ETP protocol implementation

**Total Framework-only LOC**: ~50,000+ lines across all submodules

---

## Getting started (Windows)

1) __Prerequisites__
- Windows 10/11.
- Visual Studio 2019 or 2022 with “.NET desktop development” workload.
- .NET Framework 4.5.2 Developer Pack (VS will prompt to install if missing).

2) __Clone with submodules__
```bash
git clone --recurse-submodules <repo-url>
# or, inside an existing clone
git submodule update --init --recursive
```

3) __Open the solution__
- Open `src/PDS.WITSMLstudio.Desktop.sln` in Visual Studio.
- Restore NuGet packages (VS usually does this automatically for `packages.config`).

4) __Build & run__
- Set startup project to `Desktop`.
- Build `Debug` and press F5 to run. The WPF app should launch.

5) __Run tests__
- Use Test Explorer to run unit/integration tests (`MSTest`).

---

## Where key things live

- __Entry point__: `src/Desktop/App.xaml`, `src/Desktop/App.xaml.cs`, `src/Desktop/Bootstrapper.cs`.
- __Shell & VMs__: `src/Desktop.Core/ViewModels/ShellViewModel.cs` and related views (`Views/*.xaml`).
- __Logging__: `src/Desktop/log4net.config` (copied to output).
- __Configuration__: legacy `app.config` in projects; no `appsettings.json` yet.
- __Project references to submodule__ (examples from csproj):
  - `ext/witsml/ext/etp-devkit/ext/etp.net/src/ETP.Messages/ETP.Messages.csproj`
  - `ext/witsml/ext/etp-devkit/src/DevKit/DevKit.csproj`
  - `ext/witsml/src/Core/Core.csproj`
  - `ext/witsml/src/Framework/Framework.csproj`
  - `ext/witsml/ext/devkit-c/source/DevKitGenerator/DataAccess.*/*.csproj`

---

## Troubleshooting build issues

- __Missing `ext/witsml` projects__
  - Symptom: project references cannot be found.
  - Fix: run `git submodule update --init --recursive`.

- __Missing .NET Framework targeting pack__
  - Symptom: build errors about v4.5.2 not installed.
  - Fix: install the .NET Framework 4.5.2 Developer Pack (or retarget to 4.8 if you intend to migrate; see specs).

- __NuGet restore problems__
  - Symptom: missing Caliburn.Micro, log4net, etc.
  - Fix: in VS, right-click solution → “Restore NuGet Packages”. Ensure you have access to nuget.org.

- __WPF app won’t run on non-Windows__
  - The UI is Windows-only. Use a Windows VM/runner to build/run the desktop app.

---

## Migration context (why `specifications/` exists)

- See `specifications/SCOPE.md` for current→target frameworks and constraints.
- See `specifications/high-level-migration.md` for the dual-track modernization plan:
  - Keep a Windows baseline of the WPF host.
  - Gradually decouple `src` from `ext` via abstractions.
  - Optionally sidecar legacy framework-only pieces while introducing modern .NET 8 components and tests.

This summary reflects the current state: .NET Framework 4.5.2 WPF with `packages.config` and project references into `ext/witsml`.
