# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Identity/Identity.Domain.Api/Identity.Domain.Api.csproj", "src/Services/Identity/Identity.Domain.Api/"]
COPY ["src/Shared/SharedLibrary/SharedLibrary.csproj", "src/Shared/SharedLibrary/"]
COPY ["src/Services/Identity/Identity.Domain.Application/Identity.Domain.Application.csproj", "src/Services/Identity/Identity.Domain.Application/"]
COPY ["src/Services/Identity/Identity.Domain.Core/Identity.Domain.Core.csproj", "src/Services/Identity/Identity.Domain.Core/"]
RUN dotnet restore "./src/Services/Identity/Identity.Domain.Api/Identity.Domain.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Identity.Domain.Api"
RUN dotnet build "./Identity.Domain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.Domain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Domain.Api.dll"]