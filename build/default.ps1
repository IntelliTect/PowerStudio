
# Helper script for those who want to run psake without importing the module.
# This script is heavily based on the original psake.ps1
# A NuGet bootstrapper is added allowing the script to download and install psake
# Example:
# .\default.ps1 "build.ps1" "BuildHelloWord" "4.0" 

# Must match parameter definitions for psake.psm1/invoke-psake 
# otherwise named parameter binding fails
param(
  [Parameter( Position = 0, Mandatory = 0 )]
  [string] $buildFile = 'build.ps1',
  [Parameter( Position = 1, Mandatory = 0 )]
  [string[]] $taskList = @(),
  [Parameter( Position = 2, Mandatory = 0 )]
  [string] $framework = '4.0x86',
  [Parameter( Position = 3, Mandatory = 0 )]
  [switch] $docs = $false,
  [Parameter( Position = 4, Mandatory = 0 )]
  [System.Collections.Hashtable] $parameters = @{},
  [Parameter( Position = 5, Mandatory = 0 )]
  [System.Collections.Hashtable] $properties = @{}
)

# We need to remove the module if it is already loaded.
remove-module psake -ea 'SilentlyContinue'

$packages = (Resolve-Path ..\Packages)

# Load psake
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.path
$psakeModule = (Get-ChildItem $packages\* -recurse -include psake.psm1).FullName
Write-Output "Loading psake module : $psakeModule"
Import-Module $psakeModule
if (-not(test-path $buildFile)) { $buildFile = (join-path $scriptPath $buildFile) } 

# Execute the script passing the command line arguments.
Invoke-Psake $buildFile $taskList $framework $docs $parameters $properties
exit $lastexitcode