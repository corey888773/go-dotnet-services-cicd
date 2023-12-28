# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build 

# publish stage
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish 

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8001
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
