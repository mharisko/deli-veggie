FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Gateway/DeliVeggie.GatewayAPI/DeliVeggie.GatewayAPI.csproj", "src/Gateway/DeliVeggie.GatewayAPI/"]
RUN dotnet restore "src/Gateway/DeliVeggie.GatewayAPI/DeliVeggie.GatewayAPI.csproj"
COPY . .
WORKDIR "/src/src/Gateway/DeliVeggie.GatewayAPI"
RUN dotnet build "DeliVeggie.GatewayAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.GatewayAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.GatewayAPI.dll"]
