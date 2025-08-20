-   **Start:** Legacy **.NET Framework 4.5.2** WPF desktop app + `ext/` domain libraries (WITSML/ETP).
    
-   **Step 1:** Upgrade to **.NET Framework 4.8** (minor issues, mostly smooth).
    
-   **Step 2:** Migrate to **modern .NET 8** → major breaking changes, missing test coverage, Framework-only dependencies.
    
-   **Key challenge:** Rewrite/replace `ext/` libs (Energistics DevKit, SOAP/ETP) and validate behavior with golden artifacts.
    
-   **End state:** Core code runs cross-platform on **.NET 8**; WPF host continues as **Windows-only .NET 8 UI**; domain libs modernized or sidecar’d.