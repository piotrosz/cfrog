## cfrog

Blazor web app

### To implement:

- Export recipies to json
- Import recipies from json
- CreatedBy (email)
- ModifiedBy (email)
- Favourite (my recipes)
- Display preparation time
- Be able to edit preparation time
- Link to original page
- Add/edit notes
- Add ingredient at index

docker compose build

docker compose up

`
docker run -d -p 8081:5289 -v $(pwd):/var/www -w "/var/www" mcr.microsoft.com/dotnet/sdk:9.0 bash -c "dotnet watch run --project ./src/CookingFrog.WebUI/CookingFrog.WebUI/CookingFrog.WebUI.csproj"  
`