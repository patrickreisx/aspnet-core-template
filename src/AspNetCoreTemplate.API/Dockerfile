FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AspNetCoreTemplate.API/AspNetCoreTemplate.API.csproj", "src/AspNetCoreTemplate.API/"]
COPY ["src/AspNetCoreTemplate.Infra/AspNetCoreTemplate.Infra.csproj", "src/AspNetCoreTemplate.Infra/"]
COPY ["src/AspNetCoreTemplate.Domain/AspNetCoreTemplate.Domain.csproj", "src/AspNetCoreTemplate.Domain/"]
RUN dotnet restore "src/AspNetCoreTemplate.API/AspNetCoreTemplate.API.csproj"
COPY . .
WORKDIR "/src/src/AspNetCoreTemplate.API"
RUN dotnet build "AspNetCoreTemplate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AspNetCoreTemplate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreTemplate.API.dll"]
