-   **Start:** Legacy **.NET Framework 4.5.2** WPF desktop app + `ext/` domain libraries (WITSML/ETP).
    
-   **ext/ complexity:** Git submodule with **legacy Framework dependencies** (~50K LOC) - SuperWebSocket (0.9.0.2), WebSocket4Net (0.15.2), Apache.Avro (1.7.7.2), System.Web.Services, MEF.
    -   ^so legacy that it makes .NET 4 look modern 😭
    
-   **Migration:** **Browser-focused approach** - migrate only WITSML Browser plugin to **.NET 8**, skip ETP/WebSocket complexity.
    
-   **Key insight:** Extract minimal SOAP client (~2-3K LOC) instead of full `ext/` migration (~50K LOC). 90%+ effort reduction.
    
-   **Development:** **Mac/Linux only** - no Windows dependencies, behavioral validation instead of golden artifacts.
    
-   **End state:** WITSML Browser runs cross-platform on **.NET 8**; other plugins remain Framework-only until future phases.
    
-   **Future migrations:** ETP Browser, Data Replay, Object Inspector plugins will follow similar selective modernization approach.