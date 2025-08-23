# WITSML SOAP Protocol Details

## Protocol Overview
WITSML (Wellsite Information Transfer Standard Markup Language) uses SOAP 1.1 over HTTP/HTTPS for client-server communication in the oil & gas industry.

## WSDL Sources

### Official WSDL Sources

#### WITSML 1.4.1.1 (Primary)
- **WSDL**: https://schemas.energistics.org/Energistics/Schemas/v1.4.1/wsdl/WMLS.WSDL
- **XSD**: https://schemas.energistics.org/Energistics/Schemas/v1.4.1/xsd_schemas/
- **Namespace**: `http://www.witsml.org/wsdl/141`

#### WITSML 1.3.1.1 (Legacy Support)
- **WSDL**: https://schemas.energistics.org/Energistics/Schemas/v1.3.1/wsdl/WMLS.WSDL
- **XSD**: https://schemas.energistics.org/Energistics/Schemas/v1.3.1/xsd_schemas/
- **Namespace**: `http://www.witsml.org/wsdl/131`

### GitHub Mirrors
```bash
# Energistics public repository
https://github.com/Energistics/WITSML-Standards

# Contains XSD schemas and documentation
https://github.com/Energistics/WITSML-Standards/tree/master/energyml/data/witsml/v1.4.1.1
```

## Core SOAP Operations

### 1. WMLS_GetCap
**Purpose:** Query server capabilities and supported objects
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <WMLS_GetCap xmlns="http://www.witsml.org/message/120">
      <OptionsIn>dataVersion=1.4.1.1</OptionsIn>
    </WMLS_GetCap>
  </soap:Body>
</soap:Envelope>
```

### 2. WMLS_GetFromStore
**Purpose:** Retrieve data objects
```xml
<soap:Body>
  <WMLS_GetFromStore>
    <WMLtypeIn>well</WMLtypeIn>
    <QueryIn><![CDATA[
      <wells xmlns="http://www.witsml.org/schemas/1series" version="1.4.1.1">
        <well uid="">
          <name/>
        </well>
      </wells>
    ]]></QueryIn>
    <OptionsIn>returnElements=id-only</OptionsIn>
    <CapabilitiesIn></CapabilitiesIn>
  </WMLS_GetFromStore>
</soap:Body>
```

### 3. WMLS_AddToStore
**Purpose:** Create new objects
```xml
<soap:Body>
  <WMLS_AddToStore>
    <WMLtypeIn>well</WMLtypeIn>
    <XMLin><![CDATA[
      <wells xmlns="http://www.witsml.org/schemas/1series">
        <well uid="W-123">
          <name>Test Well</name>
          <country>USA</country>
        </well>
      </wells>
    ]]></XMLin>
    <OptionsIn></OptionsIn>
    <CapabilitiesIn></CapabilitiesIn>
  </WMLS_AddToStore>
</soap:Body>
```

## Authentication Methods

### 1. Basic Authentication
```http
Authorization: Basic base64(username:password)
```

### 2. Token Authentication
```http
Authorization: Bearer <token>
```

### 3. Certificate Authentication
- Client certificates for mutual TLS
- Server validates certificate chain

## Compression Support

### GZIP Request/Response
```http
# Request compression
Content-Encoding: gzip
Accept-Encoding: gzip, deflate

# Response compression
Content-Encoding: gzip
```

## OptionsIn Parameters

### GetFromStore Options
```
returnElements=all              # Return all elements
returnElements=id-only          # Return only UIDs
returnElements=header-only      # Return header data
returnElements=data-only        # Return only data arrays
returnElements=station-location-only  # Trajectory specific
returnElements=latest-change-only     # Latest changes
maxReturnNodes=1000            # Limit returned nodes
requestLatestValues=10         # Latest n values for logs
```

### AddToStore/UpdateInStore Options
```
compressionMethod=gzip         # Compress data
cascadedDelete=true           # Delete child objects
```

## Return Codes

### Success Codes
```
1    = Success (one or more objects returned)
2    = Partial success (growing objects)
```

### Warning Codes
```
1001 = Data returned but with warnings
1002 = No data matching query
```

### Error Codes
```
-401 = Missing required input parameter
-402 = Invalid token
-403 = Invalid unit of measure
-404 = Invalid state/province code
-405 = Invalid bottom hole location
-406 = Missing required element attribute
-407 = Invalid XML structure
-408 = Invalid UID
-409 = Invalid input template
-410 = Invalid max/min range
-411 = Invalid measure class
-412 = Invalid unit of measure for class
-413 = Invalid capitalization
-414 = Required element not allowed to be empty
-415 = Required child element missing
-416 = Invalid query template
-417 = Node limit exceeded
-418 = Invalid date format
-419 = API version not supported
-420 = Invalid data object
-421 = Element not expected
-422 = Recurring element limit exceeded
-423 = Query uses unrecognized element
-424 = Query requests selection not supported
-425 = Input template requests non-conforming constraints
-426 = User not authorized for operation
-427 = Operation not supported by server
-428 = Update template has empty required node
-429 = Cannot delete required node
-430 = Cascaded delete not allowed
```

## XML Namespaces

### WITSML 1.4.1.1
```xml
xmlns="http://www.witsml.org/schemas/1series"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
xsi:schemaLocation="http://www.witsml.org/schemas/1series ../xsd_schemas/obj_well.xsd"
version="1.4.1.1"
```

### WITSML 1.3.1.1
```xml
xmlns="http://www.witsml.org/schemas/131"
version="1.3.1.1"
```

## Common Data Objects

### Hierarchy
```
well
├── wellbore
│   ├── log (time/depth)
│   ├── trajectory
│   ├── mudLog
│   ├── opsReport
│   └── rig
├── bhaRun
├── cementJob
├── convCore
├── fluidsReport
├── formationMarker
├── message
├── risk
├── surveyProgram
├── target
├── tubular
├── wbGeometry
└── wellLog
```

## Testing Servers

### Public Test Servers
```
# Note: These may require registration
http://test.witsml.org/witsml/services
https://witsml.wellstrat.com/witsml/services
```

### Mock Server Options
```
# SoapUI Mock Service
# WireMock with SOAP stubs
# Custom ASP.NET Core mock
```

## .NET 8 Implementation Notes

### System.ServiceModel Packages
```xml
<PackageReference Include="System.ServiceModel.Http" Version="8.0.0" />
<PackageReference Include="System.ServiceModel.Primitives" Version="8.0.0" />
<PackageReference Include="System.ServiceModel.Security" Version="8.0.0" />
```

### Key Differences from .NET Framework
1. No automatic proxy generation from Add Service Reference
2. Use `dotnet-svcutil` tool instead
3. Manual binding configuration in code
4. Async-only operations by default
5. No app.config/web.config support

### SOAP Message Inspection (Debugging)
```csharp
public class SoapMessageInspector : IClientMessageInspector
{
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
        Console.WriteLine($"Response: {reply}");
    }

    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
        Console.WriteLine($"Request: {request}");
        return null;
    }
}
```

## Validation Requirements

### Must Support
- [x] SOAP 1.1 protocol
- [x] HTTP and HTTPS endpoints
- [x] Basic authentication
- [x] GZIP compression
- [x] Large messages (>10MB)
- [x] Timeout configuration
- [x] Certificate validation options

### Nice to Have
- [ ] SOAP 1.2 support
- [ ] Token authentication
- [ ] Request/response logging
- [ ] Performance metrics
- [ ] Connection pooling
- [ ] Retry logic
