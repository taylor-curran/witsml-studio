# WITSML Studio — .NET 8 Migration

Minimal, pragmatic rewrite focused on migrating the legacy WPF/.NET Framework 4.5.2 app to .NET 8. The goal is to bring the WitsmlBrowser plugin online first, with a clean, cross‑platform core.

## Current scope
- WitsmlBrowser (SOAP) only; other plugins (ETP Browser, Data Replay, Object Inspector) are out of scope for now
- `modern-ext/`: new cross‑platform libraries targeting `net8.0` (no Windows dependencies)
- `src/`: Windows/WPF projects upgraded incrementally
- `ext/witsml`: read‑only git submodule (legacy code) — do not modify

## Quick start
1) Check your .NET SDK (8.0+ works; 9.0 is fine while targeting net8.0)

```bash
dotnet --list-sdks
```

2) Ensure submodules are initialized

```bash
git submodule update --init --recursive
```

3) Build and test the modern libraries

```bash
cd modern-ext
dotnet build
dotnet test
```

## Repository layout
```
witsml-studio/
├── ext/                    # UNCHANGED (read-only submodule)
│   └── witsml/             # Legacy .NET Framework 4.5.2
├── modern-ext/             # NEW - Cross-platform .NET 8 libraries and tests
│   ├── WitsmlClient/
│   ├── WitsmlFramework/
│   └── *.Tests/
├── src/                    # Windows/WPF projects upgraded incrementally
└── prompts/migration-sessions/  # Step-by-step migration guides
```

## Migration sessions
- Source of truth: `prompts/migration-sessions/README.md`
- Session 1 — Scaffold & Initial .NET 8 Upgrade: `prompts/migration-sessions/SESSION-1-SCAFFOLD.md`
- Session 2 — Selective Upgrade & Stub Implementation: `prompts/migration-sessions/SESSION-2-BUILD-CORE.md`
- Session 3 — Replace Stubs with Real Implementation: `prompts/migration-sessions/SESSION-3-INTEGRATE.md`
- Session 4 — Fix Integration Errors: `prompts/migration-sessions/SESSION-4-FIX-INTEGRATION.md`
- Session 5 — Validate with Running Code: `prompts/migration-sessions/SESSION-5-VALIDATE-RUNNING.md`

## Notes
- Target `net8.0` for libraries; they will run on newer runtimes (e.g., .NET 9)
- Keep `modern-ext/` platform-agnostic; avoid Windows-only APIs/packages
- It’s expected that non-WitsmlBrowser plugins remain broken until later phases

---
 
### Copyright and License
Copyright &copy; 2018 PDS Americas LLC

Released under the PDS Open Source WITSML™ Product License Agreement
http://www.pds.group/WITSMLstudio/OpenSource/ProductLicenseAgreement

---

### Export Compliance

This source code makes use of cryptographic software:
- SSL/TLS is optionally used to secure web communications

The country in which you currently reside may have restrictions on the import, possession,
use, and/or re-export to another country, of encryption software.  BEFORE using any
encryption software, please check your country's laws, regulations and policies concerning
the import, possession, or use, and re-export of encryption software, to see if this is
permitted.  See <http://www.wassenaar.org/> for more information.

The U.S. Government Department of Commerce, Bureau of Industry and Security (BIS), has
classified this source code as Export Control Classification Number (ECCN) 5D002.c.1, which
includes information security software using or performing cryptographic functions with
symmetric and/or asymmetric algorithms.

This source code is published here:
> https://github.com/pds-technology/witsml-studio

In accordance with US Export Administration Regulations (EAR) Section 742.15(b), this
source code is not subject to EAR:
 - This source code has been made publicly available in accordance with EAR Section
   734.3(b)(3)(i) by publishing it in accordance with EAR Section 734.7(a)(4) at the above
   URL.
 - The BIS and the ENC Encryption Request Coordinator have been notified via e-mail of this
   URL.