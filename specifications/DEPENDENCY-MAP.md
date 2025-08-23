# WITSML Studio Dependency Mapping

## Critical Path: WitsmlBrowser Plugin Dependencies

### Direct Project References from WitsmlBrowser
```xml
<!-- From Desktop.Plugins.WitsmlBrowser.csproj -->
1. ext/witsml/ext/devkit-c/source/DevKitGenerator/DataAccess/
2. ext/witsml/ext/devkit-c/source/DevKitGenerator/DataAccess.WITSML/
3. ext/witsml/src/Core/
4. ext/witsml/src/Framework/
```

### Namespace Usage Analysis

#### 1. Energistics.DataAccess (from devkit-c)
**Used For:** SOAP client and data models
**Classes Used:**
- `WITSMLWebServiceConnection` - SOAP proxy
- `WMLSVersion` - Version enumeration
- `Functions` - Operation types (GetFromStore, etc.)
- `IDataObject` - Data object interface
- WITSML141.* - Data models (Well, Wellbore, Log, etc.)

**Migration Strategy:** Generate new client from WSDL

#### 2. PDS.WITSMLstudio.Framework (from ext/witsml/src/Framework)
**Used For:** Core utilities
**Classes Needed:**
- `OptionsIn` - Query options constants
- `ErrorCodes` - WITSML error code definitions
- Extension methods for LINQ/XML
- `DateTimeExtensions`
- `StringExtensions`

**Migration Strategy:** Extract ~10-15 files

#### 3. PDS.WITSMLstudio.Connections (from ext/witsml/src/Core)
**Used For:** Connection management
**Classes Needed:**
- `Connection` - Connection model
- `ConnectionExtensions` - Helper methods
- `WitsmlConnectionTest` - Connection validation
- `CompressionMethods` - GZIP settings

**Migration Strategy:** Extract ~5-8 files

## Minimal Extraction List

### From ext/witsml/src/Framework/
```
DateTimeExtensions.cs       # Date/time utilities
StringExtensions.cs          # String helpers
XmlExtensions.cs            # XML parsing
LinqExtensions.cs           # LINQ helpers
OptionsIn.cs                # Query options
ErrorCodes.cs               # Error definitions
```

### From ext/witsml/src/Core/
```
Connections/
├── Connection.cs           # Connection model
├── ConnectionExtensions.cs # Connection helpers
├── WitsmlConnectionTest.cs # Connection tester
└── CompressionMethods.cs   # Compression enum

Data/
├── DataObjectParser.cs     # XML to object parsing
└── WitsmlQueryParser.cs    # Query parsing
```

### Generated from WSDL
```
WitsmlClient/
├── WITSMLWebServiceConnection.cs  # Generated SOAP proxy
├── WMLS_Operations.cs              # Service operations
├── DataTypes/                      # WITSML data models
│   ├── Well.cs
│   ├── Wellbore.cs
│   ├── Log.cs
│   └── Trajectory.cs
└── Enums/
    ├── WMLSVersion.cs
    └── Functions.cs
```

## Dependencies NOT Needed

### Can Skip from ext/witsml/
- Store.Core/ - Server implementation (428 files)
- Store.* - All store projects
- Framework.Web/ - Web-specific code
- ETP-related code - Not in browser scope
- MEF composition - Can use DI instead
- Avro serialization - ETP-only
- WebSocket libraries - ETP-only

## File Count Summary
- **Need to Extract:** ~20-25 files
- **Need to Generate:** ~50-100 files (from WSDL)
- **Can Skip:** ~50,000+ lines of code

## Dependency Resolution Order

1. **First: Generate SOAP Client**
   - Download WSDL files
   - Generate with dotnet-svcutil
   - Create connection wrapper

2. **Second: Extract Core Utilities**
   - Copy needed Framework files
   - Update namespaces
   - Remove Framework dependencies

3. **Third: Extract Connection Code**
   - Copy Connection classes
   - Update to use new SOAP client
   - Add async support

4. **Fourth: Update Plugin**
   - Replace project references
   - Update using statements
   - Convert to async patterns

## Validation Checklist

### Must Work After Migration:
- [ ] Connect to WITSML server
- [ ] Execute GetCap operation
- [ ] Query wells (GetFromStore)
- [ ] Parse XML responses
- [ ] Display in tree view
- [ ] Handle authentication
- [ ] Support GZIP compression
- [ ] Show error messages

### Nice to Have:
- [ ] Certificate validation options
- [ ] Proxy server support
- [ ] Request/response logging
- [ ] Performance metrics

## Risk Areas

### High Risk
- **SOAP Compatibility:** Generated client must match server expectations
- **XML Namespaces:** Must preserve exact namespace handling

### Medium Risk  
- **Authentication:** Basic auth, certificates, tokens
- **Compression:** GZIP request/response handling

### Low Risk
- **Error Codes:** Simple constants to copy
- **Extensions:** Utility methods easy to port
