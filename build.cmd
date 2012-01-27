@echo off

pushd "%~dp0\build"

call build.cmd %*

popd