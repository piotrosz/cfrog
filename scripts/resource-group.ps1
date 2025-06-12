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
az keyvault create --name $vaultName --resource-group $resourceGroupName --enable-rbac-authorization false

# Create app service plan
az appservice plan create --name $appPlanName --resource-group $resourceGroupName --sku B1 --is-linux --location $location

# Create app service
az webapp create --name $webAppName --resource-group $resourceGroupName --plan $appPlanName --runtime "DOTNETCORE:9.0"

# Assign idenity
az webapp identity assign -g $resourceGroupName -n $webAppName

$principalId = (az webapp identity show -g $resourceGroupName -n $webAppName --query principalId)

# Add configuration
az webapp config appsettings set --resource-group $resourceGroupName --name $webAppName --settings "VaultName=$vaultName"

az webapp config appsettings list --resource-group $resourceGroupName --name $webAppName

# Grant access to the vault
az keyvault set-policy --secret-permissions get list --name $vaultName --object-id $principalId.Trim("""")