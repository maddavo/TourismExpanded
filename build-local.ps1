param([string]$KspDir = "C:\Program Files (x86)\Steam\steamapps\common\Kerbal Space Program")
$ErrorActionPreference = "Stop"
$repo = $PSScriptRoot
New-Item -ItemType Directory -Path "$repo\build" -Force | Out-Null
$csc = Get-ChildItem "${env:ProgramFiles}\dotnet\sdk\*\Roslyn\bincore\csc.dll" | Sort-Object FullName -Descending | Select-Object -First 1
$compilerArgs = @('/target:library','/nostdlib+','/optimize+',("/out:`"$repo\build\TourismExpanded.dll`""))
$compilerArgs += Get-ChildItem "$KspDir\KSP_x64_Data\Managed\*.dll" | ForEach-Object { "/reference:`"$($_.FullName)`"" }
$compilerArgs += "/reference:`"$KspDir\GameData\ContractConfigurator\ContractConfigurator.dll`""
$compilerArgs += Get-ChildItem "$repo\Source\TourismExpanded\TourismExpanded\*.cs" -Recurse | ForEach-Object { "`"$($_.FullName)`"" }
$compilerArgs | Set-Content "$repo\build\compile.rsp" -Encoding UTF8
& dotnet $csc.FullName "@$repo\build\compile.rsp"
if ($LASTEXITCODE -ne 0) { throw "Compilation failed" }
