
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LibraryWebAPI/LibraryWebAPI.csproj", "LibraryWebAPI/"]
COPY ["BankManagementLogic/BankManagementLogic.csproj", "BankManagementLogic/"]
RUN dotnet restore "LibraryWebAPI/LibraryWebAPI.csproj"
COPY . .
WORKDIR "/src/LibraryWebAPI"
RUN dotnet build "LibraryWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryWebAPI.dll"]