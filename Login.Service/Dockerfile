FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Login.Procress.Service/Login.Procress.Service.csproj", "Login.Procress.Service/"]
RUN dotnet restore "Login.Procress.Service/Login.Procress.Service.csproj"
COPY . .
WORKDIR "/src/Login.Procress.Service"
RUN dotnet build "Login.Procress.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Login.Procress.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Login.Procress.Service.dll"]
