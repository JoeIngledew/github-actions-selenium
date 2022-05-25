FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM node:lts-alpine AS node_base
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env

COPY --from=node_base . .
WORKDIR /src
COPY . ./
RUN dotnet restore

FROM build-env AS publish
RUN dotnet publish -c Release -o out

FROM base AS final
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
WORKDIR /app
COPY --from=publish /src/out .
ENTRYPOINT ["dotnet", "WebApp.dll"]
