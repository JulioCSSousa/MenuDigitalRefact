# ----------------------------
# Etapa 1: Build
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj de cada projeto (aproveita cache do restore)
COPY MenuDigitalApi.Application/MenuDigital.Application.csproj MenuDigitalApi.Application/
COPY MenuDigitalApi.Domain/MenuDigital.Domain.csproj MenuDigitalApi.Domain/
COPY MenuDigitalApi.Infrastructure/MenuDigital.Infrastructure.csproj MenuDigitalApi.Infrastructure/
COPY MenuDigitalApi.Shared/MenuDigital.Shared.csproj MenuDigitalApi.Shared/
COPY MenuDigitalApi/MenuDigitalApi.csproj MenuDigitalApi/

# Restaura pacotes
RUN dotnet restore MenuDigitalApi/MenuDigitalApi.csproj

# Publica em Release
RUN dotnet publish MenuDigitalApi/MenuDigitalApi.csproj -c Release -o /app/publish

# ----------------------------
# Etapa 2: Runtime
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:${PORT}
ENTRYPOINT ["dotnet", "MenuDigitalApi.dll"]
