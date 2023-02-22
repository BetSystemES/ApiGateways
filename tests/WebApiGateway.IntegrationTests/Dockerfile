#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tests/WebApiGateway.IntegrationTests/WebApiGateway.IntegrationTests.csproj", "tests/WebApiGateway.IntegrationTests/"]
RUN dotnet restore "tests/WebApiGateway.IntegrationTests/WebApiGateway.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/tests/WebApiGateway.IntegrationTests"
RUN dotnet build "WebApiGateway.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiGateway.IntegrationTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiGateway.IntegrationTests.dll"]