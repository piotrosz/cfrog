az login

pwsh

$resourceGroupName = "rg-cfrog"
$storageAccountName = "stor0cfrog"
$location = "polandcentral"
$subscription = "311d0bf6-7b5c-40ae-880d-a81a7407ebf1"
$appPlanName = "appplan-cfrog"

az account set --subscription $subscription

az account show

# Create resource group
az group create -n $resourceGroupName -l $location

az configure --defaults group=$resourceGroupName

# Create storage account
az storage account create -n $storageAccountName -g $resourceGroupName -l $location --subscription $subscription --sku Standard_LRS

# Create key vault
az keyvault create --name "kvcfrog" --resource-group $resourceGroupName

# Create app service plan
az appservice plan create --name $appPlanName --resource-group $resourceGroupName --sku B1 --is-linux --location $location

# Create app service
az webapp create --name "webapp-cfrog" --resource-group $resourceGroupName --plan $appPlanName --runtime "DOTNETCORE:8.0"