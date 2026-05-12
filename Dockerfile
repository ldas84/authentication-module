FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar SOLO los csproj
COPY ./src/Auth.Api/Auth.Api.csproj ./Auth.Api/
COPY ./src/Auth.Application/Auth.Application.csproj ./Auth.Application/
COPY ./src/Auth.Infrastructure/Auth.Infrastructure.csproj ./Auth.Infrastructure/
COPY ./src/Auth.Domain/Auth.Domain.csproj ./Auth.Domain/

RUN cat Auth.Api/Auth.Api.csproj

RUN dotnet restore ./Auth.Api/Auth.Api.csproj

COPY ./src/ .

RUN dotnet publish ./Auth.Api/Auth.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]

