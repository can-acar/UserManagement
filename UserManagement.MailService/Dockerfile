FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserManagement.MailService/UserManagement.MailService.csproj", "UserManagement.MailService/"]
RUN dotnet restore "UserManagement.MailService/UserManagement.MailService.csproj"
COPY . .
WORKDIR "/src/UserManagement.MailService"
RUN dotnet build "UserManagement.MailService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.MailService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.MailService.dll"]
