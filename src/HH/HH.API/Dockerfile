#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HH.API/HH.API.csproj", "HH.API/"]
COPY ["HH.Application/HH.Application.csproj", "HH.Application/"]
COPY ["HH.Common.Contracts/HH.Common.Contracts.csproj", "HH.Common.Contracts/"]
COPY ["HH.Domain/HH.Domain.csproj", "HH.Domain/"]
COPY ["HH.Infrastructure/HH.Infrastructure.csproj", "HH.Infrastructure/"]
RUN dotnet restore "./HH.API/HH.API.csproj"
COPY . .
WORKDIR "/src/HH.API"
RUN dotnet build "./HH.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HH.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HH.API.dll"]