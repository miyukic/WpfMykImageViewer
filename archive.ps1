param(
    [Parameter()]
    [string]$inputPath,
    [string]$outputPath
)

Compress-Archive -Path $inputPath -DestinationPath $outputPath
