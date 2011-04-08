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
}

task build -depends Compile, Test
task default -depends Release

task Test -depends Compile, Clean { 
  $testMessage
}

task Init -depends Clean {
    new-item $release_directory -itemType directory | Out-Null
    new-item $build_directory -itemType directory | Out-Null
}

task Compile -depends Clean {
  exec {msbuild "/logger:FileLogger,Microsoft.Build.Engine;logfile=$msbuild_logfile" /p:Configuration="$build_configuration" /p:Platform="Any CPU" /p:OutDir="$build_directory"\\ "$solution_file"}
}

task Clean { 
  #remove-item -force -recurse $build_directory -ErrorAction SilentlyContinue | Out-Null
  remove-item -force -recurse $release_directory -ErrorAction SilentlyContinue | Out-Null
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}