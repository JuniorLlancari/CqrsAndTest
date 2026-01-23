# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CQRS.Api/CQRS.Api.csproj", "src/CQRS.Api/"]
COPY ["src/CQRS.Application/CQRS.Application.csproj", "src/CQRS.Application/"]
COPY ["src/CQRS.Domain/CQRS.Domain.csproj", "src/CQRS.Domain/"]
COPY ["src/CQRS.Common/CQRS.Common.csproj", "src/CQRS.Common/"]
COPY ["src/CQRS.External/CQRS.External.csproj", "src/CQRS.External/"]
COPY ["src/CQRS.Persistence/CQRS.Persistence.csproj", "src/CQRS.Persistence/"]
RUN dotnet restore "./src/CQRS.Api/CQRS.Api.csproj"
COPY . .
WORKDIR "/src/src/CQRS.Api"
RUN dotnet build "./CQRS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CQRS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRS.Api.dll"]