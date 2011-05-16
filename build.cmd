@echo off

pushd "%~dp0"

echo "Updating Dependencies"
powershell -NoProfile -ExecutionPolicy unrestricted -Command "& { Get-ChildItem -recurse -include packages.config | ForEach-Object { .\Tools\NuGet\NuGet.exe i $_ -o Packages } }"

SETLOCAL

SET _default = ""

if "%1" == "" (
    echo "no build arguments were supplied, defaulting to: -framework 4.0x86"
    set _default=-framework 4.0x86
)

powershell -NoProfile -ExecutionPolicy unrestricted -Command "& { Import-Module "(Get-ChildItem .\Packages\* -recurse -include psake.psm1).FullName"; Invoke-psake .\build.ps1 %_default% %* }"

ENDLOCAL

popd

pause