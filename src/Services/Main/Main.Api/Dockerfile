# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Main/Main.Api/Main.Api.csproj", "src/Services/Main/Main.Api/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Shared/SharedLibrary/SharedLibrary.csproj", "src/Shared/SharedLibrary/"]
COPY ["src/Services/Logs/Logs.Grpc2/Logs.Grpc/Logs.Grpc.csproj", "src/Services/Logs/Logs.Grpc2/Logs.Grpc/"]
COPY ["src/Services/Logs/Logs.Core/Logs.Core.csproj", "src/Services/Logs/Logs.Core/"]
COPY ["src/Services/Logs/Logs.Domain/Logs.Domain.csproj", "src/Services/Logs/Logs.Domain/"]
COPY ["src/Services/Logs/Logs.Infrastructure/Logs.Infrastructure.csproj", "src/Services/Logs/Logs.Infrastructure/"]
COPY ["src/Services/Main/Main.IoC/Main.IoC.csproj", "src/Services/Main/Main.IoC/"]
COPY ["src/Services/Main/Main.Application/Main.Application.csproj", "src/Services/Main/Main.Application/"]
COPY ["src/Services/Main/Main.Domain/Main.Domain.csproj", "src/Services/Main/Main.Domain/"]
COPY ["src/Services/Main/Main.Infrastructure/Main.Infrastructure.csproj", "src/Services/Main/Main.Infrastructure/"]
RUN dotnet restore "./src/Services/Main/Main.Api/Main.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Main/Main.Api"
RUN dotnet build "./Main.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Main.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Main.Api.dll"]