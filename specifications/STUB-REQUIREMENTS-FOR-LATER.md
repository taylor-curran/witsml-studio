# WITSML Browser Stub Requirements

This document defines the minimal set of stubs needed for WitsmlBrowser plugin migration to .NET 8.

This is also where we will find the pieces of code to migrate after WitsmlBrowser is shown to work.

## Required Stubs for WitsmlBrowser

### In WitsmlFramework/
- `Connection.cs` - Connection info class
- `IWitsmlClient.cs` - WITSML operations interface  
- `ErrorCodes.cs` - Error constants
- `Functions.cs` - WITSML function enum
- `OptionsIn.cs` - Query options

### In Desktop.Core/ (stub these)
- `IRuntimeService.cs` - But only WITSML methods, stub ETP methods
- `IPluginViewModel.cs` - Plugin interface
- `ISoapMessageHandler.cs` - SOAP handling

## NOT Needed (skip these stubs)

### ❌ ETP-related:
- `IEtpClient`
- `IEtpSession` 
- `WebSocketClient`
- Anything with "Etp" in the name

### ❌ Store components:
- `IDataStore`
- `IMongoDbStore`
- Store providers

### ❌ Other plugins:
- Data Replay interfaces
- Object Inspector interfaces

## Implementation Strategy

1. **Reactive approach**: Only create stubs when compilation errors require them
2. **Minimal scope**: Focus exclusively on WitsmlBrowser dependencies
3. **Progressive replacement**: Session 3 will replace stubs with real implementations
4. **Selective stubbing**: For mixed interfaces (like `IRuntimeService`), stub ETP methods but implement WITSML methods

## Validation

Each stub should:
- Allow compilation to succeed
- Provide minimal interface contract
- Throw `NotImplementedException` for unimplemented methods
- Include clear comments indicating it's a temporary stub

This keeps the migration focused and prevents scope creep into unnecessary components.
