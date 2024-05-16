FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Estágio de construção
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS build
WORKDIR /main

# Restaurar e copiar arquivos de projeto
COPY ["uniFlow/ControleInternet/uniFlow.csproj", "uniFlow/ControleInternet/"]
RUN dotnet restore "uniFlow/ControleInternet/uniFlow.csproj"

# Copiar e compilar o código
COPY . .
RUN dotnet build "uniFlow/ControleInternet/uniFlow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish"uniFlow/ControleInternet/uniFlow.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "uniFlow.dll"]