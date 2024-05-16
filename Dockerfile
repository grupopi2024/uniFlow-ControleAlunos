FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /main
COPY ["uniFlow/ControleInternet/uniFlow.csproj", "."]
RUN dotnet restore "uniFlow/ControleInternet/uniFlow.csproj"
COPY . .

RUN dotnet build "uniFlow/ControleInternet/uniFlow.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish"uniFlow/ControleInternet/uniFlow.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "uniFlow.dll"]
