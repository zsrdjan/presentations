Import-Module  .\common.psm1

# stop on error
$ErrorActionPreference = "Stop"

$config = Read-Configuration

$keyVaultName = $config.keyVault

# Docs: https://docs.microsoft.com/en-us/cli/azure/

# in case you need to login
# az login -u johndoe@contoso.com -p VerySecret

# show account information
az account show

if($config.createKeyVault -eq $True){
    Write-Host "Creating keyvault: $keyVaultName"
    az keyvault create --resource-group $config.resourceGroup --name $keyVaultName --enable-soft-delete true --location $config.location

    Write-Host "Adding sample key"
    az keyvault key create --vault-name $keyVaultName --name 'SampleKeyVaultKey' --protection software

    Write-Host "Adding sample secret"
    az keyvault secret set --vault-name $keyVaultName --name 'App--SampleKeyVaultSecret' --value 'I am secret coming from keyvault'

    Write-Host "Adding sample secret"
    az keyvault secret set --vault-name $keyVaultName --name 'App--GitHubApi--SuperSecretKey' --value 'I am GitHubApi secret coming from keyvault'
}

$keyVaultUrl = "https://$keyVaultName.vault.azure.net/".ToLower()
Write-Host "Connecting app with key vault: $keyVaultUrl"
    
az webapp config appsettings set  -g $config.resourceGroup -n $config.webAppName --settings ASPNETCORE_HOSTINGSTARTUP__KEYVAULT__CONFIGURATIONENABLED=true
az webapp config appsettings set  -g $config.resourceGroup -n $config.webAppName --settings ASPNETCORE_HOSTINGSTARTUP__KEYVAULT__CONFIGURATIONVAULT=$keyVaultUrl

Write-Host "Enabling managed service identity for the web app"
$result = ($(az webapp identity assign -g $config.resourceGroup -n $config.webAppName))
Write-Host $result
$principalId = $result | ConvertFrom-Json | Select -ExpandProperty principalId

Write-Host "PrincipalId: $principalId"

Write-Host "Authorizing application with id: $principalId to read secrets in the vault"
az keyvault set-policy --name $keyVaultName --object-id $principalId --secret-permissions get list

# Command for authorizing application to decrypt and sign iwth keys in the vault
#az keyvault set-policy --name $keyVaultName --object-id $principalId --key-permissions decrypt sign