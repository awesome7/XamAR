[string] $global:buildNative = '';

$env:BUILDQUEUE.Split(';') | ForEach-Object {
    if(-Not $_.Contains("Forms")) {
        $global:buildNative = 'True';
    }
}

Write-Host "##vso[task.setvariable variable=BUILDNATIVE]$global:buildNative"