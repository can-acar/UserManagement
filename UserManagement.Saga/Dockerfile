﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserManagement.Saga/UserManagement.Saga.csproj", "UserManagement.Saga/"]
COPY ["UserManagement.Core/UserManagement.Core.csproj", "UserManagement.Core/"]
COPY ["UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "UserManagement.Infrastructure/"]
RUN dotnet restore "UserManagement.Saga/UserManagement.Saga.csproj"
COPY . .
WORKDIR "/src/UserManagement.Saga"
RUN dotnet build "UserManagement.Saga.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Saga.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Saga.dll"]
