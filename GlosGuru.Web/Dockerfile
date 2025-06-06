# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
ARG VERSION
WORKDIR /src
COPY ["GlosGuru.Web/GlosGuru.Web.csproj", "GlosGuru.Web/"]
#COPY ["GlosGuru.ServiceDefaults/GlosGuru.ServiceDefaults.csproj", "GlosGuru.ServiceDefaults/"]
RUN dotnet restore "./GlosGuru.Web/GlosGuru.Web.csproj" --arch $TARGETARCH
COPY . .
WORKDIR "/src/GlosGuru.Web"
RUN dotnet build "./GlosGuru.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build --arch $TARGETARCH -p:AssemblyVersion=$VERSION

# This stage is used to publish the service project to be copied to the final stage
FROM --platform=$BUILDPLATFORM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
ARG VERSION
RUN dotnet publish "./GlosGuru.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --arch $TARGETARCH -p:AssemblyVersion=$VERSION

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlosGuru.Web.dll"]
