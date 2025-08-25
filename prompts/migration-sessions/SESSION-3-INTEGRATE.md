# Session 3: Replace Stubs with Real Implementation

## 📍 Test Projects Overview
**Remember**: Two groups of test projects validate our work:
1. **modern-ext/WitsmlClient.Tests** - Tests the new SOAP client implementation
2. **src/Desktop.IntegrationTest & Desktop.UnitTest** - Tests Desktop.Core + WitsmlBrowser
   - Already upgraded to .NET 8 in Session 2
   - With real implementations, more tests should pass
   - Use these to validate that real SOAP operations work

**Testing Philosophy**: Write quick sanity check tests as needed! Test coverage is sparse, so create simple tests to verify your work. For example: write a test that calls GetCapabilities on the real SOAP client, or create a test that confirms WitsmlBrowser can load with the real client. These quick tests are crucial for validating that real implementations actually work.

**Note**: Comprehensive test coverage is NOT the goal - focus on getting key integration tests passing. Additional tests are encouraged but optional.

## 📍 Context: Still Focused on WitsmlBrowser + Desktop.Core
**Primary Focus**: Keep WitsmlBrowser and Desktop.Core working while replacing stub implementations with real SOAP functionality

Now that Desktop.Core + WitsmlBrowser compile with stubs, we need to make them actually functional by:
1. Generating real SOAP client from WSDL
2. Replacing stub WitsmlClient with real implementation
3. Extracting ONLY the minimal code WitsmlBrowser actually uses from ext/witsml

The `ext/` submodule contains much older legacy code (50,000+ LOC) with obsolete patterns and dependencies from the Framework 4.5.2 era - **so legacy that it makes .NET Framework 4.5.2 look modern** 😭. We're **completely re-writing** these dependencies in `modern-ext/` rather than trying to port the legacy code. This clean-slate approach avoids:
- Legacy WebSocket libraries (SuperWebSocket 0.9)
- Obsolete serialization (Apache.Avro 1.7.7)
- Framework-only APIs (System.Web.Services)
- 50,000+ lines of unnecessary code

## 📍 Strategy: Progressive Implementation - Make WitsmlBrowser Work
**Goal**: Replace stub implementations with real SOAP client while keeping Desktop.Core + WitsmlBrowser compilation intact

## Phase 3A: Generate SOAP Client from WSDL (30 min)

Now that everything compiles with stubs, add real SOAP:

```bash
cd modern-ext/WitsmlClient

# Install SOAP generation tool
dotnet tool install --global dotnet-svcutil

# Generate from WSDL  
dotnet-svcutil https://schemas.energistics.org/wsdl/WMLS.WSDL \
  --namespace "*.Energistics.DataAccess" \
  --outputDir ./Generated \
  --serializer XmlSerializer

# Add System.ServiceModel packages
dotnet add package System.ServiceModel.Http
dotnet add package System.ServiceModel.Primitives

# Build to verify generation worked
dotnet build
```

✅ **Validation Point 1**: SOAP client generated and compiles

## Phase 3B: Replace WitsmlClient Stub with Real Implementation (30 min)

Update the stub with actual SOAP calls:

**modern-ext/WitsmlClient/WitsmlClient.cs**:
```csharp
namespace WitsmlClient;
using WitsmlFramework;
using System.ServiceModel;
using Energistics.DataAccess;

public class WitsmlClient : IWitsmlClient
{
    private readonly Connection _connection;
    private readonly WMLS_PortTypeClient _soapClient;
    
    public WitsmlClient(Connection connection)
    {
        _connection = connection;
        
        var binding = new BasicHttpBinding
        {
            MaxReceivedMessageSize = int.MaxValue,
            Security = new BasicHttpSecurity
            {
                Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.Basic
                }
            }
        };
        
        var endpoint = new EndpointAddress(connection.Uri);
        _soapClient = new WMLS_PortTypeClient(binding, endpoint);
        
        if (!string.IsNullOrEmpty(connection.Username))
        {
            _soapClient.ClientCredentials.UserName.UserName = connection.Username;
            _soapClient.ClientCredentials.UserName.Password = connection.Password;
        }
    }
    
    public async Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities)
    {
        var response = await _soapClient.WMLS_GetFromStoreAsync(
            objectType, query, options, capabilities);
        
        if (response.Result != 0)
        {
            throw new Exception($"WITSML Error {response.Result}: {response.SuppMsgOut}");
        }
        
        return response.XMLout;
    }
    
    // Implement other methods similarly...
}
```

```bash
# Test compilation with real SOAP client
cd modern-ext
dotnet build
```

✅ **Validation Point 2**: Real SOAP implementation compiles

## Phase 3C: Extract Minimal Required Code from ext/ (1 hour)

**Priority extraction** (based on actual usage in WitsmlBrowser):

1. **ErrorCodes.cs** - Replace stub with real constants:
```bash
cp ext/witsml/src/Core/Data/ErrorCodes.cs modern-ext/WitsmlFramework/
# Edit to remove MEF attributes and update namespace
```

2. **OptionsIn.cs** - Query options builder:
```bash
cp ext/witsml/src/Core/OptionsIn.cs modern-ext/WitsmlFramework/
# Simplify for .NET 8
```

3. **Functions.cs** - WITSML function enum:
```bash
cp ext/witsml/src/Core/Functions.cs modern-ext/WitsmlFramework/
# Update namespace
```

