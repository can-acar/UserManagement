FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Architectures/Architectures.csproj", "Architectures/"]
RUN dotnet restore "Architectures/Architectures.csproj"
COPY . .
WORKDIR "/src/Architectures"
RUN dotnet build "Architectures.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Architectures.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Architectures.dll"]
