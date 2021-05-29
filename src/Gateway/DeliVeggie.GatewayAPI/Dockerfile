FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Gateway/DeliVeggie.GatewayAPI/DeliVeggie.GatewayAPI.csproj", "Gateway/DeliVeggie.GatewayAPI/"]
COPY ["src/Common/DeliVeggie.Common.MessageTypes/DeliVeggie.Common.MessageTypes.csproj", "Common/DeliVeggie.Common.MessageTypes/"]
COPY ["src/Common/DeliVeggie.Common.Infrastructure/DeliVeggie.Common.Infrastructure.csproj", "Common/DeliVeggie.Common.Infrastructure/"]
RUN dotnet restore "Gateway/DeliVeggie.GatewayAPI/DeliVeggie.GatewayAPI.csproj"

COPY ["src/Gateway/DeliVeggie.GatewayAPI/", "Gateway/DeliVeggie.GatewayAPI/"]
COPY ["src/Common/DeliVeggie.Common.MessageTypes/", "Common/DeliVeggie.Common.MessageTypes/"]
COPY ["src/Common/DeliVeggie.Common.Infrastructure/", "Common/DeliVeggie.Common.Infrastructure/"]

WORKDIR "/src/Gateway/DeliVeggie.GatewayAPI"
RUN dotnet build "DeliVeggie.GatewayAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.GatewayAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.GatewayAPI.dll"]
