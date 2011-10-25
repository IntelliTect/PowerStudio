properties {
  $xunit = @{}
  $xunit.runner = (Resolve-Path .\tools\xunit\xunit.console.clr4.exe)
  $xunit.logfile = "xunit.log.xml"
  if(($xunit.runner -eq $null) -or ($xunit.runner -eq "")) {Assert $false "Could not find xunit runner"}
}

function Invoke-xUnitTestRunner {
  param(
    [Parameter(Position=0,Mandatory=0)]
    [string[]]$dlls = @()
  )
  # TODO: This only keeps the last log file. Need to output many and merge.
  $dlls | % { exec { Invoke-Expression "& `"$($xunit.runner)`" `"$_`" /xml `"$build_directory\$($xunit.logfile)`" /noshadow" }}
}