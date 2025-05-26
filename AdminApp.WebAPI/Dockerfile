# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AdminApp.WebAPI/AdminApp.WebAPI.csproj", "AdminApp.WebAPI/"]
COPY ["AdminApp.Core/AdminApp.Core.csproj", "AdminApp.Core/"]
COPY ["AdminApp.Infrastructure/AdminApp.Infrastructure.csproj", "AdminApp.Infrastructure/"]
COPY ["AdminApp.Services/AdminApp.Services.csproj", "AdminApp.Services/"]
COPY ["AdminApp.Utils/AdminApp.Utils.csproj", "AdminApp.Utils/"]
RUN dotnet restore "./AdminApp.WebAPI/AdminApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/AdminApp.WebAPI"
RUN dotnet build "./AdminApp.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AdminApp.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminApp.WebAPI.dll"]