# WITSML Studio Migration Plan - Browser-Focused

## A. Operating model (important constraints)

-   **Mac/Linux development:** All development and testing on Mac/Linux - no Windows access
    
-   **Browser-only scope:** Focus on WITSML Browser plugin only (SOAP operations)
    
-   **Minimal ext/ migration:** Extract only ~2-3K LOC needed for SOAP client, skip 47K+ LOC of ETP/WebSocket complexity
    

---

## B. Migration approach

**Selective modernization** - migrate only what's needed for browser functionality.

Replace Framework-only dependencies with modern .NET 8 equivalents.

---

## C. Step-by-step plan

### 1) Project modernization (src/ first) — immediate cross-platform value

**Goal:** Get core projects building on .NET 8 on Mac/Linux.

-   **Actions**
    
    -   Convert `Desktop.Core` to **SDK-style** + multi-target `net48;net8.0`
        
    -   Convert `Desktop.UnitTest` to **SDK-style** + target `net8.0`
        
    -   Convert `Desktop.Plugins.WitsmlBrowser` to **SDK-style** + multi-target `net48;net8.0`
        
    -   Migrate `packages.config` → **PackageReference** across all projects
        
-   **Validation gate**
    
    -   Linux CI builds all projects on net8.0
        
    -   Compile-only validation (no runtime testing needed yet)
        

---

### 2) Extract minimal WITSML client — replace ext/ dependencies

**Goal:** Create .NET 8 SOAP client for core WITSML operations.

-   **Actions**
    
    -   Create **`WitsmlClient.Core`** project targeting **net8.0**
        
    -   Extract data models: `Well`, `Wellbore`, `Log`, `WitsmlResult` POCOs
        
    -   Implement modern SOAP client using `HttpClient` + XML serialization
        
    -   Support 5 core operations: `GetCap`, `GetFromStore`, `AddToStore`, `UpdateInStore`, `DeleteFromStore`
        
-   **What to replace from ext/**
    
    -   `System.Web.Services` → `HttpClient` with SOAP XML formatting
        
    -   `PDS.WITSMLstudio.Connections` → Simple connection management
        
    -   `PDS.WITSMLstudio.Framework` → Basic utilities only
        
-   **What to skip from ext/**
    
    -   SuperWebSocket/WebSocket4Net (ETP protocol)
        
    -   Apache.Avro (binary serialization)
        
    -   Store.Core (server implementation)
        
    -   Framework.Web (web-specific features)
        
-   **Validation gate**
    
    -   Unit tests for SOAP XML generation/parsing
        
    -   Mock server integration tests
        
    -   Behavioral validation: correct XML structure, proper error handling
        

---

### 3) Update WitsmlBrowser plugin — use new client

**Goal:** Replace ext/ references with modern .NET 8 client.

-   **Actions**
    
    -   Update `MainViewModel.cs` to use `WitsmlClient.Core`
        
    -   Replace `WITSMLWebServiceConnection` with new `IWitsmlClient` interface
        
    -   Update dependency injection to use new client
        
    -   Test core workflows: connect → query capabilities → get wells → display tree
        
-   **Validation gate**
    
    -   Plugin compiles and runs on .NET 8
        
    -   Core operations work with mock/test data
        
    -   UI displays WITSML data correctly
        

---

### 4) Add real server integration — validate against live WITSML

**Goal:** Test against actual WITSML servers to ensure compatibility.

-   **Actions**
    
    -   Test against public WITSML test servers
        
    -   Validate XML request/response formats match WITSML standards
        
    -   Handle authentication, compression, error responses
        
    -   Performance testing for reasonable response times
        
-   **Validation gate**
    
    -   Successfully connects to real WITSML server
        
    -   Retrieves and displays actual well data
        
    -   Error handling works for common failure scenarios
        

---

### 5) UI modernization — .NET 8 WPF compatibility

**Goal:** Ensure WPF components work cleanly on .NET 8.

-   **Actions**
    
    -   Update WPF dependencies: AvalonEdit, Xceed to .NET 8 versions
        
    -   Replace MEF with Microsoft.Extensions.DependencyInjection
        
    -   Add `appsettings.json` alongside existing `app.config`
        
    -   Enable nullable reference types on new code
        
-   **Validation gate**
    
    -   Full application runs on .NET 8 (when Windows testing available)
        
    -   No runtime exceptions in common workflows
        

---

## D. Scope boundaries

### In Scope (Browser Plugin Only)
- WITSML SOAP operations (GetCap, GetFromStore, AddToStore, UpdateInStore, DeleteFromStore)
- Connection management and authentication
- XML request/response handling
- Basic data models (Well, Wellbore, Log)
- WPF UI for browsing WITSML data

### Out of Scope (Future Phases)
- ETP Browser plugin (WebSocket streaming)
- Data Replay plugin (simulation)
- Object Inspector plugin (schema viewing)
- Advanced query building
- Data compression/optimization
- Multi-server management

### Migration Effort Estimate
- **Total ext/ LOC:** ~50,000
- **Browser-needed LOC:** ~2,000-3,000 (4-6% of total)
- **Effort reduction:** 90%+ compared to full migration

---

## E. Development workflow (Mac/Linux only)

### Repository structure
```bash
/src                    # Original WPF projects (multi-target)
/ext                    # Original Framework dependencies (reference only)
/modern
  /WitsmlClient.Core    # New .NET 8 SOAP client
  /WitsmlClient.Tests   # Unit and integration tests
/tests
  /behavioral          # Contract validation tests
  /integration         # Mock server tests
```

### CI pipeline (Linux)
```bash
# Build and test pipeline
dotnet build --configuration Release
dotnet test --no-build --verbosity normal
dotnet pack WitsmlClient.Core --no-build
```

### Development cycle
1. **Develop on Mac/Linux:** All coding, unit testing, mock integration
2. **Validate behavior:** Use behavioral contracts instead of golden artifacts  
3. **Test with real servers:** Use public WITSML test endpoints
4. **Future Windows testing:** When Windows environment becomes available

---

## F. Next steps (immediate - 1-2 weeks)

1. **Convert Desktop.Core to SDK-style + .NET 8**
   ```bash
   cd src/Desktop.Core
   # Convert project file, update dependencies
   dotnet build --framework net8.0
   ```

2. **Create WitsmlClient.Core project**
   ```bash
   mkdir modern/WitsmlClient.Core
   dotnet new classlib --framework net8.0
   # Implement basic IWitsmlClient interface
   ```

3. **Extract minimal data models**
   - Copy Well, Wellbore, Log POCOs from ext/
   - Remove Framework-specific attributes
   - Add to WitsmlClient.Core

4. **Implement basic SOAP operations**
   - Start with GetCap (simplest operation)
   - Use HttpClient + XML serialization
   - Create unit tests

5. **Update WitsmlBrowser plugin**
   - Replace ext/ references with WitsmlClient.Core
   - Test compilation on .NET 8

---

## TL;DR

- **Scope:** Browser plugin only (~4% of total ext/ complexity)
- **Approach:** Extract minimal SOAP client, skip ETP/WebSocket entirely  
- **Development:** Mac/Linux only, no Windows dependencies
- **Validation:** Behavioral contracts + real server testing
- **Timeline:** Working browser on .NET 8 in 2-4 weeks vs 6+ months for full migration