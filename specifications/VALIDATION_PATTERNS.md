# WITSML Studio Validation Patterns for .NET 8 Migration

## Purpose
This document outlines practical validation patterns you can use during the .NET 8 migration without needing Windows or a WITSML server.

## Core Operations to Validate

### 1. WITSML SOAP Operations (Primary Focus)
The app primarily does these 8 operations:
- **GetCap**: Query server capabilities
- **GetFromStore**: Retrieve data (wells, wellbores, logs, trajectories)
- **AddToStore**: Create new objects
- **UpdateInStore**: Modify existing objects
- **DeleteFromStore**: Remove objects
- **GetBaseMsg**: Get error messages
- **GetVersion**: Check server version
- **Subscribe/Unsubscribe**: Data change notifications

### 2. Key Data Objects
- **Wells**: Top-level container for all drilling data
- **Wellbores**: Physical holes drilled (multiple per well)
- **Logs**: Time/depth-based curve data (drilling parameters, geology)
- **Trajectories**: 3D well path coordinates
- **Messages**: Operational messages/reports

### 3. ETP Protocol (Streaming)
- Real-time data channels
- WebSocket-based protocol
- Binary message format (Apache Avro)

## Validation Strategy for Mac/Linux

### Phase 1: Mock Console Testing (What We Built)
```bash
cd validation/MockWitsmlConsole
dotnet run
```
This gives you hands-on experience with:
- What each WITSML operation does
- Expected request/response formats
- Data structures and relationships

### Phase 2: Golden Artifact Capture
When you get Windows CI running:
1. Capture actual SOAP XML requests/responses
2. Save ETP message streams
3. Store as test fixtures in `/tests/golden/`

### Phase 3: Cross-Platform Validation Harness
```csharp
// Example validation test structure
public class WitsmlOperationValidator
{
    [Test]
    public void ValidateGetFromStore_Wells()
    {
        // Load golden artifact
        var goldenRequest = LoadGoldenArtifact("getfromstore_wells_request.xml");
        var goldenResponse = LoadGoldenArtifact("getfromstore_wells_response.xml");
        
        // Execute through new .NET 8 implementation
        var actualResponse = ModernWitsmlClient.GetFromStore("well", goldenRequest);
        
        // Compare (allowing for acceptable differences)
        AssertXmlEquivalent(goldenResponse, actualResponse, 
            ignorePaths: new[] { "//timestamp", "//serverVersion" });
    }
}
```

### Phase 4: Behavioral Contracts
Instead of mocking servers, define behavioral contracts:

```csharp
public interface IWitsmlContract
{
    // Core behaviors to preserve during migration
    bool CanParseWitsmlObject(string xml, string objectType);
    bool ValidatesRequiredFields(string objectType, Dictionary<string, object> fields);
    bool HandlesPartialUpdates(string before, string update, string expectedAfter);
    bool PreservesUidReferences(string parentUid, string childUid);
}
```

## Practical Validation Checklist

### Without Server Access
- [ ] XML parsing/generation matches schema
- [ ] Object relationships maintained (well->wellbore->log)
- [ ] UID generation and referential integrity
- [ ] Options parsing (returnElements, maxReturnNodes, etc.)
- [ ] Error code mappings
- [ ] Compression/decompression logic
- [ ] Query template generation

### With Mock Data
- [ ] CRUD operations follow WITSML patterns
- [ ] Partial updates work correctly
- [ ] Cascading deletes handle relationships
- [ ] Data filtering and projections
- [ ] Growing object updates (logs, trajectories)

### With Golden Artifacts (from Windows CI)
- [ ] Request XML structure matches
- [ ] Response parsing handles all fields
- [ ] Namespace handling correct
- [ ] Special characters escaped properly
- [ ] Large dataset handling
- [ ] Streaming data protocols

## Static Analysis Approach

Since we can't run the current Windows-only app, use static analysis to understand behavior:

1. **Code Tracing**: Follow operation flows through `MainViewModel.cs` and related files
2. **XML Pattern Analysis**: Examine request/response structures in the codebase
3. **Behavioral Documentation**: Use `BEHAVIOR_MAP.md` to understand what each operation does
4. **Interface Extraction**: Identify contracts that need preservation during migration

This approach helps you:
- Understand what to validate post-migration
- Build test cases before the migration  
- Create validation harnesses that work cross-platform

## Next Steps

1. Run the mock console to get familiar with operations
2. Identify the most critical operations for your use case
3. Build validation tests using the patterns above
4. Create abstraction interfaces (as suggested in high-level-migration.md)
5. Implement golden artifact capture when Windows CI is ready

## Key Insight
The core value of WITSML Studio is in:
- **Protocol correctness** (SOAP/ETP message formats)
- **Data model integrity** (WITSML schema compliance)
- **Operation semantics** (how CRUD operations behave)

Focus validation on these rather than UI behavior.
