<# 
.SYNOPSIS
	This is the default script created by PowerStudio.
.DESCRIPTION
	Use this as a starting point your PowerShell scripting file.
.NOTES
	This is a sample note for the script.
.PARAMETER fileName
	The filename that the script will use.
.PARAMETER taskList
	A list of the possible tasks to execute.
.PARAMETER dwarf
	A validated set of options.
.EXAMPLE
	Sample command to execute.
		%scriptName% myFile.txt 1,2,4 doc 	
#>
#requires -version 2.0
param(
  [Parameter(Mandatory=$true)]
  [string]$fileName = 'defaultFile.txt',
 
  [Parameter(Mandatory=$false)]
  [string[]]$taskList = @(),
 
  [ValidateSet('happy', 'sleepy', 'grumpy', 'doc', 'bashful', 'sneezy', 'dopey')]
  [string]$dwarf="happy"
)
 
write-host "$fileName for `"$taskList`" given I am `"$dwarf`""