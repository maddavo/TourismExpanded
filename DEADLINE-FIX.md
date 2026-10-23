# Disabled deadlines fix v2 (assembly 1.0.0.2)

Tourism Expanded's configurations set deadline = 1. The original disabled option skipped its custom calculation but left this six-hour default intact.

The first fork patch cleared the type, duration and absolute timestamp only in OnOffered. That missed cached offers restored from a save. The 13 affected contract GUIDs accepted at Year 4 Day 118 04:33:19 were already present as Offered in quicksave #166. Accepting them after restarting KSP did not invoke OnOffered again.

Version 2 applies the same policy at OnOffered, OnAccepted and OnLoad (offered/active states only). When disabled it clears Contract.deadlineType, TimeDeadline and dateDeadline together. When enabled it leaves deadline fields unchanged and retains the original offer-time calculation. Completed contract history is not rewritten. Reflection field names and types were checked against installed KSP 1.12.5. Base Contract.Load restores its deadline fields before invoking the behaviour OnLoad callback.

Diagnostic log: [TourismExpanded deadline-fix-v2] Cleared deadline at load/accept/offer: <title>. A message is emitted only when deadline data changes. Assembly version 1.0.0.2 identifies the revised binary in the KSP assembly list.

Build: ./build-local.ps1. Output: build/TourismExpanded.dll. Requires a .NET SDK and installed KSP/Contract Configurator assemblies. The script does not install files.

Regression checks: dotnet run --project tests/DeadlinePolicy/DeadlinePolicy.csproj. Tests compile the production DeadlinePolicy against a minimal Contract field fixture and cover all three cleared fields, unrelated reward/passenger preservation, idempotence, enabled-setting preservation and residual absolute timestamps. Installed callback signatures and ordering were independently checked in the real game/Contract Configurator assemblies; the rebuilt callbacks were inspected.

Validation: build and all five checks passed. Installed file hash verified. Revised in-game callbacks and newly accepted contracts await KSP restart and gameplay confirmation. The prior save repair was confirmed in-game; that did not validate future offer handling.
