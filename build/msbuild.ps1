# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

properties {
  Write-Output "Loading msbuild properties"
  $msbuild = @{}
  $msbuild.logfile = "$($build.dir)\MSBuildOutput.txt"
  $msbuild.max_cpu_count = ([System.Environment]::ProcessorCount / 2)
  $msbuild.build_in_parralel = $true
  $msbuild.logger = "FileLogger,Microsoft.Build.Engine"
  $msbuild.platform = "Any CPU"
}

Task Invoke-MsBuild {
  exec { msbuild /m:"$($msbuild.max_cpu_count)" /p:BuildInParralel=$msbuild.build_in_parralel "/logger:$($msbuild.logger);logfile=$($msbuild.logfile)" /p:Configuration="$($build.configuration)" /p:Platform="$($msbuild.platform)" /p:OutDir="$($build.dir)"\\ "$($solution.file)" }
}