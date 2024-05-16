FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# ... outras etapas de construção

FROM build AS publish
WORKDIR /main  # Diretório de trabalho dentro da etapa de construção
COPY ["uniFlow/ControleInternet/uniFlow.csproj", "."]
RUN dotnet restore "uniFlow/ControleInternet/uniFlow.csproj"
COPY . .
RUN dotnet build "uniFlow/ControleInternet/uniFlow.csproj" -c Release -o /app/build

# ... outras etapas de publicação
