FROM registryll.azurecr.io/ti/container/images/netcore3/x-runweb:stable AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM registryll.azurecr.io/ti/container/images/netcore3/x-build:stable AS build
WORKDIR /src
COPY src .
WORKDIR /src/API.POC_MONGO.Api
RUN dotnet publish API.POC_MONGO.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.POC_MONGO.Api.dll"]