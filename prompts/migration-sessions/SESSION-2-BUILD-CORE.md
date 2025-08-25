# Session 2: Selective Upgrade & Stub Implementation

## 📍 Context: .NET Framework 4.5.2 → .NET 8 Migration
**Primary Focus**: Upgrade `src/` projects from .NET Framework 4.5.2 to .NET 8.0

The `src/` directory contains the main WPF desktop application built on .NET Framework 4.5.2. Upgrading from Framework to .NET 8 is already a significant undertaking involving:
- Framework API changes and removals
- NuGet package updates and replacements  
- Build system migration (packages.config → PackageReference)
- WPF compatibility adjustments

To keep this manageable, we're **stubbing out** the `ext/witsml` dependencies rather than trying to migrate both `src/` and `ext/` simultaneously. This constraint allows us to focus on one major challenge at a time.

## ⚠️ IMPORTANT: Breaking Changes Are Expected and Acceptable

**The .NET Framework 4.5.2 app does NOT need to keep running!** 

During this session:
- ✅ **OK to break**: The main Desktop app (.NET Framework 4.5.2)
- ✅ **OK to break**: Other plugins (ETP Browser, Data Replay, Object Inspector)  
- ✅ **OK to break**: Any .NET Framework components not in scope
- ✅ **Mixed frameworks won't work**: You cannot run .NET Framework 4.5.2 and .NET 8 together

**Success = Desktop.Core + WitsmlBrowser running on .NET 8.0 (standalone)**

The goal is NOT to maintain backwards compatibility or keep the old app running. We're doing a selective upgrade where only the migrated components need to work, and they'll run independently on .NET 8.

## 📍 Test Projects Overview
**Important**: There are two groups of test projects:
1. **modern-ext/WitsmlClient.Tests** (NEW) - Tests for the new SOAP client (created in Session 1)
2. **src/Desktop.IntegrationTest & Desktop.UnitTest** (EXISTING) - Tests for Desktop.Core + WitsmlBrowser
   - These test WPF ViewModels, plugin loading, and connection management
   - Currently on .NET Framework 4.5.2 - MUST be upgraded alongside the main projects
   - Will validate our migration even with stub implementations

**Testing Philosophy**: Create and run simple sanity check tests as you go! The existing test coverage is sparse, so don't hesitate to write quick tests when you need to verify something works. For example, create a simple test to verify WitsmlClient can be instantiated, or that a ViewModel loads without crashing. These ad-hoc tests are your safety net.

**Note**: Comprehensive test coverage is NOT the goal - focus on getting existing tests to compile and run. Additional tests are encouraged but optional.

## 📍 Strategy: Stub-First Migration for WitsmlBrowser Only
**Goal**: Upgrade ONLY Desktop.Core + WitsmlBrowser to .NET 8 using stub implementations to get compilation working

Upgrade ONLY Desktop.Core + WitsmlBrowser to .NET 8.0
^to further reduce scope for this session

⚠️ **IMPORTANT**: Only create stubs that WitsmlBrowser directly needs. Do NOT create stubs for:
- ❌ ETP-related interfaces (`IEtpClient`, `IEtpSession`, WebSocket)
- ❌ DataReplay plugin interfaces
- ❌ Object Inspector plugin interfaces
- ❌ Store/MongoDB components
- ❌ Anything not directly referenced by WitsmlBrowser

## Phase 2A: Create Minimal Stubs in modern-ext/ (30 min)

Create ONLY the interfaces and stubs that WitsmlBrowser requires to compile:

**modern-ext/WitsmlFramework/Connection.cs**:
```csharp
namespace WitsmlFramework;

public class Connection
{
    public string Name { get; set; }
    public string Uri { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
```

**modern-ext/WitsmlFramework/IWitsmlClient.cs**:
```csharp
namespace WitsmlFramework;

public interface IWitsmlClient
{
    Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities);
    Task<string> AddToStoreAsync(string objectType, string xml, string options, string capabilities);
    Task<string> UpdateInStoreAsync(string objectType, string xml, string options, string capabilities);
    Task<string> DeleteFromStoreAsync(string objectType, string query, string options, string capabilities);
}
```

**modern-ext/WitsmlClient/WitsmlClient.cs**:
```csharp
namespace WitsmlClient;
using WitsmlFramework;

public class WitsmlClient : IWitsmlClient
{
    private readonly Connection _connection;
    
    public WitsmlClient(Connection connection)
    {
        _connection = connection;
    }
    
    // Stub implementations - just return empty for now
    public Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities)
    {
        // TODO: Replace with real SOAP implementation in Session 3
        return Task.FromResult("<stub>GetFromStore not implemented</stub>");
    }
    
    public Task<string> AddToStoreAsync(string objectType, string xml, string options, string capabilities)
    {
        return Task.FromResult("<stub>AddToStore not implemented</stub>");
    }
    
    public Task<string> UpdateInStoreAsync(string objectType, string xml, string options, string capabilities)
    {
        return Task.FromResult("<stub>UpdateInStore not implemented</stub>");
    }
    
    public Task<string> DeleteFromStoreAsync(string objectType, string query, string options, string capabilities)
    {
        return Task.FromResult("<stub>DeleteFromStore not implemented</stub>");
    }
}
```

