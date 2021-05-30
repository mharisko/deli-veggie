# DeliVeggie

The solution contains below items

1. WebApplication - Angular application to display product list and product details.
    Contains 2 pages 
        1. product list page
        2. product details page

    Components:
    1. product-list - to show product list.
    2. product-details - to show product details.

    services/api.service.ts
        contains functions to call web api to fetch product list and product details.
    
2. Gateway - Web api to get product details.
    1. ProductController Contains 2 apis
        1.1)  GetProducts (https://localhost:5001/product) - to get all products.
        1.2) GetProductDetails (https://localhost:5001/product/{id}) - to get details of a product.
    
    2. Services/ProductService.cs
        Service class to call product microservice to fetch product list and product details.
    3. Models\ProductModel.cs - to hold product data.

3. Service.Product - Microservice to fetch product list and product details from db.
    1. DbModels/ProductMdo - to hold product data
    2. BL\ProductLogic - Business layer to fetch product data from DAL.
    3. DAL\ProductRepository - Repository class to fetch product data from db.

4. Common  - This libraray contains models to pass product data from Gateway to Microservice.
    1. ProductDto.cs - model to pass product data.
    2. Messages\ProductRequest.cs - RabitMq communication model to pass input request
    3. Messages\ProductResponse.cs - RabitMq communication model to pass output response.

Note: Used RabitMq for communication between Getway and Microservice.

