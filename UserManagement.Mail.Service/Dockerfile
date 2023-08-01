FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserManagement.Mail.Service/UserManagement.Mail.Service.csproj", "UserManagement.Mail.Service/"]
RUN dotnet restore "UserManagement.Mail.Service/UserManagement.Mail.Service.csproj"
COPY . .
WORKDIR "/src/UserManagement.Mail.Service"
RUN dotnet build "UserManagement.Mail.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Mail.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Mail.Service.dll"]
