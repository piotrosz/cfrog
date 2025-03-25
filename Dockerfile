FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

LABEL author="piotrosz"

WORKDIR /app

EXPOSE 80
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["./src/", "."]
RUN dotnet restore "CookingFrog.WebUI/CookingFrog.WebUI/CookingFrog.WebUI.csproj"

COPY . .
RUN dotnet build "CookingFrog.WebUI/CookingFrog.WebUI/CookingFrog.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CookingFrog.WebUI/CookingFrog.WebUI/CookingFrog.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookingFrog.WebUI.dll"]