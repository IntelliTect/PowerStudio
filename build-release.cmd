@echo off

pushd "%~dp0"

powershell -NoProfile -ExecutionPolicy unrestricted -Command "& { Import-Module "(Get-ChildItem .\Packages\* -recurse -include psake.psm1).FullName"; Invoke-psake .\build.ps1 -framework 4.0x86 -t Test }"

popd

pause