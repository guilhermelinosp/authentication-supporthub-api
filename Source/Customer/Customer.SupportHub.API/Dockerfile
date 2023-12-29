FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7070
EXPOSE 7071

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/Customer/Customer.SupportHub.API/Customer.SupportHub.API.csproj", "Source/Customer/Customer.SupportHub.API/"]
COPY ["Source/Customer/Customer.SupportHub.Application/Customer.SupportHub.Application.csproj", "Source/Customer/Customer.SupportHub.Application/"]
COPY ["Source/Customer/Customer.SupportHub.Domain/Customer.SupportHub.Domain.csproj", "Source/Customer/Customer.SupportHub.Domain/"]
COPY ["Source/Customer/Customer.SupportHub.Infrastructure/Customer.SupportHub.Infrastructure.csproj", "Source/Customer/Customer.SupportHub.Infrastructure/"]
RUN dotnet restore "Source/Customer/Customer.SupportHub.API/Customer.SupportHub.API.csproj"
COPY . .
WORKDIR "/src/Source/Customer/Customer.SupportHub.API"
RUN dotnet build "Customer.SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Customer.SupportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.SupportHub.API.dll"]
