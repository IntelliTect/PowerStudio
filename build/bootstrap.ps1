# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

$path = (Resolve-Path ".\..\")
Push-Location $path
Write-Output "Loading Nuget Dependencies"
$nuget = ".\Tools\Nuget\NuGet.exe"
if(!(Test-Path($nuget))) {  
  $nugets = @(Get-ChildItem "..\*" -recurse -include NuGet.exe)
    if ($nugets.Length -le 0) { 
     Write-Host -ForegroundColor Red "No NuGet executables found."
     return 
  }
  $nuget = $nugets[0].FullName
}
$nuget = Resolve-Path $nuget
$output = "Packages"
$package_files = Get-ChildItem . -recurse -include packages.config
$package_files | % { & $nuget i $_ -OutputDirectory $output }
Pop-Location