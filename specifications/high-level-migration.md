# A. Operating model (important constraints)

-   **Windows-only:** current WPF/.NET **4.8** app and any **Framework-only** deps in `ext/`.
    
-   **Linux-friendly (Devin can run):** any code you move to **.NET 5/6/8+** (console libs, services, tests, tooling).
    
-   **Fact of life:** You **cannot** modernize `src/` meaningfully without a plan for `ext/`. Treat `ext/` as **first-class scope**.
    

---

# B. Tracks & checkpoints

We’ll run **two tracks in parallel** with explicit sync points:

1.  **Track W (Windows Baseline & UI)**  
    Baseline capture + WPF host validation on **Windows runner/VM**.
    
2.  **Track L (Linux/Devin Modernization)**  
    All new cross-platform work (SDK-style, .NET 8 libs, adapters, tests, tooling) in **Devin**.
    

**Sync points** ensure each increment is verifiable.

---

# C. Step-by-step plan (with validation at each stage)

## 0) Baseline capture (Track W) — start here

**Goal:** Freeze how the app behaves today on **.NET 4.8**.

-   **Actions**
    
    -   Build & run on a **Windows CI runner/VM**.
        
    -   Capture **golden artifacts**: WITSML SOAP XML (req/resp), ETP message streams, minimal UI smoke videos, runtime configs.
        
    -   Store under `/tests/golden/**`.
        
-   **Validation gate**
    
    -   Baseline build artifact + golden files published by CI.
        
    -   One-pager “Baseline Report” checked in.
        

> Devin won’t run this UI; it just **triggers** the Windows job and consumes the artifacts.

---

## 1) Project plumbing (Track L) — **src** first, **ext** audit in parallel

**Goal:** Get solution building cross-platform where possible, without behavior changes.

-   **Actions (src/)**
    
    -   Convert to **SDK-style**; set `<UseWPF>true</UseWPF>` where needed.
        
    -   **Multi-target** key projects: `net48;net8.0-windows` (WPF stays Windows-only, but core libs can add `net8.0`).
        
    -   Migrate `packages.config` → **PackageReference**.
        
    -   Keep Caliburn/MEF; only bump versions to compile.
        
-   **Actions (ext/ audit)**
    
    -   For each domain lib: note target frameworks, API surface, transitive deps.
        
    -   Classify: **Portable now** / **Shim possible** / **Framework-only (sidecar)**.
        
-   **Validation gate**
    
    -   CI: net48 **and** net8.0-windows build for `src` projects; Linux CI builds any newly cross-targeted libs.
        
    -   `ext/` audit doc with per-lib strategy approved.
        

---

## 2) Define protocol boundaries (Track L)

**Goal:** Decouple plugins from concrete `ext/` so we can run cross-platform logic.

-   **Actions**
    
    -   Introduce **abstractions** (e.g., `IWitsmlClient`, `IEtpClient`) in a new **`Abstractions`** project targeting **netstandard2.0**.
        
    -   Refactor `src` plugins to depend on interfaces only.
        
    -   Add **two impls** stubs: `LegacyAdapter` (calls current `ext/`) and `ModernAdapter` (future .NET 8).
        
-   **Validation gate**
    
    -   Builds pass; plugins compile against interfaces.
        
    -   Manual smoke (Windows): app launches and interacts via **LegacyAdapter** as before.
        

---

## 3) Unblock with a sidecar (Track W + L) — only where `ext/` is Framework-only

**Goal:** Run legacy `ext/` out-of-process so the host and most logic can move forward.

-   **Actions**
    
    -   Build a **.NET Framework sidecar** exposing RPC (gRPC/HTTP) for the required ETP/WITSML calls.
        
    -   Implement **`ModernAdapter`** in net8 that talks to the sidecar.
        
    -   Keep `LegacyAdapter` for net48 builds (reduces risk).
        
-   **Validation gate**
    
    -   **Replay the golden artifacts** through the sidecar path: SOAP/ETP outputs must match baseline (or diffs are documented).
        
    -   Operator checklist (Windows): connect → run queries → see expected data.
        

> Devin develops the **ModernAdapter** and test harnesses on Linux; Windows runner executes UI + sidecar for validation.

---

## 4) Native modernization (Track L) — replace sidecar incrementally

**Goal:** Remove sidecar gradually by implementing real **.NET 8** protocol clients.

