docker build -f .\src\Services\DeliVeggie.Data.Product\Dockerfile -t deliveggie-data-service:latest .

docker build -f .\src\Gateway\DeliVeggie.GatewayAPI\Dockerfile -t deliveggie-gateway-api:latest .


docker network create deliveggie

docker-compose -f "docker-compose.yml" up -d --build