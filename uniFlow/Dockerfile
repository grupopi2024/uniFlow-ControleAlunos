# Estágio de construção
FROM http://mcr.microsoft.com/dotnet/aspnet:7.0 AS build
WORKDIR /main

# Restaurar e copiar arquivos de projeto
COPY ["ControleInternet/uniFlow.csproj", "uniFlow/"]
RUN nuget restore "uniFlow.csproj"

# Copiar e compilar o código
COPY . .
RUN msbuild "uniFlow.csproj" /p:Configuration=Release /p:OutputPath=/app/build

# Etapa de publicação
FROM http://mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /inetpub/wwwroot
COPY --from=build /app/build .

# Configuração final
FROM http://mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /inetpub/wwwroot
COPY --from=publish /inetpub/wwwroot .

ENTRYPOINT ["C:\\inetpub\\wwwroot\\uniFlow.exe"]