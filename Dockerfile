# Estágio de construção
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# Restaurar e copiar arquivos de projeto
COPY uniFlow/ControleInternet/*.csproj ./
RUN nuget restore uniFlow.csproj

# Copiar e compilar o código
COPY . .
RUN msbuild uniFlow.csproj /p:Configuration=Release /p:OutputPath=out

# Estágio de publicação
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS publish
WORKDIR /inetpub/wwwroot
COPY --from=build /app/out .

# Configurações finais
EXPOSE 80
ENTRYPOINT ["C:\\inetpub\\wwwroot\\uniFlow.exe"]