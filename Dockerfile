FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Poliedro.Client.Api/Poliedro.Client.Api.csproj", "Poliedro.Client.Api/"]
COPY ["Poliedro.Client.Application/Poliedro.Client.Application.csproj", "Poliedro.Client.Application/"]
COPY ["Poliedro.Client.Domain/Poliedro.Client.Domain.csproj", "Poliedro.Client.Domain/"]
COPY ["Poliedro.Client.Infraestructure.Persistence.Mysql/Poliedro.Client.Infraestructure.Persistence.Mysql.csproj", "Poliedro.Client.Infraestructure.Persistence.Mysql/"]
COPY ["Poliedro.Client.Common/Poliedro.Client.Common.csproj", "Poliedro.Client.Common/"]
RUN dotnet restore "./Poliedro.Client.Api/Poliedro.Client.Api.csproj"
COPY . .
WORKDIR "/src/Poliedro.Client.Api"
RUN dotnet build "./Poliedro.Client.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Poliedro.Client.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poliedro.Client.Api.dll"]