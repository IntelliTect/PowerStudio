# 
# Copyright (c) 2011, PowerStudio Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

function Test-Is64Bit {
	  $ptrSize = [System.IntPtr]::Size
	  switch ($ptrSize) {
    4 {		return $false		}
    8 {		return $true		}
  	  default { throw ("Unknown pointer size ({0}) returned from System.IntPtr." -f $ptrSize) }
  }
}

function Get-ProgramFilesX86 {
  if(Test-Is64Bit) {
    return (Get-Item "env:ProgramFiles(x86)").Value
  }
  return (Get-Item "env:ProgramFiles").Value
}

properties {
  $solution_name = "PowerStudio"
  $base_directory = resolve-path .
  $build_directory = "$base_directory\build"
  $build_configuration = "Release"
  $tools_directory = "$base_directory\tools"
  $solution_file = "$base_directory\$solution_name.sln"
  $release_directory = "$base_directory\release"
  $build_number = if($env:BUILD_NUMBER) {$env:BUILD_NUMBER.Split('.')[2] } else { "0" }
  $version = if($env:BUILD_NUMBER) {$env:BUILD_NUMBER} else { "1.0.0.0" }
  $testMessage = 'Executed Test!'
  $compileMessage = 'Executed Compile!'
  $cleanMessage = 'Executed Clean!'
  $msbuild_logfile = 'MSBuildOutput.txt'
  $max_cpu_count = [System.Environment]::ProcessorCount / 2
  $build_in_parralel = $true
  $mstest = [System.IO.Path]::Combine((Get-ProgramFilesX86), "Microsoft Visual Studio 10.0\Common7\IDE\MSTest.exe")
}

task build -depends Compile, Test
task default -depends Release

task Test -depends Compile { 
  exec { . $mstest /testcontainer:"$build_directory\PowerStudio.VsExtension.Tests.dll" }
}

task IntegrationTest -depends Test { 
  exec { . $mstest /testcontainer:"$build_directory\PowerStudio.VsExtension.IntegrationTests.dll" }
}

task Init -depends Clean {
  new-item $release_directory -itemType directory | Out-Null
  new-item $build_directory -itemType directory | Out-Null
}

task Compile -depends Init {
  exec {msbuild /m:$max_cpu_count /p:BuildInParralel=$build_in_parralel "/logger:FileLogger,Microsoft.Build.Engine;logfile=$msbuild_logfile" /p:Configuration="$build_configuration" /p:Platform="Any CPU" /p:OutDir="$build_directory"\\ "$solution_file"}
}

task Clean { 
  remove-item -force -recurse $build_directory -ErrorAction SilentlyContinue | Out-Null
  remove-item -force -recurse $release_directory -ErrorAction SilentlyContinue | Out-Null
}

task ? -Description "Helper to display task info" {
  Write-Documentation
}