-   **Actions (SOAP/WITSML)**
    
    -   Regenerate clients with supported bindings; normalize requests to match golden XML.
        
-   **Actions (ETP/WebSockets)**
    
    -   Choose WebSocket stack; implement handshake, framing, reconnect.
        
-   **Scope discipline**
    
    -   Replace **one operation/flow at a time** (start with most used).
        
-   **Validation gate**
    
    -   **Snapshot tests**: golden XML matches (with documented exceptions).
        
    -   **ETP replay tests**: message sequence equality.
        
    -   Windows smoke still OK through the WPF host.
        

---

## 5) Config & logging consolidation (Track L)

**Goal:** Stabilize runtime differences while keeping behavior.

-   **Actions**
    
    -   Introduce `appsettings.json` + Microsoft.Extensions.Configuration.
        
    -   Keep log4net; optionally bridge to MEL.
        
    -   Do not break existing config keys; deprecate gradually.
        
-   **Validation gate**
    
    -   Flip a setting in `appsettings.json`; observe effect on Linux & Windows runs.
        
    -   Logs flow in both old and new sinks.
        

---

## 6) UI dependencies & polish (Track W)

**Goal:** Ensure WPF runs cleanly on .NET 8.

-   **Actions**
    
    -   Update AvalonEdit/Xceed/AvalonDock (or swap forks) to net6+/net8-compatible versions.
        
    -   Turn on Nullable + analyzers for crashy hotspots.
        
-   **Validation gate**
    
    -   Scripted **UI smoke**: open each plugin, perform a standard action; no runtime errors.
        

---

## 7) Build up tests while migrating (both tracks)

**Goal:** Confidence without boiling the ocean.

-   **Actions (priority order)**
    
    1.  **Characterization tests** for top WITSML ops (golden XML snapshots).
        
    2.  **ETP replay harness** (record → replay → compare).
        
    3.  **Critical ViewModel tests** in plugins (EtpBrowser, DataReplay).
        
    4.  **Desktop.Core**: tests for `DataGridViewModel`, `WitsmlTreeViewModel`, connection flow.
        
    5.  Light **UI smoke** automation (or a manual script + video if tooling is heavy).
        
-   **Validation gate**
    
    -   CI fails if snapshot/replay regress; “delete-to-green” changes must quarantine tests with owner/date.
        

---

# D. Repo & CI layout (so Devin thrives)

```bash
/src               # WPF host + plugins (multi-target where possible)
/ext               # domain libs (treat as first-class)
/adapters
  /Abstractions    # netstandard2.0
  /ModernAdapter   # net8.0 (calls sidecar now; native later)
/sidecar           # .NET Framework service (Windows-only transitional)
/tests
  /golden          # captured XML, ETP logs, sample configs, videos
  /snapshot        # request/response snapshot tests (runs on Linux)
  /replay          # ETP replay tests (Linux)
/tools/baseline    # PowerShell to build/run baseline and capture artifacts
```

**CI pipelines**

-   **linux:** build net8 libs; run snapshot/replay/unit tests (Devin’s lane).
    
-   **windows:** build net48 + net8-windows WPF; run baseline capture & UI smoke; run sidecar integration.
    

---

# E. What to do first (next 1–2 weeks)

1.  **Step 0**: Baseline capture on Windows; check in golden artifacts.
    
2.  **Step 1**: SDK-style + multi-target for `src` libs; migrate to PackageReference.
    
3.  **Step 1 (parallel)**: `ext/` audit + per-lib decision (portable/shim/sidecar).
    
4.  **Step 2**: Add `Abstractions`; refactor plugins to the interfaces.
    
5.  **Step 3 (if needed)**: Sidecar scaffold + `ModernAdapter` RPC calls.
    
6.  Start **snapshot/replay** harnesses so every change is measurable.
    

---

## TL;DR

-   **Start with `src/` plumbing** (so you can build/run on Linux where possible) and do an **`ext/` audit in parallel** (it’s part of the scope, not an afterthought).
    
-   Use a **Windows runner** for baseline & WPF; let **Devin handle all cross-platform modernization** work.
    
-   Decouple `src` from `ext` via **interfaces**, unblock with a **sidecar**, then **replace** with native .NET 8 protocol clients **incrementally**, proving each step against **golden artifacts**.
