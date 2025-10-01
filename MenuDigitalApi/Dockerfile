# ----------------------------
# Etapa 1: Build
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas csproj primeiro (cache das deps)
COPY MenuDigitalApi/*.csproj MenuDigitalApi/
COPY MenuDigital.Application/*.csproj MenuDigital.Application/
COPY MenuDigital.Domain/*.csproj MenuDigital.Domain/
COPY MenuDigital.Infrastructure/*.csproj MenuDigital.Infrastructure/

# Restaura pacotes
RUN dotnet restore MenuDigitalApi/MenuDigitalApi.csproj

# Copia o resto
COPY . .

# Publica em Release
RUN dotnet publish MenuDigitalApi/MenuDigitalApi.csproj -c Release -o /app/publish

# ----------------------------
# Etapa 2: Runtime
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/publish .


ENTRYPOINT ["dotnet", "MenuDigitalApi.dll"]
