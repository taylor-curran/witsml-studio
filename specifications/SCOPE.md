# WITSML Studio .NET 8 Migration - Project Scope

## 🔑 Migration Philosophy

### Two Distinct Strategies
- **ext/ Code (50K+ LOC):** Complete rebuild with new scaffold 🏗️
  - Legacy Framework 4.5.2 with obsolete dependencies
  - Generate fresh SOAP client from WSDL
  - Build new validation system from scratch
  
- **src/ Code:** Traditional version upgrade ⬆️
  - Modern WPF architecture worth preserving
  - Update project files and dependencies
  - Maintain existing structure and patterns

## Current State
-   **Start:** Legacy WPF desktop app + `ext/` domain libraries (WITSML/ETP).
    
-   **ext/ complexity:** Git submodule with legacy dependencies (~50K LOC) - SuperWebSocket (0.9.0.2), WebSocket4Net (0.15.2), Apache.Avro (1.7.7.2), System.Web.Services, MEF.
    -   ^so legacy that it makes .NET look modern 😭
    
-   **Migration:** Browser-focused approach - migrate only WITSML Browser plugin to .NET 8, skip ETP/WebSocket complexity.
    
-   **Key insight:** Extract minimal SOAP client (~2-3K LOC) instead of full `ext/` migration (~50K LOC). 90%+ effort reduction.
    
-   **Development:** Mac/Linux only - no Windows dependencies, behavioral validation instead of golden artifacts.
    
-   **End state:** WITSML Browser runs cross-platform on .NET 8; other plugins remain Framework-only until future phases.
    
-   **Future migrations:** ETP Browser, Data Replay, Object Inspector plugins will follow similar selective modernization approach.