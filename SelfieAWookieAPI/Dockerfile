FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SelfieAWookieAPI/SelfieAWookieAPI.csproj", "SelfieAWookieAPI/"]
RUN dotnet restore "SelfieAWookieAPI/SelfieAWookieAPI.csproj"
COPY . .
WORKDIR "/src/SelfieAWookieAPI"
RUN dotnet build "SelfieAWookieAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SelfieAWookieAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SelfieAWookieAPI.dll"]
