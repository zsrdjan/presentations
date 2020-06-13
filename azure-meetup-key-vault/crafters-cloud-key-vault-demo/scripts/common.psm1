class ConfigParameters {
    [string]$resourceGroup
    [string]$appServicePlan
    [bool]$createPlanAndResourceGroup
    [string]$location
    [string]$planSku
    [string]$webAppName
    [bool] $createKeyVault
    [string] $keyVault
}

 function Read-Configuration {
    [OutputType([ConfigParameters])]
    param ()

    return [ConfigParameters] (Get-Content 'parameters.json' | Out-String | ConvertFrom-Json)
}

Export-ModuleMember -Function Read-Configuration