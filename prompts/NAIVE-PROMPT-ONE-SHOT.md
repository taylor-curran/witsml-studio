# WITSML Studio .NET 8 Migration Task

taylor-curran/witsml is a submodule to taylor-curran/witsml-studio

Hi! Your task is to migrate the WITSML Browser plugin from .NET Framework 4.5.2 to .NET 8, making it cross-platform compatible. The goal is to have a working WITSML Browser running on .NET 8 that can connect to WITSML servers and perform basic operations.

## 📚 Specifications Tour

The `specifications/` directory contains all the planning documents you'll need:

### Essential Reading Order:
1. **SCOPE.md** - Understand the project boundaries and migration philosophy
2. **HIGH-LEVEL-MIG.md** - See the phased approach and critical distinction between ext/ vs src/
3. **IMPLEMENTATION-PLAN.md** - Detailed technical steps for each phase
4. **DEPENDENCY-MAP.md** - Exactly which files to extract from ext/witsml
5. **WITSML-PROTOCOL.md** - SOAP operations and WSDL details

### Reference Documents:
- **NET8-CLIENT-SAMPLE.md** - Code examples for the new SOAP client
- **BEHAVIOR_MAP.md** - Understand current app behavior to preserve
- **VALIDATION-CHECKLIST.md** - Success criteria for each phase
- **TESTING-STRATEGY.md** - How to test without Windows

## 🔑 Critical Architecture Decision

### Two Different Approaches:
- **ext/ code**: Complete rebuild from scratch (50K+ LOC of legacy Framework 4.5.2)
- **src/ code**: Traditional in-place upgrade (modern WPF worth preserving)

### Important: Submodule Structure
- `ext/witsml/` is a git submodule (separate repository)
- **DO NOT modify the submodule directly**
- Create all new ext/ replacement code in the parent repository:
  ```
  witsml-studio/           # Parent repo - put new code here
  ├── modern/              # Create this for new .NET 8 libraries
  │   ├── WitsmlClient/    # New SOAP client to replace ext/
  │   └── WitsmlFramework/ # Minimal utilities extracted from ext/
  ├── src/                 # Upgrade these files in-place
  └── ext/witsml/          # Submodule - READ ONLY reference
  ```

## 📋 Migration Steps

### Phase 1: Create New .NET 8 Libraries (ext/ rebuild)
**Location**: Create in `modern/` directory in parent repo

1. **Generate SOAP Client**
   - See `WITSML-PROTOCOL.md` for WSDL URLs and operations
   - Use `dotnet-svcutil` to generate from WSDL
   - Target namespace: `Energistics.DataAccess`

2. **Extract Minimal Utilities**
   - See `DEPENDENCY-MAP.md` Section C for exact files needed (~23 files)
   - Modernize during extraction (async/await, nullable references)

3. **Create Connection Layer**
   - Simple connection management without MEF
   - Basic authentication and compression support

### Phase 2: Upgrade src/ Projects (in-place)
**Location**: Modify existing files in `src/` directory

1. **Convert Project Files**
   - Update to SDK-style .csproj format
   - Target `net8.0` or `net8.0-windows`
   - Convert packages.config to PackageReference

2. **Update Dependencies**
   - Replace references to ext/witsml with modern/ libraries
   - Update NuGet packages to .NET 8 versions
   - See `DEPENDENCY-MAP.md` for package mappings

3. **Fix Breaking Changes**
   - Update WCF calls to use new SOAP client
   - Add async/await patterns
   - Handle any API changes

### Phase 3: Validation
**Environment**: Linux-based testing

1. **Unit Tests**
   - Test SOAP XML serialization/deserialization
   - Mock server responses
   - Connection handling

2. **Integration Tests**
   - Use mock WITSML server or test endpoints
   - Validate core operations work
   - See `VALIDATION_PATTERNS.md` for Linux testing approaches

3. **Behavioral Tests**
   - Verify UI displays data correctly
   - Test CRUD operations
   - Check error handling

## ⚠️ Common Issues & Solutions

### WSDL Generation Issues
- If `dotnet-svcutil` fails, try downloading WSDL locally first
- May need to manually fix generated code for nullable references
- Generated async methods might need wrapper for sync compatibility

### Missing Dependencies
- If ext/ code references something not in extraction list, check if it's really needed
- Many utilities can be replaced with modern .NET 8 built-ins
- When in doubt, check `AUDIT-REPORT.md` for identified gaps

### Linux Testing Limitations
- No WPF UI testing on Linux - focus on business logic
- Use mock servers for WITSML testing
- Can validate XML structure without real server

## 🎯 Success Criteria

You're done when:
1. ✅ All `modern/` libraries build on .NET 8
2. ✅ WitsmlBrowser plugin compiles against new libraries
3. ✅ Core SOAP operations work (GetCap, GetFromStore, AddToStore, UpdateInStore, DeleteFromStore)
4. ✅ Unit tests pass on Linux
5. ✅ Can connect to a WITSML server (mock or real)
6. ✅ XML serialization/deserialization works correctly

## 💡 Tips

- Start with Phase 1 (modern/ libraries) as it's independent
- Keep the new SOAP client minimal - only what Browser plugin needs
- Test early and often with mock data
- Don't try to migrate all 50K LOC from ext/ - just the ~2-3K needed
- When upgrading src/, preserve existing architecture patterns
- Use `BEHAVIOR_MAP.md` to understand what functionality to preserve

## 📝 Deliverables

Create these directories and projects:
```
modern/
├── WitsmlClient/
│   ├── WitsmlClient.csproj (.NET 8)
│   ├── Generated/          (SOAP client from WSDL)
│   └── [connection code]
├── WitsmlFramework/
│   ├── WitsmlFramework.csproj (.NET 8)
│   └── [minimal utilities from ext/]
└── WitsmlClient.Tests/
    ├── WitsmlClient.Tests.csproj (.NET 8)
    └── [unit tests]
```

Update these existing projects:
- `src/Desktop.Core/` → .NET 8
- `src/Desktop.Plugins.WitsmlBrowser/` → .NET 8
- `src/Desktop.UnitTest/` → .NET 8 (if time permits)

Good luck! Remember: ext/ is a liability to rebuild, src/ is an asset to upgrade.