4. **XmlUtil.cs** - XML helper methods:
```bash
# Create simplified version with just what's needed
cat > modern-ext/WitsmlFramework/XmlUtil.cs << 'EOF'
namespace WitsmlFramework;
using System.Xml.Linq;

public static class XmlUtil
{
    public static string PrettyPrint(string xml)
    {
        try 
        {
            return XDocument.Parse(xml).ToString();
        }
        catch 
        {
            return xml;
        }
    }
}
EOF
```

```bash
# After each extraction, verify still compiles
cd src/Desktop.Plugins.WitsmlBrowser
dotnet build
```

✅ **Validation Point 3**: Extracted files integrated successfully

## Phase 3C.5: Run src/ Tests with Real Implementation (20 min)

Now that we have real implementations, run the existing tests:

```bash
# Run Desktop.Core tests 
cd src/Desktop.UnitTest
dotnet test --logger "console;verbosity=normal"
# More tests should pass now compared to Session 2

# Run WitsmlBrowser integration tests
cd ../Desktop.IntegrationTest
dotnet test --filter "FullyQualifiedName~WitsmlBrowser" --logger "console;verbosity=normal"
# Connection tests might pass if mock server available
# ViewModel tests should work better with real client
```

✅ **Validation Point 3.5**: More src/ tests pass with real implementations

## Phase 3D: Create Integration Test (30 min)

**modern-ext/WitsmlClient.Tests/IntegrationTests.cs**:
```csharp
using NUnit.Framework;
using WitsmlClient;
using WitsmlFramework;

namespace WitsmlClient.Tests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public void CanCreateWitsmlClient()
    {
        var connection = new Connection
        {
            Name = "Test",
            Uri = "http://localhost/witsml",
            Username = "test",
            Password = "test"
        };
        
        var client = new WitsmlClient(connection);
        Assert.IsNotNull(client);
    }
    
    [Test]
    [Category("RequiresServer")]
    public async Task CanGetCapabilities()
    {
        // This test requires a real WITSML server
        // Mark with category so it can be skipped in CI
        var connection = new Connection
        {
            Uri = Environment.GetEnvironmentVariable("WITSML_URL") ?? "http://localhost/witsml"
        };
        
        var client = new WitsmlClient(connection);
        var caps = await client.GetCapabilitiesAsync();
        
        Assert.That(caps, Does.Contain("<capServer>"));
    }
}
```

```bash
# Run tests (skip integration tests if no server)
cd modern-ext/WitsmlClient.Tests
dotnet test --filter "Category!=RequiresServer"
```

✅ **Validation Point 4**: Basic tests pass

## Phase 3E: Verify Functionality Beyond Compilation (45 min)

**Testing Strategy - Progressive Validation**:

1. **Mock Server Testing** (no external dependencies):
```bash
# Create a mock WITSML server for testing
cd modern-ext/WitsmlClient.Tests
dotnet add package WireMock.Net

# Add mock server test to verify SOAP calls are formatted correctly
```

2. **Console Test App** (manual validation):
```bash
# Create simple console app to test real operations
cd modern-ext
dotnet new console -n WitsmlConsoleTest
cd WitsmlConsoleTest
dotnet add reference ../WitsmlClient/WitsmlClient.csproj
dotnet add reference ../WitsmlFramework/WitsmlFramework.csproj

# Test GetCap, GetFromStore operations manually
dotnet run -- --url "http://test.server/witsml" --operation GetCap
```

3. **WPF Integration Test** (end-to-end):
```bash
# Run WitsmlBrowser plugin in test harness
cd src/Desktop.Plugins.WitsmlBrowser
dotnet run

# Verify:
# - UI loads without crashes
# - Can enter connection details
# - SOAP requests are sent (use Fiddler/Wireshark to verify)
# - Response XML is displayed in UI
```

4. **Against Real WITSML Server** (if available):
- Connect to public test server or Docker container
- Verify GetCap returns valid capabilities
- Verify GetFromStore retrieves wells/wellbores
- Check error handling for invalid queries

✅ **Validation Point 5**: Real SOAP operations work (not just compilation)

## Success Criteria for Session 3 (WitsmlBrowser + Desktop.Core Focus)
1. ✅ SOAP client generated and integrated for WitsmlBrowser
2. ✅ WitsmlClient has real implementation (not stubs) 
3. ✅ ONLY the 3-5 essential files that WitsmlBrowser uses extracted from ext/witsml
4. ✅ Desktop.Core + WitsmlBrowser still compile with real implementations
5. ✅ src/Desktop.IntegrationTest & Desktop.UnitTest pass more tests than Session 2
6. ✅ modern-ext/WitsmlClient.Tests basic tests pass
7. ✅ Mock server tests verify SOAP formatting for WitsmlBrowser operations
8. ✅ Console app can execute real WITSML operations that WitsmlBrowser needs
9. ✅ WitsmlBrowser plugin loads and sends SOAP requests

## What This Session Does NOT Do
- ❌ Fix other plugins (ETP Browser, Data Replay, etc.)
- ❌ Make the full Desktop app work (just Desktop.Core)
- ❌ Extract utilities that WitsmlBrowser doesn't use
- ❌ Implement WITSML operations beyond what WitsmlBrowser needs
- ❌ Handle edge cases unrelated to core WitsmlBrowser functionality

**Next**: Session 4 will validate end-to-end functionality and fix Desktop main app
