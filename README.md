## 🐸 cfrog

Blazor web app. 
Uses `InteractiveServer` and `InteractiveWebAssembly` render modes: [link](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-9.0)
Azure table storage used as data store. 
Google auth.

### Docker

Run locally with docker with source code attached:
```bash
docker run -d -p 8081:5289 -v $(pwd):/var/www -w "/var/www" mcr.microsoft.com/dotnet/sdk:9.0 bash -c "dotnet watch run --project ./src/CookingFrog.WebUI/CookingFrog.WebUI/CookingFrog.WebUI.csproj"  
```

launchsettings.json - replace localhost with *.

Build and then run docker image from Dockerfile

```
docker build -t piotrosz/cfrog:1.0 .
docker run -p 8081:8080 -e ASPNETCORE_ENVIRONMENT=Development piotrosz/cfrog:1.0 
```

Build and run using Docker compose:

```
docker compose build
docker compose up [-d]
docker compose up --no-deps web
docker compose down

docker compose logs azurite

docker compose ps [-a]

docker compose start
docker compose stop
docker compose remove

```

### Store secrets in local secret storage

Init:
```PowerShell
cd ./src/CookingFrog.WebUI/CookingFrog.WebUI
dotnet user-secrets init
```
Set secrets:

```PowerShell
# Set Google authentication secrets
dotnet user-secrets set "Authentication:Google:ClientId" ""
dotnet user-secrets set "Authentication:Google:Secret" ""

# Set emails authorized to manage recipes
dotnet user-secrets set "Authorization:Emails" ""

# Set Azure table storage secrets
dotnet user-secrets set "Azure:Storage:Uri" ""
dotnet user-secrets set "Azure:Storage:AccountName" ""
dotnet user-secrets set "Azure:Storage:AccountKey" ""

# OR
dotnet user-secrets set "Azure:Storage:ConnectionString" ""

```

### Ideas:

- Use `IDistributedCache`
- Import & export recipies in json
- Add CreatedBy (email)
- Add ModifiedBy (email)
- Recipies per user
- Display and edit preparation time
