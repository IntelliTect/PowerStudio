# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

properties {
  Write-Output "Loading assembly info properties"
  
  $assemblyinfo = @{}
  $assemblyinfo.file = "$($source.dir)\GlobalAssemblyInfo.cs"
  $assemblyinfo.contents = @"
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyCompany(`"`")]
[assembly: AssemblyProduct(`"`")]
[assembly: AssemblyCopyright(`"Copyright © 2011`")]
[assembly: AssemblyTrademark(`"`")]
[assembly: AssemblyCulture(`"`")]
[assembly: AssemblyVersion(`"1.0.0`")]
[assembly: AssemblyFileVersion(`"1.0.0`")]
"@
}

Task Version-AssemblyInfo {
  if(!(Test-Path($assemblyinfo.file))) { 
    Set-Content -Value $assemblyinfo.contents -Path $assemblyinfo.file
    Write-Host -ForegroundColor Red "GlobalAssemblyInfo was not detected has has been created: $($assemblyinfo.file)"
  }
  #$pattern = "\d*\.\d*\.\d*\.\d*"  # 4 digit
  $pattern = "\d*\.\d*\.\d*"   # 3 digit for semver
  $content = Get-Content $assemblyinfo.file | % { [Regex]::Replace($_, $pattern, $build.version) } 
  Set-Content -Value $content -Path $assemblyinfo.file
}