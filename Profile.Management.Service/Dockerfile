FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Profile.Management.Service/Profile.Management.Service.csproj", "Profile.Management.Service/"]
RUN dotnet restore "Profile.Management.Service/Profile.Management.Service.csproj"
COPY . .
WORKDIR "/src/Profile.Management.Service"
RUN dotnet build "Profile.Management.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile.Management.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile.Management.Service.dll"]
