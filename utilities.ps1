Import-Module .\References\Pscx\Pscx.psd1

function Get-ProgramFilesX86 {
  if($Pscx:Is64BitProcess) {
    return (Get-Item "env:ProgramFiles(x86)").Value
  }
  return (Get-Item "env:ProgramFiles").Value
}
