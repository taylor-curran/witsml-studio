# Session 1: Scaffold & Initial .NET 8 Upgrade

## 📍 Strategy: Build-Break-Fix Iteratively
**Goal**: Get everything on .NET 8 ASAP (even if broken), then fix incrementally with validation at each step.

## Phase 1A: Minimal Scaffold of modern-ext/ (30 min)
```bash
# Create basic structure
mkdir -p modern-ext/WitsmlClient
mkdir -p modern-ext/WitsmlFramework
mkdir -p modern-ext/WitsmlClient.Tests

# Create empty .NET 8 projects
cd modern-ext/WitsmlClient
dotnet new classlib -n WitsmlClient -f net8.0
cd ../WitsmlFramework  
dotnet new classlib -n WitsmlFramework -f net8.0
cd ../WitsmlClient.Tests
dotnet new nunit -n WitsmlClient.Tests -f net8.0

# Add references
dotnet add reference ../WitsmlClient/WitsmlClient.csproj
dotnet add reference ../WitsmlFramework/WitsmlFramework.csproj

# Verify it builds (empty but valid)
cd ..
dotnet build
```

✅ **Validation Point 1**: Empty projects build on .NET 8

## Phase 1B: Create Basic Test Infrastructure (30 min)

Add a simple test to verify the scaffold works:

**modern-ext/WitsmlClient.Tests/ScaffoldTests.cs**:
```csharp
using NUnit.Framework;

namespace WitsmlClient.Tests;

[TestFixture]
public class ScaffoldTests
{
    [Test]
    public void ProjectsExist()
    {
        Assert.Pass("Modern-ext scaffold is set up");
    }
    
    [Test]
    public void CanRunOnLinux()
    {
        // This test verifies we're not Windows-dependent
        var platform = Environment.OSVersion.Platform;
        Assert.That(platform, Is.AnyOf(
            PlatformID.Unix, 
            PlatformID.MacOSX, 
            PlatformID.Win32NT));
    }
}
```

```bash
# Verify tests run on current platform
cd modern-ext/WitsmlClient.Tests
dotnet test
```

✅ **Validation Point 2**: Tests pass on Linux/Mac/Windows

## Phase 1C: Simple Runtime Validation (15 min)

Create a simple console app to verify runtime if needed:

**modern-ext/RuntimeCheck/RuntimeCheck.csproj** (optional):
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  
  <ItemGroup>
    <ProjectReference Include="../WitsmlClient/WitsmlClient.csproj" />
    <ProjectReference Include="../WitsmlFramework/WitsmlFramework.csproj" />
  </ItemGroup>
</Project>
```

**modern-ext/RuntimeCheck/Program.cs** (optional):
```csharp
using System;

Console.WriteLine($"✅ .NET 8 Runtime Check");
Console.WriteLine($"Framework: {Environment.Version}");
Console.WriteLine($"Platform: {Environment.OSVersion.Platform}");
Console.WriteLine($"OS: {Environment.OSVersion}");
Console.WriteLine($"\n✅ Modern-ext scaffold is ready!");
```

```bash
# Quick validation - this is all that's really needed:
cd modern-ext
dotnet build
dotnet test

# Optional: Run console app for runtime info
cd RuntimeCheck
dotnet run
```

✅ **Validation Point 3**: Build succeeds and tests pass

## Required to Work at End of Session

### 1. Directory Structure
```
modern-ext/
├── WitsmlClient/
│   └── WitsmlClient.csproj (net8.0)
├── WitsmlFramework/
│   └── WitsmlFramework.csproj (net8.0)
└── WitsmlClient.Tests/
    ├── WitsmlClient.Tests.csproj (net8.0)
    └── ScaffoldTests.cs
```

### 2. Validation Checklist
- ✅ `dotnet build` succeeds in modern-ext/ (targeting .NET 8.0+)
- ✅ `dotnet test` passes basic scaffold tests (on .NET 8.0+)
- ✅ Cross-platform test verifies Linux/Mac/Windows compatibility
- ✅ Zero Windows-specific dependencies in modern-ext/

## NOT Required to Work (Out of Scope)
- ❌ src/ projects compiling
- ❌ SOAP client generation
- ❌ Any meaningful code extraction from ext/witsml
- ❌ Integration with Desktop.Core or WitsmlBrowser
- ❌ Actual WITSML functionality

These are handled in Session 2 and 3.

**Next**: Session 2 will upgrade src/ to .NET 8, generate SOAP client, and start extracting code from ext/