```bash
# Verify stubs compile
cd modern-ext
dotnet build
```

✅ **Validation Point 1**: Stub implementations compile

## Phase 2B: Selective src/ Upgrade - Desktop.Core Only (30 min)

**Desktop.Core/Desktop.Core.csproj**:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="Caliburn.Micro" Version="4.0.212" />
    <PackageReference Include="AvalonDock" Version="4.72.1" />
    <!-- Only reference the Framework, not Client yet -->
    <ProjectReference Include="..\..\modern-ext\WitsmlFramework\WitsmlFramework.csproj" />
  </ItemGroup>
</Project>
```

```bash
# Try building ONLY Desktop.Core
cd src/Desktop.Core
dotnet build 2>&1 | tee core-errors-initial.txt

# Common errors will be:
# - Missing MEF attributes → Comment out temporarily
# - Missing ETP interfaces → Create stub interfaces
# - Missing ext/witsml types → Add to WitsmlFramework stubs
```

✅ **Validation Point 2**: Desktop.Core compiles (with stubs)

## Phase 2B: Upgrade Test Projects (20 min)

Upgrade the existing test projects that validate Desktop.Core + WitsmlBrowser:

```bash
cd src/Desktop.IntegrationTest
# Upgrade to .NET 8.0 Windows (needs WPF)
dotnet migrate # or manually update .csproj
# Update to:
# <Project Sdk="Microsoft.NET.Sdk">
#   <PropertyGroup>
#     <TargetFramework>net8.0-windows</TargetFramework>
#     <UseWPF>true</UseWPF>
#     <OutputType>Library</OutputType>
#   </PropertyGroup>
# Add NuGet packages for MSTest or convert to NUnit/xUnit

cd ../Desktop.UnitTest
# Same upgrade process
dotnet migrate # or manually update .csproj
```

Try to compile - expect errors due to missing ext/ dependencies. That's OK for now.

## Phase 2C: Upgrade WitsmlBrowser Plugin (30 min)

**Desktop.Plugins.WitsmlBrowser/Desktop.Plugins.WitsmlBrowser.csproj**:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
  
  <ItemGroup>
    <ProjectReference Include="..\Desktop.Core\Desktop.Core.csproj" />
    <ProjectReference Include="..\..\modern-ext\WitsmlClient\WitsmlClient.csproj" />
    <ProjectReference Include="..\..\modern-ext\WitsmlFramework\WitsmlFramework.csproj" />
  </ItemGroup>
</Project>
```

## Phase 2D: Add Missing Stubs Based on Errors (30 min)

Based on compilation errors, add more stubs as needed:

⚠️ **Rule**: Only add a stub if you get a compilation error that requires it. Don't preemptively create stubs "just in case".

**modern-ext/WitsmlFramework/ErrorCodes.cs**:
```csharp
namespace WitsmlFramework;

public static class ErrorCodes
{
    public const int Success = 0;
    public const int GeneralError = -1;
    public const int InvalidRequest = -401;
    // Add more as needed from build errors
}
```

**modern-ext/WitsmlFramework/Functions.cs**:
```csharp
namespace WitsmlFramework;

public enum Functions
{
    GetFromStore,
    AddToStore,
    UpdateInStore,
    DeleteFromStore,
    GetCap,
    GetVersion
}
```

```bash
# After adding each stub, retest
cd src/Desktop.Plugins.WitsmlBrowser
dotnet build

# Goal: Get to zero errors even if functionality is stubbed
```

✅ **Validation Point 3**: WitsmlBrowser plugin compiles

## Phase 2D: Run Tests with Stubs (15 min)

Test what we can with stub implementations:

```bash
# Try running the upgraded test projects
cd src/Desktop.IntegrationTest
dotnet test --filter "TestCategory!=RequiresServer"

cd ../Desktop.UnitTest  
dotnet test

# Expected results:
# - Tests that check basic instantiation should PASS
# - Tests that require real WITSML operations will FAIL (that's OK)
# - ViewModels and plugin loading tests might partially work
```

✅ **Validation Point 4**: Test projects compile and some basic tests pass

## Success Criteria for Session 2
1. ✅ Stub interfaces created in modern-ext/
2. ✅ Desktop.Core upgraded to .NET 8 and compiles
3. ✅ WitsmlBrowser plugin upgraded to .NET 8 and compiles
4. ✅ Desktop.IntegrationTest & Desktop.UnitTest upgraded to .NET 8 and compile
5. ✅ Some basic tests pass (instantiation, ViewModels, non-WITSML operations)
6. ✅ Other plugins (ETP, DataReplay) remain untouched/broken
7. ✅ Can instantiate WitsmlClient (even if stubbed)

## What This Session Does NOT Do
- ❌ Implement real SOAP functionality
- ❌ Upgrade Desktop main app
- ❌ Fix other plugins
- ❌ Extract real code from ext/witsml
- ❌ Make anything actually work

**Next**: Session 3 will replace stubs with real SOAP client and extract minimal code from ext/
