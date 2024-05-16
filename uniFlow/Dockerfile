# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Restaurar e compilar o código
COPY ["ControleInternet/uniFlow.csproj", "."]
RUN dotnet restore "uniFlow.csproj"
COPY . .
RUN msbuild "uniFlow.csproj" /p:Configuration=Release /p:OutputPath=/app/build

# Etapa de publicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /inetpub/wwwroot
COPY --from=build /app/build .

# Configuração final
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /inetpub/wwwroot
COPY --from=publish /inetpub/wwwroot .

ENTRYPOINT ["dotnet", "uniFlow.dll"]
