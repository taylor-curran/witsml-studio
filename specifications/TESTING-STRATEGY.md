# Testing Strategy for .NET 8 Migration

## Test Organization

### 1. Unit Tests (`*.Tests`)
**Purpose:** Test individual components in isolation
- Data model serialization/deserialization
- SOAP envelope construction
- XML parsing logic
- Connection string validation

**Example:** `WitsmlClient.Core.Tests`
```csharp
[Test]
public void Well_SerializesToCorrectXml()
{
    var well = new Well { Name = "Test Well" };
    var xml = XmlSerializer.Serialize(well);
    Assert.Contains("<name>Test Well</name>", xml);
}
```

### 2. Integration Tests (`*.IntegrationTests`)
**Purpose:** Test component interactions
- SOAP client with mock server
- Authentication flow
- Connection management
- Error handling

**Example:** `WitsmlClient.IntegrationTests`
```csharp
[Test]
public async Task GetFromStore_ReturnsWellData()
{
    using var mockServer = new MockWitsmlServer();
    var client = new WitsmlClient(mockServer.Url);
    var result = await client.GetFromStore("Well", "uid='123'");
    Assert.NotNull(result);
}
```

### 3. Behavioral Tests (`*.BehavioralTests`)
**Purpose:** Validate protocol compliance
- SOAP XML format matches WITSML standard
- Request/response patterns
- Error codes and messages
- Performance characteristics

**Example:** `WitsmlClient.BehavioralTests`
```csharp
[Test]
public void GetCapabilities_ProducesValidWitsmlRequest()
{
    var request = WitsmlClient.BuildGetCapRequest();
    // Compare structure, not exact bytes
    AssertXmlStructureMatches(request, WitsmlStandard.GetCapTemplate);
}
```

## Test Execution Strategy

### Phase 1: Pre-Migration (Now)
- **Capture behavior** from existing code on Windows (if possible)
- **Document expected outputs** (SOAP envelopes, response formats)
- **Create test fixtures** with sample data

### Phase 2: During Migration (Mac/Linux)
- **Write new tests** for .NET 8 code
- **Use captured behavior** as reference
- **Mock external dependencies**

### Phase 3: Post-Migration Validation
- **Compare outputs** between old and new implementations
- **Test against real WITSML servers**
- **Performance benchmarking**

## Key Testing Areas

### SOAP Protocol Tests
- Envelope structure
- Namespace handling
- Authentication headers
- Compression support

### Data Model Tests
- XML serialization
- Required vs optional fields
- Data type conversions
- Special characters handling

### Connection Tests
- URI validation
- Proxy support
- Timeout handling
- Retry logic

## Test Data Management

### Mock Responses
Store in `tests/TestData/`:
- `GetCapabilities_Response.xml`
- `GetFromStore_Well_Response.xml`
- `Error_InvalidQuery_Response.xml`

### Test Fixtures
- Connection strings
- Sample queries
- Expected results

## Validation Without Windows

Since we can't run original tests on Mac/Linux:

1. **Use WITSML specification** as source of truth
2. **Create contract tests** based on protocol docs
3. **Mock server responses** based on captured data
4. **Test against public WITSML servers** when available

## Success Criteria

✅ All SOAP operations produce valid WITSML XML
✅ Authentication works with test servers
✅ Error handling matches WITSML standards
✅ Performance within 10% of original
