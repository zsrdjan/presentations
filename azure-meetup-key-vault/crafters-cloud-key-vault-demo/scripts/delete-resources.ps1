Import-Module  .\common.psm1

# stop on error
$ErrorActionPreference = "Stop"

$config = Read-Configuration

# show account information
az account show

Write-Host "Deleting web app: $($config.webAppName)"
az webapp delete --name $config.webAppName --resource-group $config.resourceGroup

Write-Host "Deleting app insights"
az resource delete --resource-group $config.resourceGroup --resource-type microsoft.insights/components --name $config.webAppName 

Write-Host "Soft deleting keyvault: $($config.keyVault)"
az keyvault delete --name $config.keyVault --resource-group $config.resourceGroup

Write-Host "Purging keyvault: $($config.keyVault)"
az keyvault purge --name $config.keyVault

#Write-Host "Deleting app service plan: $($config.appServicePlan)"
#az appservice plan delete --name $config.appServicePlan --resource-group $config.resourceGroup
