# Readme

This folder contains a set of sample scripts for setting up the:

- App service
- App Insights
- Azure Key Vault

Use scripts in following order:

- Open `account.ps1` - check whether you are logged in and which subscription is selected
- Open `parameters.json` and adjust the values. The values entered here are used by all the Powershell scripts. No additional configuration is needed.
- Run `create-app-service.ps1` - this script will create the app service, app insights and configure them. E.g:
  - turn off PHP
  - configure app to use SSL only
  - create api virtual application
  - enable file system logging
- Run `create-key-vault.ps1` - this script creates an Azure Key Vault and allows the app service to access the key vault

//TODO:
