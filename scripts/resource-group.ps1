az login

$resourceGroupName = "rg-cfrog"
$storageAccountName = "stor0cfrog"
$location = "polandcentral"
$subscription = "311d0bf6-7b5c-40ae-880d-a81a7407ebf1"

az account set --subscription $subscription

# Create resource group
az group create -n $resourceGroupName -l $location

az configure --defaults group=$resourceGroupName

# Create storage account
az storage account create -n $storageAccountName -g $resourceGroupName -l $location --subscription $subscription --sku Standard_LRS
