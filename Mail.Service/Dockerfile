FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mail.Service/Mail.Service.csproj", "Mail.Service/"]
RUN dotnet restore "Mail.Service/Mail.Service.csproj"
COPY . .
WORKDIR "/src/Mail.Service"
RUN dotnet build "Mail.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mail.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mail.Service.dll"]
