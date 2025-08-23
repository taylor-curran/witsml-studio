# Migration Audit Results (src/ + ext/)

## Summary
- **src/ projects**: Mixed complexity - some portable, some Framework-only
- **ext/witsml projects**: 95% Framework-only (sidecar required)

---

## src/ Projects Analysis

| Project | Files | Classification | Migration Strategy | Effort |
|---------|-------|----------------|-------------------|---------|
| **Desktop** | ~50 | Framework-only | Multi-target net48;net8.0-windows | Medium |
| **Desktop.Core** | ~100 | Mostly Portable | Multi-target net48;net8.0 | Low |
| **Desktop.Plugins.WitsmlBrowser** | ~30 | Framework-only | Depends on ext/ sidecar | High |
| **Desktop.Plugins.EtpBrowser** | ~25 | Framework-only | Depends on ext/ sidecar | High |
| **Desktop.Plugins.DataReplay** | ~20 | Framework-only | Depends on ext/ sidecar | Medium |
| **Desktop.Plugins.ObjectInspector** | ~15 | Mostly Portable | Multi-target net48;net8.0 | Low |
| **Desktop.UnitTest** | ~20 | Portable | Convert to net8.0 | Low |
| **Desktop.IntegrationTest** | ~15 | Framework-only | Depends on ext/ sidecar | Medium |

### src/ Migration Blockers
- **WPF Dependencies**: Desktop project requires `net8.0-windows`
- **Legacy WebSocket**: Same SuperWebSocket/WebSocket4Net as ext/
- **MEF Composition**: `System.ComponentModel.Composition` throughout
- **Caliburn.Micro**: Old version (3.0.0) may need updates
- **ClickOnce Deployment**: Legacy deployment model

### src/ Portable Components
- **MVVM ViewModels**: Most business logic can be cross-platform
- **Data Models**: POCO classes should migrate easily  
- **Converters/Behaviors**: WPF-specific but .NET 8 compatible
- **Configuration**: Can modernize to appsettings.json

---

## ext/witsml Projects Analysis

## Project Breakdown

| Project | Files | Classification | Reason |
|---------|-------|----------------|---------|
| **Core** | 96 | Framework-only | SuperWebSocket, WebSocket4Net, System.Web.Services |
| **Framework** | 23 | Framework-only | System.ComponentModel.Composition (MEF) |
| **Framework.Web** | 18 | Framework-only | ASP.NET Framework dependencies |
| **Store.Core** | 428 | Framework-only | Deep integration with Core + Framework |
| **Store.IntegrationTest** | 257 | Framework-only | References all above projects |

## Critical Dependencies (Migration Blockers)

### WebSocket Stack
- `SuperWebSocket` (0.9.0.2) - Legacy, no .NET 8 support
- `WebSocket4Net` (0.15.2) - Legacy, no .NET 8 support
- Custom ETP protocol implementation built on these

### Serialization
- `Apache.Avro` (1.7.7.2) - Binary protocol serialization
- Custom WITSML XML serialization

### Framework-Only APIs
- `System.Web.Services` - SOAP client generation
- `System.ComponentModel.Composition` - MEF dependency injection
- `Microsoft.VisualBasic` - Legacy VB.NET utilities

### External References (Nested Submodules)
- `ext/devkit-c/source/DevKitGenerator/DataAccess.*` - Energistics DevKit
- `ext/etp-devkit/ext/etp.net/src/ETP.Messages` - ETP protocol messages

## Migration Strategy Per Project

| Project | Strategy | Effort | Priority |
|---------|----------|---------|----------|
| Core | Sidecar | High | Critical |
| Framework | Sidecar | Medium | Critical |
| Framework.Web | Sidecar | Low | Optional |
| Store.Core | Sidecar | High | Critical |
| Store.IntegrationTest | Skip/Rewrite | Medium | Later |

**Total LOC requiring sidecar: ~50,000+**
