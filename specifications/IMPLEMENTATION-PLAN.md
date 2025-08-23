# WITSML Studio .NET 8 Migration - Implementation Plan

## Executive Summary
Migration of WITSML Studio's Browser plugin from .NET Framework 4.5.2 to .NET 8, focusing on creating a minimal cross-platform SOAP client while preserving existing architecture.

## Key Discoveries

### 1. SOAP Client Architecture
- **Source**: `Energistics.DataAccess` namespace comes from `ext/witsml/ext/devkit-c` submodule
- **Projects**:
  - `DataAccess.csproj` - Core data access interfaces
  - `DataAccess.WITSML.csproj` - WITSML-specific implementations
- **Problem**: Submodules are empty, original source unavailable
- **Solution**: Generate new SOAP client using .NET 8 tools

### 2. Critical Dependencies
**WitsmlBrowser Plugin Dependencies:**
```
PDS.WITSMLstudio.Connections    → Connection management
PDS.WITSMLstudio.Framework       → Core framework utilities  
Energistics.DataAccess           → SOAP client (from devkit-c)
Energistics.DataAccess.WITSML141 → WITSML 1.4.1 data models
```

### 3. SOAP Operations Used
- `WMLS_GetFromStore` - Query data
- `WMLS_AddToStore` - Create objects
- `WMLS_UpdateInStore` - Modify objects
- `WMLS_DeleteFromStore` - Remove objects
- `WMLS_GetCap` - Server capabilities
- `WMLS_GetVersion` - Protocol version
- `WMLS_GetBaseMsg` - Error messages

## Implementation Steps

### Phase 1: SOAP Client Generation (Week 1)
1. **Obtain WITSML WSDL**
   - Download from http://schemas.energistics.org/witsml/
   - Version 1.4.1.1 and 1.3.1.1 support needed
   
2. **Generate .NET 8 Client**
   ```bash
   # Use dotnet-svcutil for .NET 8 compatibility
   dotnet tool install --global dotnet-svcutil
   dotnet-svcutil http://schemas.energistics.org/witsml/wsdl/WITSML_v1.4.1.1_API.wsdl \
     --namespace "Energistics.DataAccess" \
     --outputDir ./generated
   ```

3. **Create Minimal Client Library**
   ```
   src/WitsmlClient/
   ├── WitsmlClient.csproj          # .NET 8 library
   ├── Generated/                    # SOAP client from svcutil
   ├── WitsmlConnection.cs           # Connection wrapper
   ├── Authentication/               # Auth handlers
   └── Compression/                  # GZIP support
   ```

### Phase 2: Extract Core Dependencies (Week 1-2)
1. **Minimal Framework Components**
   ```
   src/WitsmlFramework/
   ├── WitsmlFramework.csproj       # .NET 8 library
   ├── DataObjectParser.cs           # XML parsing
   ├── Extensions/                   # LINQ extensions
   ├── OptionsIn.cs                  # Query options
   └── ErrorCodes.cs                 # WITSML error codes
   ```

2. **Connection Management**
   ```
   src/WitsmlConnections/
   ├── WitsmlConnections.csproj     # .NET 8 library  
   ├── Connection.cs                 # Connection model
   ├── ConnectionExtensions.cs       # Helper methods
   └── WitsmlConnectionTest.cs       # Connection validation
   ```

### Phase 3: Update WitsmlBrowser Plugin (Week 2-3)
1. **Project File Migration**
   - Update to .NET 8 SDK-style project
   - Replace WCF references with new client
   - Update NuGet packages to .NET 8 versions

2. **Code Updates**
   ```csharp
   // Old (WCF-based)
   var wmls = connection.CreateProxy(WMLSVersion.WITSML141);
   
   // New (.NET 8)
   var wmls = new WitsmlClient(connection);
   await wmls.GetFromStoreAsync(objectType, xmlIn, optionsIn);
   ```

3. **Async Pattern Migration**
   - Convert sync SOAP calls to async
   - Update ViewModels for async/await

### Phase 4: Testing Strategy Implementation (Week 3-4)
1. **Unit Tests**
   ```
   tests/WitsmlClient.Tests/
   ├── SerializationTests.cs         # XML serialization
   ├── ConnectionTests.cs            # Connection setup
   └── MockServerTests.cs            # Mock SOAP responses
   ```

2. **Integration Tests**
   ```
   tests/WitsmlBrowser.IntegrationTests/
   ├── QueryTests.cs                 # Real server queries
   ├── CrudOperationsTests.cs        # Create/Update/Delete
   └── BehaviorTests.cs              # UI behavior validation
   ```

### Phase 5: UI Compatibility (Week 4)
1. **WPF → Avalonia Migration** (if needed for Mac/Linux)
   - Keep ViewModels unchanged
   - Convert XAML views to Avalonia
   - Update data binding syntax

2. **Or use .NET 8 WPF** (Windows-only initially)
   - Simpler migration path
   - Mac/Linux support via Wine/Parallels

## Project Structure (Post-Migration)

```
witsml-studio/
├── src/
│   ├── WitsmlClient/              # New SOAP client (.NET 8)
│   ├── WitsmlFramework/           # Core utilities (.NET 8)
│   ├── WitsmlConnections/         # Connection mgmt (.NET 8)
│   ├── Desktop.Core/              # Updated to .NET 8
│   └── Desktop.Plugins.WitsmlBrowser/ # Updated to .NET 8
├── tests/
│   ├── WitsmlClient.Tests/
│   └── WitsmlBrowser.IntegrationTests/
└── specifications/
    ├── SCOPE.md
    ├── TESTING-STRATEGY.md
    └── IMPLEMENTATION-PLAN.md
```

## Risk Mitigation

### High Priority Risks
1. **SOAP Compatibility**
   - Risk: Generated client incompatible with servers
   - Mitigation: Test against multiple WITSML servers early
   
2. **Missing devkit-c Source**
   - Risk: Can't replicate exact behavior
   - Mitigation: Focus on protocol compliance, not implementation

### Medium Priority Risks
1. **Async Migration Complexity**
   - Risk: UI deadlocks or race conditions
   - Mitigation: Systematic async/await conversion with tests

2. **Cross-platform UI**
   - Risk: WPF doesn't work on Mac/Linux
   - Mitigation: Start with console tests, defer UI decision

## Success Criteria
- [ ] SOAP client passes all protocol tests
- [ ] Browser plugin queries data successfully
- [ ] CRUD operations work correctly
- [ ] Tests run on Mac/Linux
- [ ] No dependency on ext/witsml submodule

## Timeline
- **Week 1**: SOAP client generation and core extraction
- **Week 2**: Plugin migration begins
- **Week 3**: Testing implementation
- **Week 4**: Integration and validation
- **Week 5**: Buffer and documentation

## Next Immediate Steps
1. Download WITSML WSDL files
2. Generate .NET 8 SOAP client
3. Create minimal test project
4. Validate against public WITSML server
