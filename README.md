## 🐸🐸🐸 cfrog 🐸🐸🐸

Blazor web app. 
Uses `InteractiveServer` and `InteractiveWebAssembly` render modes: [link](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-9.0)


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

### Ideas:

- Export recipies to json
- Import recipies from json
- CreatedBy (email)
- ModifiedBy (email)
- Favourite (my recipes)
- Display preparation time
- Be able to edit preparation time
- Link to original page
- Add ingredient at index
