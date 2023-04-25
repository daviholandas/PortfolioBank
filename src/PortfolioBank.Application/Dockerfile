FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/PortfolioBank.Application/PortfolioBank.Application.csproj", "src/PortfolioBank.Application/"]
COPY ["src/PortfolioBank.TradeCategorization/PortfolioBank.TradeCategorization.csproj", "src/PortfolioBank.TradeCategorization/"]
RUN dotnet restore "src/PortfolioBank.Application/PortfolioBank.Application.csproj"
COPY . .
WORKDIR "/src/src/PortfolioBank.Application"
RUN dotnet build "PortfolioBank.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortfolioBank.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortfolioBank.Application.dll"]
