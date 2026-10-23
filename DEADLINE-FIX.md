# Disabled deadlines fix

The installed 1.0.0 DLL matches the DLL on upstream main. Its OnOffered handler only calculates deadlines when enableDeadlines is true. When false, it leaves the configuration's deadline = 1 (one Kerbin day, six hours) intact.

The disabled branch now sets Contract.deadlineType to None, TimeDeadline to zero, and dateDeadline to zero. The protected fields are accessed through reflection against Contracts.Contract; both names and types were checked in the installed KSP 1.12.5 assembly. The enabled branch and all travel calculations are unchanged.

This affects newly offered Tourism Expanded contracts. Already offered or accepted contracts require a separate save repair; changing the setting does not rewrite them. Restart KSP after replacing the DLL. Keep Enable deadlines disabled for contracts without deadlines.

Build with `./build-local.ps1`; requires a .NET SDK and the installed KSP/Contract Configurator assemblies. Output: build/TourismExpanded.dll. This script does not install files.

Validation: compilation against installed KSP and Contract Configurator passed; rebuilt DLL inspection confirms all three fields are cleared on the disabled branch. Gameplay validation is pending restart and a newly generated offer.
