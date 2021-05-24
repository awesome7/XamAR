[bool] $global:buildNative = $false;

$env:BUILDQUEUE.Split(';') | ForEach-Object {
    if(-Not $_.Contains("Forms")) {
        $global:buildNative = $true;
    }
}

Write-Host "##vso[task.setvariable variable=buildNative;isOutput=true]$global:buildNative"