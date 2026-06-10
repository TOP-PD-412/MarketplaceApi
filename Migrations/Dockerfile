FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Migrations/Migrations.csproj", "Migrations/"]
RUN dotnet restore "Migrations/Migrations.csproj"
COPY . .
WORKDIR "/src/Migrations"
RUN dotnet build "./Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base as update
USER root
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

FROM update AS final
USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrations.dll"]
