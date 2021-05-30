FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/DeliVeggie.Product.Service/DeliVeggie.Product.Service.csproj", "Services/DeliVeggie.Product.Service/"]
COPY ["src/Common/DeliVeggie.Common.MessageTypes/DeliVeggie.Common.MessageTypes.csproj", "Common/DeliVeggie.Common.MessageTypes/"]
COPY ["src/Common/DeliVeggie.Common.Infrastructure/DeliVeggie.Common.Infrastructure.csproj", "Common/DeliVeggie.Common.Infrastructure/"]
RUN dotnet restore "Services/DeliVeggie.Product.Service/DeliVeggie.Product.Service.csproj"

COPY ["src/Services/DeliVeggie.Product.Service/", "Services/DeliVeggie.Product.Service/"]
COPY ["src/Common/DeliVeggie.Common.MessageTypes/", "Common/DeliVeggie.Common.MessageTypes/"]
COPY ["src/Common/DeliVeggie.Common.Infrastructure/", "Common/DeliVeggie.Common.Infrastructure/"]

WORKDIR "/src/Services/DeliVeggie.Product.Service"
RUN dotnet build "DeliVeggie.Product.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.Product.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.Product.Service.dll"]