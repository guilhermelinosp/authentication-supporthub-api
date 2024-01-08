FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/Authentication.SupportHub.API/Authentication.SupportHub.API.csproj", "Source/Authentication.SupportHub.API/"]
COPY ["Source/Authentication.SupportHub.Application/Authentication.SupportHub.Application.csproj", "Source/Authentication.SupportHub.Application/"]
COPY ["Source/Authentication.SupportHub.Domain/Authentication.SupportHub.Domain.csproj", "Source/Authentication.SupportHub.Domain/"]
COPY ["Source/Authentication.SupportHub.Infrastructure/Authentication.SupportHub.Infrastructure.csproj", "Source/Authentication.SupportHub.Infrastructure/"]
RUN dotnet restore "Source/Authentication.SupportHub.API/Authentication.SupportHub.API.csproj"
COPY . .
WORKDIR "/src/Source/Authentication.SupportHub.API"
RUN dotnet build "Authentication.SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Authentication.SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.SupportHub.API.dll"]
