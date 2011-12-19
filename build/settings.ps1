# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 


properties {
  Write-Output "Loading settings properties"
  
  $base = @{}
  $base.dir = resolve-path .\..\
  
  $source = @{}
  $source.dir = "$($base.dir)\src"
  if(!(Test-Path($source.dir))) { $source.dir = "$($base.dir)\source" }
  if(!(Test-Path($source.dir))) { $source.dir = "$($base.dir)" }
  
  $build = @{}
  $build.dir = "$($base.dir)\bin"
  $build.configuration = "Release"
  $build.version = if($env:BUILD_NUMBER) {$env:BUILD_NUMBER} else { "1.1.1" }
  
  $tools = @{}
  $tools.dir = "$($base.dir)\tools"
  
  $solution = @{}
  $solution.name = "$(Split-Path $($base.dir) -leaf)"
  $solution.file = "$($base.dir)\$($solution.name).sln"
  
  $release = @{}
  $release.dir = "$($base.dir)\release"
  
  $packages = @{}
  $packages.name = "Packages"
  $packages.dir = "$($base.dir)\$($packages.name)"
}