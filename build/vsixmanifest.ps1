# 
# Copyright (c) 2011, Toji Project Contributors
# 
# Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
# See the file LICENSE.txt for details.
# 

properties {
  Write-Output "Loading vsixmanifest properties"
  
  $vsixmanifest = @{}
  $vsixmanifest.file = "$($source.dir)\PowerStudio\source.extension.vsixmanifest"
}

Task Version-VsixManifest {
  Assert (Test-Path($vsixmanifest.file)) "VsixManifest was not detected: $($vsixmanifest.file)"
  #$pattern = "\d*\.\d*\.\d*\.\d*"  # 4 digit
  $pattern = "<Version>\d*\.\d*\.\d*</Version>"   # 3 digit for semver
  $content = Get-Content $vsixmanifest.file -encoding UTF8 | % { [Regex]::Replace($_, $pattern, "<Version>$($build.version)</Version>") } 
  Set-Content -Value $content -Path $vsixmanifest.file -encoding UTF8
}