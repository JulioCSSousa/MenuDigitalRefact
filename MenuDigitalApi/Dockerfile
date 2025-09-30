# ----------------------------
# Etapa 1: Build
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj de cada projeto para aproveitar cache
COPY MenuDigital.Application/MenuDigital.Application.csproj MenuDigital.Application/
COPY MenuDigital.Domain/MenuDigital.Domain.csproj MenuDigital.Domain/
COPY MenuDigital.Infrastructure/MenuDigital.Infrastructure.csproj MenuDigital.Infrastructure/
COPY MenuDigital.Shared/MenuDigital.Shared.csproj MenuDigital.Shared/
COPY MenuDigitalApi/MenuDigitalApi.csproj MenuDigitalApi/

# Restaura pacotes
RUN dotnet restore MenuDigitalApi/MenuDigitalApi.csproj

# Copia o restante do código
COPY . .

# Publica em Release
RUN dotnet publish MenuDigitalApi/MenuDigitalApi.csproj -c Release -o /app/publish

# ----------------------------
# Etapa 2: Runtime
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia arquivos publicados
COPY --from=build /app/publish .

# Render usa variável de ambiente PORT
ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "MenuDigitalApi.dll"]
