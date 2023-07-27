FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["User.Registration.Service/User.Registration.Service.csproj", "User.Registration.Service/"]
RUN dotnet restore "User.Registration.Service/User.Registration.Service.csproj"
COPY . .
WORKDIR "/src/User.Registration.Service"
RUN dotnet build "User.Registration.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Registration.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Registration.Service.dll"]
