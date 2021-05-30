# DeliVeggie

The solution contains below items

1. WebApplication - Angular application to display product list and product details.
    Contains 2 pages
    
        1. product list page
        2. product details page

    Components:
    
        1. products - to show product list.
        2. product - to show product details.

    services/product.service.ts
        contains functions to call web api to fetch products and product details.
    
2. Gateway - Web api has endpoints execute CRUD operations on products & price reductions.

        1. Swagger Added to all the endpoints.
        2. ProductsController Contains 6 endpoints
    ![image](https://user-images.githubusercontent.com/20236391/120116850-cf817880-c1a7-11eb-8cae-9eed1a93a2d9.png)
    
        3. PriceReductionController contains 5 endpoints
    ![image](https://user-images.githubusercontent.com/20236391/120116880-f770dc00-c1a7-11eb-9c34-182f0b5869d5.png)
    
        4. All the communications via abstract instead of concrete class.
        5. Asp.net DI extension used to inject interfaces.
        6. EasyMQ used to send/receive request & responsed from/to backend service.
3. Service.Product - Microservice to manage all the CRUS operations for **products** & **price reductions**.

        1. Dotnet core hosted services used to manage requests comming to the service.
        2. All the request are validated with data anotations.
        3. All the communications via abstract instead of concrete class.
        4. Domain layer added for business validations/calculations.
        5. Respotory patterns used to communicate mongodb.

4. Common

        1. All the methods are supported asynchrounous communications to avoid high usage to CPU/RAM.
        2. Singletone patterns used on repository classess & hosted service classess.

## Deployment
-----------------------
1. Docker compose file added to deploy the applications in docker swarm.

     ![image](https://user-images.githubusercontent.com/20236391/120117126-54b95d00-c1a9-11eb-99ae-5c01d6067f92.png)
     
        Once docker compose file executed, mongodb,rabbitmq, gateway, serviceapp, 
        angular ui app will be deployed in docker swarm.

2. Kubernetes meta file added to deploy the kubernetes cluster.

    ![image](https://user-images.githubusercontent.com/20236391/120117187-921dea80-c1a9-11eb-8cd4-3bf839c84507.png)
    
        Local registry used to push docker images for kubernetes.



