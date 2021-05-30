### For k8s

# Start docker local registry
docker run -d -p 5000:5000 --restart=always --name registry registry:2

# Build & Push application docker images
docker build -f .\src\Gateway\DeliVeggie.GatewayAPI\Dockerfile -t deliveggie-gateway-api:latest .
docker tag deliveggie-gateway-api localhost:5000/deliveggie-gateway-api:latest
docker push localhost:5000/deliveggie-gateway-api:latest

docker build -f .\src\Services\DeliVeggie.Product.Service\Dockerfile -t  deliveggie-product-service:latest .
docker tag deliveggie-product-service localhost:5000/deliveggie-product-service:latest
docker push localhost:5000/deliveggie-product-service:latest

docker build -f .\src\Presentation\DeliVeggieUI\Dockerfile -t  deliveggie-product-ui:latest .
docker tag deliveggie-product-ui localhost:5000/deliveggie-product-ui:latest
docker push localhost:5000/deliveggie-product-ui:latest

# Deploy Mongo & RabbitMq service, then exceute below step to deploy applications in k8s
kubectl apply -f .\deployment\deliveggie.yml