# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

properties {
  Write-Output "Loading nunit properties"
  $nunit = @{}
  $nunit.runner = (Get-ChildItem "$($packages.dir)\*" -recurse -include nunit-console-x86.exe).FullName
  $nunit.runner
  Assert (![string]::IsNullOrEmpty($nunit.runner)) "The location of the nunit runner must be specified."
  Assert (Test-Path($nunit.runner)) "Could not find nunit runner"
}

function Invoke-TestRunner {
  param(
    [Parameter(Position=0,Mandatory=0)]
    [string[]]$dlls = @()
  )
  if ($dlls.Length -le 0) { 
     Write-Host -ForegroundColor Red "No tests defined"
     return 
  }
  exec { & $nunit.runner $dlls /noshadow }
}