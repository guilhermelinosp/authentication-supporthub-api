FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore as distinct layers
COPY ["Source/SupportHub.API/SupportHub.API.csproj", "Source/SupportHub.API/"]
COPY ["Source/SupportHub.Application/SupportHub.Application.csproj", "Source/SupportHub.Application/"]
COPY ["Source/SupportHub.Domain/SupportHub.Domain.csproj", "Source/SupportHub.Domain/"]
COPY ["Source/SupportHub.Infrastructure/SupportHub.Infrastructure.csproj", "Source/SupportHub.Infrastructure/"]

# Restore NuGet packages for all projects
RUN dotnet restore "Source/SupportHub.API/SupportHub.API.csproj"

# Copy the entire solution
COPY . .
WORKDIR "/src/Source/SupportHub.API"
RUN dotnet build "SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupportHub.API.dll"]
