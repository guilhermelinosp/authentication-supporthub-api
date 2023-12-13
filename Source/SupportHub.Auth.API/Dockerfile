FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/SupportHub.Auth.API/SupportHub.Auth.API.csproj", "Source/SupportHub.Auth.API/"]
COPY ["Source/SupportHub.Auth.Application/SupportHub.Auth.Application.csproj", "Source/SupportHub.Auth.Application/"]
COPY ["Source/SupportHub.Auth.Domain/SupportHub.Auth.Domain.csproj", "Source/SupportHub.Auth.Domain/"]
COPY ["Source/SupportHub.Auth.Infrastructure/SupportHub.Auth.Infrastructure.csproj", "Source/SupportHub.Auth.Infrastructure/"]
RUN dotnet restore "Source/SupportHub.Auth.API/SupportHub.Auth.API.csproj"
COPY . .
WORKDIR "/src/Source/SupportHub.Auth.API"
RUN dotnet build "SupportHub.Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SupportHub.Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupportHub.Auth.API.dll"]
