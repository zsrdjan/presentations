Import-Module  .\common.psm1

# stop on error
$ErrorActionPreference = "Stop"

$config = Read-Configuration

# Docs: https://docs.microsoft.com/en-us/cli/azure/

# in case you need to login
# az login -u johndoe@contoso.com -p VerySecret

# show account information
az account show

if($config.createPlanAndResourceGroup -eq $True){
    Write-Host "Creating resource group: $($config.resourceGroup)"
    az group create --location $config.location --name $config.resourceGroup

    Write-Host "Creating plan: $($config.appServicePlan)"
    az appservice plan create -g $config.resourceGroup -n $config.appServicePlan --sku $config.planSku
}

Write-Host "Creating web app: $($config.webAppName)"
az webapp create --name $config.webAppName --resource-group $config.resourceGroup --plan $config.appServicePlan

Write-Host "Configuring the web app"
#turn off php
az webapp config set -g $config.resourceGroup -n $config.webAppName --php-version off

#configure logging
az webapp log config -g $config.resourceGroup -n $config.webAppName --application-logging false --web-server-logging filesystem --level error
az webapp config appsettings set  -g $config.resourceGroup -n $config.webAppName --settings WEBSITE_HTTPLOGGING_RETENTION_DAYS=5

az webapp update -g $config.resourceGroup -n $config.webAppName --https-only=true

Write-Host "Creating AppInsights: $($config.webAppName)"
az resource create --resource-group $config.resourceGroup --resource-type microsoft.insights/components --name $config.webAppName --location $config.location --properties '{\"ApplicationId\":\"\", \"Application_Type\":\"web\", \"Flow_Type\":\"Redfield\",\"Request_Source\":\"IbizaAIExtension\"}'

Write-Host "Getting the AppInsights instrumentation key"
$instrumentationKey = az resource show -g $config.resourceGroup -n $config.webAppName --resource-type microsoft.insights/components --query properties.InstrumentationKey

Write-Host "AppInsights Instrumentation key is: $instrumentationKey"

$instrumentationKey = $instrumentationKey.Replace('"', '')

Write-Host "Connectig AppInsights with the WebApp"
az webapp config appsettings set  -g $config.resourceGroup -n $config.webAppName --settings APPINSIGHTS_INSTRUMENTATIONKEY=$instrumentationKey