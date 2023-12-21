FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore as distinct layers
COPY ["Source/SupportHub.Authentication.API/SupportHub.Authentication.API.csproj", "Source/SupportHub.Authentication.API/"]
COPY ["Source/SupportHub.Authentication.Application/SupportHub.Authentication.Application.csproj", "Source/SupportHub.Authentication.Application/"]
COPY ["Source/SupportHub.Authentication.Domain/SupportHub.Authentication.Domain.csproj", "Source/SupportHub.Authentication.Domain/"]
COPY ["Source/SupportHub.Authentication.Infrastructure/SupportHub.Authentication.Infrastructure.csproj", "Source/SupportHub.Authentication.Infrastructure/"]

# Restore NuGet packages for all projects
RUN dotnet restore "Source/SupportHub.Authentication.API/SupportHub.Authentication.API.csproj"

# Copy the entire solution
COPY . .
WORKDIR "/src/Source/SupportHub.Authentication.API"
RUN dotnet build "SupportHub.Authentication.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SupportHub.Authentication.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupportHub.Authentication.API.dll"]
