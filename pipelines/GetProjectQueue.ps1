# https://github.com/nikolic-bojan/azure-yaml-build/blob/master/build/get-service-queue.ps1

$global:buildQueueVariable = ""
$global:buildSeparator = ";"

$organizationName = "awesome7"
$definitionId = 2

Function AppendQueueVariable([string]$folderName)
{
	$folderNameWithSeparator = -join($folderName, $global:buildSeparator)

	if ($global:buildQueueVariable -notmatch $folderNameWithSeparator)
	{
        $global:buildQueueVariable = -join($global:buildQueueVariable, $folderNameWithSeparator)
	}
}

if ($env:BUILDQUEUEINIT)
{
	Write-Host "Build Queue Init: $env:BUILDQUEUEINIT"
	Write-Host "##vso[task.setvariable variable=buildQueue;isOutput=true]$env:BUILDQUEUEINIT"
	exit 0
}

# https://stackoverflow.com/a/63418864/1518596
# Get all files that were changed
$azureDevOpsAuthenicationHeader = @{Authorization = 'Basic ' + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":$env:PAT")) }
$url = "https://dev.azure.com/${organizationName}/Public/_apis/build/latest/${definitionId}?branchName=main&api-version=6.0-preview.1"

$response = (Invoke-RestMethod -Uri $url -Method GET -Headers $azureDevOpsAuthenicationHeader)
$editedFiles = (git diff HEAD $response.sourceVersion --name-only)

# Check each file that was changed and add that Project to Build Queue
$editedFiles | ForEach-Object { 
    $parts = $_.split('/')
    if($parts.Count -gt 1) {
        AppendQueueVariable $parts[1]
    }
}

Write-Host "Build Queue: $global:buildQueueVariable"
Write-Host "##vso[task.setvariable variable=BUILDQUEUE]$global:buildQueueVariable"