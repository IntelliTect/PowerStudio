# 
# Copyright (c) 2011, PowerStudio Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

Include xunit.ps1

properties {
  $solution_name = "PowerStudio"
  $base_directory = resolve-path .
  $build_directory = "$base_directory\build"
  $build_configuration = "Release"
  $tools_directory = "$base_directory\tools"
  $solution_file = "$base_directory\$solution_name.sln"
  $release_directory = "$base_directory\release"
  $build_number = if($env:BUILD_NUMBER) {$env:BUILD_NUMBER.Split('.')[2] } else { "0" }
  $version = if($env:BUILD_NUMBER) {$env:BUILD_NUMBER} else { "1.0.0" }
  $testMessage = 'Executed Test!'
  $compileMessage = 'Executed Compile!'
  $cleanMessage = 'Executed Clean!'
  $msbuild_logfile = 'MSBuildOutput.txt'
  $max_cpu_count = [System.Environment]::ProcessorCount / 2
  $build_in_parralel = $true
}

Task Build -depends Compile
Task Default -depends Build
Task Release -depends Default, Test

Task Test { 
  $test_dlls = gci "$build_directory\*.Tests.dll"
  Invoke-xUnitTestRunner $test_dlls
}

Task IntegrationTest -depends Test { 
  $test_dlls = gci "$build_directory\*.IntegrationTests.dll"
  Invoke-xUnitTestRunner $test_dlls
}

Task Init -depends Clean {
  new-item $release_directory -itemType directory | Out-Null
  new-item $build_directory -itemType directory | Out-Null
}

Task Compile -depends Init {
  exec {msbuild /m:$max_cpu_count /p:BuildInParralel=$build_in_parralel "/logger:FileLogger,Microsoft.Build.Engine;logfile=$msbuild_logfile" /p:Configuration="$build_configuration" /p:Platform="Any CPU" /p:OutDir="$build_directory"\\ "$solution_file"}
}

Task Clean { 
  remove-item -force -recurse $build_directory -ErrorAction SilentlyContinue | Out-Null
  remove-item -force -recurse $release_directory -ErrorAction SilentlyContinue | Out-Null
}

Task ? -Description "Helper to display task info" {
  Write-Documentation
}