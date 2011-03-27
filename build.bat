@echo off

pushd "%~dp0"

powershell -NoProfile -ExecutionPolicy unrestricted -Command "& { Import-Module .\tools\psake\psake.psm1; Invoke-psake .\build.ps1 -framework 4.0x86 -t Build }"

popd

pause