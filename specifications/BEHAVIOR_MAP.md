# WITSML Studio Behavior Map (Static Analysis)

## What This App Actually Does (Based on Code Analysis)

### User Journey: "Get Well Data"

1. **User Opens App** → `Desktop/App.xaml.cs`
   - Loads WPF window
   - Initializes plugin system (MEF/Caliburn.Micro)
   - Shows connection dialog

2. **User Connects to Server** → `Desktop.Core/Connections/`
   - Enter WITSML server URL (e.g., `https://server.com/witsml/services`)
   - Provide credentials
   - Select WITSML version (1.3.1 or 1.4.1)
   - Creates `WITSMLWebServiceConnection` proxy

3. **User Clicks "Get Wells"** → `Desktop.Plugins.WitsmlBrowser/ViewModels/MainViewModel.cs:501`
   ```csharp
   returnCode = wmls.WMLS_GetFromStore(
       objectType: "well",
       xmlIn: "<wells xmlns='...' />",  // Query
       optionsIn: "returnElements=id-only",
       xmlOut: out xmlOut,  // Response
       suppMsgOut: out suppMsgOut  // Messages
   );
   ```

4. **Server Returns XML** → Response parsing
   ```xml
   <wells>
     <well uid="WELL-001">
       <name>Test Well Alpha</name>
       <field>North Field</field>
       <operator>Acme Oil</operator>
     </well>
   </wells>
   ```

5. **App Displays Tree View** → `Desktop.Core/ViewModels/WitsmlTreeViewModel.cs`
   - Parses XML into tree nodes
   - Shows hierarchical structure
   - Allows drill-down into wellbores, logs

### Core Code Paths to Validate

#### Path 1: SOAP Operations (`MainViewModel.cs:481-503`)
```
User Action → MainViewModel.ExecuteQuery() 
    → Create SOAP proxy
    → Compress request (if enabled)
    → Call WMLS_* function
    → Decompress response
    → Parse XML result
    → Update UI tree
```

#### Path 2: Data Tree Navigation (`WitsmlTreeViewModel.cs`)
```
XML Response → ParseXml()
    → Build TreeNode hierarchy
    → Lazy-load children on expand
    → Context menu for operations
    → Generate query templates
```

#### Path 3: Plugin Architecture (`Desktop.Core/Runtime/`)
```
App Start → MEF Container
    → Load plugins from assemblies
    → Inject IRuntimeService
    → Register IPluginViewModel
    → Show in tab interface
```

### Key Files for Understanding

| File | Purpose | What to Look For |
|------|---------|------------------|
| `MainViewModel.cs:470-526` | SOAP operation dispatch | How each WITSML function is called |
| `WitsmlTreeViewModel.cs` | Data display logic | How XML becomes UI tree |
| `WitsmlSettings.cs` | Configuration model | What options affect behavior |
| `RequestViewModel.cs` | Query building | How XML queries are constructed |
| `ResultViewModel.cs` | Response handling | How results are displayed |

### Validation Points (Without Running)

1. **XML Structure**
   - Request format matches WITSML schema
   - Response parsing handles all elements
   - Namespace management

2. **Operation Semantics**
   - GetFromStore with empty query = get all
   - UpdateInStore only updates provided fields
   - DeleteFromStore cascades if option set

3. **Error Handling**
   - Return codes: 1=success, 0=warning, -1=error
   - suppMsgOut contains details
   - Client-side validation before send

4. **Options Processing**
   - `returnElements`: all, id-only, header-only, data-only
   - `maxReturnNodes`: limits response size
   - `requestLatestValues`: for growing objects

### Manual "Execution" Exercise

Pick a function and trace through the code manually:

**Example: Get Wellbores for Well "ABC-123"**

1. Start at `MainViewModel.ExecuteQuery()`
2. Build XML: `<wellbores uidWell="ABC-123"/>`
3. Set options: `"returnElements=header-only"`
4. Follow to line 501: `WMLS_GetFromStore()`
5. Imagine response XML
6. Trace to `ProcessQueryResults()`
7. Follow to `WitsmlTreeViewModel.ParseXml()`
8. See how nodes are created

This gives you the "feel" without running anything.

### What Makes This App Complex

1. **Plugin Architecture**: Dynamic loading, dependency injection
2. **XML Processing**: Heavy LINQ to XML, namespace handling
3. **Async Operations**: Task-based async pattern throughout
4. **Data Binding**: WPF MVVM with Caliburn.Micro
5. **Protocol Variants**: SOAP vs ETP, compression options

### Suggested Validation Strategy

Since you can't run it:

1. **Extract interface contracts** from the code
2. **Document expected behaviors** (like above)
3. **Create test scenarios** based on code paths
4. **Build assertion library** for post-migration validation

This behavioral understanding from static analysis is your "sanity check" foundation.
