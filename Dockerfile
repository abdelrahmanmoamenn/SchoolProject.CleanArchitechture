# ── Build Stage ──────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files first (layer caching)
COPY SchoolProject.CleanArchitechture.sln ./
COPY SchoolProject.Api/SchoolProject.Api.csproj                         SchoolProject.Api/
COPY SchoolProject.Core/SchoolProject.Core.csproj                       SchoolProject.Core/
COPY SchoolProject.Data/SchoolProject.Data.csproj                       SchoolProject.Data/
COPY SchoolProject.Infrastructure/SchoolProject.Infrastructure.csproj   SchoolProject.Infrastructure/
COPY SchoolProject.Service/SchoolProject.Service.csproj                 SchoolProject.Service/

# Clear Windows-specific NuGet fallback folders
RUN dotnet nuget locals all --clear
# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .

# Inject a clean NuGet.config to block Windows fallback paths
RUN printf '<?xml version="1.0" encoding="utf-8"?>\n<configuration>\n  <packageSources>\n    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />\n  </packageSources>\n  <fallbackPackageFolders>\n  </fallbackPackageFolders>\n</configuration>' > /src/NuGet.config

WORKDIR /src/SchoolProject.Api
RUN dotnet publish -c Release -o /app/publish

# ── Runtime Stage ─────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Copy resource files (.resx) needed for localization
COPY --from=build /src/SchoolProject.Core/Resources ./Resources

EXPOSE 8080
ENTRYPOINT ["dotnet", "SchoolProject.Api.dll"]