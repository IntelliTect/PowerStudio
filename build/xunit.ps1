# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

properties {
  Write-Output "Loading xunit properties"
  $xunit = @{}
  $xunit.runner = $null#(Resolve-Path .\Path\to\xunit\console)
  if($xunit.runner -eq $null -or !(Test-Path($xunit.runner))) {  
    $xunit.runner = (Get-ChildItem "$($tools.dir)\*" -recurse -include xunit.console.clr4.x86.exe).FullName
  }
  $xunit.logfile = "xunit.log.xml"
  Assert (![string]::IsNullOrEmpty($xunit.runner)) "The location of the xunit runner must be specified."
  Assert (Test-Path($xunit.runner)) "Could not find xunit runner"
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
  # TODO: This only keeps the last log file. Need to output many and merge.
  #$dlls | % { Resolve-Path $dlls } | % { exec { Invoke-Expression "& `"$($xunit.runner)`" `"$_`" /xml `"$($build.dir)\$($xunit.logfile)`" /noshadow" }}
  $dlls | % { exec { & "$($xunit.runner)" "$_" /xml "$($build.dir)\$($xunit.logfile)" /noshadow }}
}