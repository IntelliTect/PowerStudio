# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

properties {
  Write-Output "Loading nuget properties"
  $nuget = @{}
  $nuget.pub_dir = "$($release.dir)"
  $nuget.file = "$($tools.dir)\NuGet\NuGet.exe"
  if(!(Test-Path($nuget.file))) {  
    $nuget.file = (Get-ChildItem "$($packages.dir)\*" -recurse -include NuGet.exe).FullName
  }
  # add either the project_name or nuspec file to use when packaging.
  $nuget.target = "$($source.dir)\$($solution.name).nuspec"
  $nuget.options = ""
  if(!(Test-Path($nuget.target))) {  
    $nuget.target = "$($source.dir)\$($solution.name)\$($solution.name).csproj"
    $nuget.options = "-Build -Sym -Properties Configuration=$($build.configuration)"
  }
  $nuget.command = "& $($nuget.file) pack $($nuget.target) $($nuget.options) -Version $($build.version) -OutputDirectory $($nuget.pub_dir)"
  Assert (![string]::IsNullOrEmpty($nuget.target)) "The location of the nuget exe must be specified."
  Assert (Test-Path($nuget.target)) "Could not find nuget exe"
  Assert (![string]::IsNullOrEmpty($nuget.file)) "The location of the nuget exe must be specified."
  Assert (Test-Path($nuget.file)) "Could not find nuget exe"
}

Task Bootstrap-NuGetPackages {
  Write-Output "Loading Nuget Dependencies"
  . { Get-ChildItem -recurse -include packages.config | % { & $nuget.file i $_ -o Packages } }
}

Task Package -depends Set-Version {
  if(!(Test-Path($nuget.pub_dir))) { new-item $nuget.pub_dir -itemType directory | Out-Null }
  $path = Split-Path $nuget.target
  Write-Host "Moving into $path"
  Push-Location $path
  Write-Host "Executing exec { Invoke-Expression $($nuget.command) }"
  exec { Invoke-Expression $nuget.command }
  Pop-Location
}

Task Publish {
  Push-Location "$($nuget.pub_dir)"
  ls "*$($build.version).nupkg" | % { & $nuget.file push $_ }
  Pop-Location
}

Task Set-Version {
  #$version_pattern = "\d*\.\d*\.\d*\.\d*"  # 4 digit
  $version_pattern = "<version>\d*\.\d*\.\d*</version>"   # 3 digit for semver
  $content = Get-Content $nuget.target | % { [Regex]::Replace($_, $version_pattern, "<version>$($build.version)</version>") } 
  Set-Content -Value $content -Path $nuget.target
}