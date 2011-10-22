@echo off

pushd "%~dp0"

powershell -NoProfile -ExecutionPolicy unrestricted -Command "& { .\default.ps1 %* }"

popd

pause