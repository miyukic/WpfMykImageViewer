param(
    [Parameter()]
    [string]$inputPath,
    [string]$outputPath
)
Get-ChildItem -Directory $inputPath | 
% { echo archive.ps1‚Ì’†$(ls $inputPath);Compress-Archive -Path $_.FullName -DestinationPath $(${outputPath} + "\" + $_.Name) && Remove-Item -Recurse -Force $_.FullName }
dir $inputPath
#Compress-Archive -Path $inputPath -DestinationPath $outputPath
