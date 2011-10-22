# Load the VS2010 command prompt for mstest et al.
$vcargs = ?: {$Pscx:Is64BitProcess} {'amd64'} {'x86'}
$VS100VCVarsBatchFile = "${env:VS100COMNTOOLS}..\..\VC\vcvarsall.bat"
Invoke-BatchFile $VS100VCVarsBatchFile $vcargs


properties {
  $mstest = [System.IO.Path]::Combine((Get-ProgramFilesX86), "Microsoft Visual Studio 10.0\Common7\IDE\MSTest.exe")
}

function Invoke-MSTest {
param(
  [Parameter(Position=0,Mandatory=0)]
  [string[]]$testContainers = @()
  )
  [System.String[]] $arguments = $testContainers | % {"/testcontainer:" + "`"$_`""}
  $argument_string = [string]::join(" ",$arguments)
  Write-Output "Executing `"$mstest $argument_string`""
  exec { & $mstest $argument_string }
}