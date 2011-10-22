properties {
  $xunitrunner = (Get-ChildItem .\Packages\* -recurse -include xunit.exe).FullName
}

function Invoke-xUnitTestRunner {
param(
  [Parameter(Position=0,Mandatory=0)]
  [string[]]$dlls = @()
  )
  [System.String[]] $arguments = @()
  $argument_string = [string]::join(" ",$arguments)
  Write-Output "Executing `"$xunitrunner $argument_string`""
  #exec { & $xunitrunner $argument_string }
}