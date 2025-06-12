az login

pwsh

$resourceGroupName = "rg-cfrog"
$storageAccountName = "stor0cfrog"
$location = "polandcentral"
$subscription = "-- Fill in subscription id --"
$appPlanName = "appplan-cfrog"
$webAppName = "webapp-cfrog"
$vaultName = "kvcfrog2"

az account set --subscription $subscription

az account show

# Create resource group
az group create -n $resourceGroupName -l $location

az configure --defaults group=$resourceGroupName

# Create storage account
az storage account create -n $storageAccountName -g $resourceGroupName -l $location --subscription $subscription --sku Standard_LRS

# Create key vault
az keyvault create --name $vaultName --resource-group $resourceGroupName --enable-rbac-authorization true

# Create app service plan
az appservice plan create --name $appPlanName --resource-group $resourceGroupName --sku B1 --is-linux --location $location

# Create app service
az webapp create --name $webAppName --resource-group $resourceGroupName --plan $appPlanName --runtime "DOTNETCORE:9.0"

# Enable system-assigned managed identity for the web app
az webapp identity assign --name $webAppName --resource-group $resourceGroupName

# Get the principal ID of the web app's managed identity
$webAppPrincipalId = $(az webapp identity show --name $webAppName --resource-group $resourceGroupName --query principalId --output tsv)
Write-Output "Web App Principal ID: $webAppPrincipalId"

# Create Key Vault access policy for the web app's managed identity
# This allows the web app to get secrets and certificates from the Key Vault
az keyvault set-policy --name $vaultName --resource-group $resourceGroupName --object-id $webAppPrincipalId --secret-permissions get list

# Add Key Vault reference to app settings
# Replace 'YourSecretName' with the name of your secret in Key Vault
az webapp config appsettings set --name $webAppName --resource-group $resourceGroupName --settings "ConnectionStrings__DefaultConnection=@Microsoft.KeyVault(SecretUri=https://$vaultName.vault.azure.net/secrets/ConnectionString/)"
az webapp config appsettings set --name $webAppName --resource-group $resourceGroupName --settings "AzureWebJobsStorage=@Microsoft.KeyVault(SecretUri=https://$vaultName.vault.azure.net/secrets/ConnectionString/)"

# Assign the Key Vault Secrets User role to the web app's managed identity
# This is required when using RBAC authorization model with Key Vault
$keyVaultId = $(az keyvault show --name $vaultName --resource-group $resourceGroupName --query id --output tsv)
az role assignment create --assignee $webAppPrincipalId --role "Key Vault Secrets User" --scope $keyVaultId

# Add Key Vault reference to app configuration
az webapp config set --name $webAppName --resource-group $resourceGroupName --generic-configurations "{'keyVaultReferenceIdentity': 'SystemAssigned'}"

# Optional: Create a test secret in the Key Vault
az keyvault secret set --vault-name $vaultName --name "ConnectionString" --value "YourConnectionStringValue"

Write-Output "Web App successfully connected to Key Vault using Managed Identity"

