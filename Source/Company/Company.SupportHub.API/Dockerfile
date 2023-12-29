FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/Company/Company.SupportHub.API/Company.SupportHub.API.csproj", "Source/Company/Company.SupportHub.API/"]
COPY ["Source/Company/Company.SupportHub.Application/Company.SupportHub.Application.csproj", "Source/Company/Company.SupportHub.Application/"]
COPY ["Source/Company/Company.SupportHub.Domain/Company.SupportHub.Domain.csproj", "Source/Company/Company.SupportHub.Domain/"]
COPY ["Source/Company/Company.SupportHub.Infrastructure/Company.SupportHub.Infrastructure.csproj", "Source/Company/Company.SupportHub.Infrastructure/"]
RUN dotnet restore "Source/Company/Company.SupportHub.API/Company.SupportHub.API.csproj"
COPY . .
WORKDIR "/src/Source/Company/Company.SupportHub.API"
RUN dotnet build "Company.SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Company.SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.SupportHub.API.dll"]
