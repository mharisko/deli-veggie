﻿
namespace DeliVeggie.Product.Service.Abstract.MessageBus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.Common.MessageTypes.ProductMessage;
    using DeliVeggie.Product.Service.Abstract.Domain;
    using DeliVeggie.Product.Service.Dto;
    using DeliVeggie.Product.Service.Helpers;
    using EasyNetQ;
    using Microsoft.Extensions.Logging;

    public class ProductMessageBus : IProductMessageBus
    {
        private readonly IProductService productService;
        private readonly IBus messageBus;
        private readonly ILogger<ProductMessageBus> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMessageBus" /> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="logger">The logger.</param>
        public ProductMessageBus(
            IProductService productService,
            IBus messageBus,
            ILogger<ProductMessageBus> logger)
        {
            this.productService = productService;
            this.messageBus = messageBus;
            this.logger = logger;
        }

        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var createTask = this.HandleCreateRequestAsync(cancellationToken);
            var getTask = this.HandleGetProductRequestAsync(cancellationToken);
            var deleteTask = this.HandleDeleteRequestAsync(cancellationToken);
            var updateTask = this.HandleUpdateRequestAsync(cancellationToken);
            var withPriceTask = this.HandleGetProductWithPriceRequestAsync(cancellationToken);
            var paginationTask = this.HandleGetProductsRequestAsync(cancellationToken);

            await Task.WhenAll(createTask, getTask, deleteTask, updateTask, withPriceTask, paginationTask);
        }

        private async Task HandleCreateRequestAsync(CancellationToken cancellationToken)
        {
            await this.messageBus
                          .Rpc
                          .RespondAsync<ProductCreateRequestMessage, ProductCreateResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (request == null || !RequestValidationHelper.IsValid(request))
                                  {
                                      statusCode = 400;
                                  }

                                  var productToAdd = this.MapCreateRequestToDto(request);
                                  await this.productService.AddNewProductAsync(productToAdd);
                                  this.logger.LogInformation($"{productToAdd.Name} successfully created.");

                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, "Error when creating a product");
                                  statusCode = 500;
                              }

                              return new ProductCreateResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private async Task HandleUpdateRequestAsync(CancellationToken cancellationToken)
        {
            await this.messageBus
                          .Rpc
                          .RespondAsync<ProductUpdateRequestMessage, ProductUpdateResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (request == null || !RequestValidationHelper.IsValid(request))
                                  {
                                      statusCode = 400;
                                  }

                                  var productToUpdate = this.MapCreateRequestToDto(request);
                                  await this.productService.UpdateProductAsync(productToUpdate.Id, productToUpdate);
                                  this.logger.LogInformation($"{productToUpdate.Name} successfully updated.");

                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, "Error when updating a product");
                                  statusCode = 500;
                              }

                              return new ProductUpdateResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private async Task HandleDeleteRequestAsync(CancellationToken cancellationToken)
        {
            await this.messageBus
                          .Rpc
                          .RespondAsync<ProductDeleteRequestMessage, ProductDeleteResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (!string.IsNullOrEmpty(request?.ProductId))
                                  {
                                      statusCode = 400;
                                  }

                                  await this.productService.DeleteProductAsync(request.ProductId);
                                  this.logger.LogInformation($"Product {request.ProductId} successfully Deleted.");

                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, "Error when updating a product");
                                  statusCode = 500;
                              }

                              return new ProductDeleteResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private async Task HandleGetProductRequestAsync(CancellationToken cancellationToken)
        {
            await this.messageBus
                          .Rpc
                          .RespondAsync<ProductGetRequestMessage, ProductGetResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (string.IsNullOrEmpty(request?.ProductId))
                                  {
                                      statusCode = 400;
                                  }

                                  var productDto = await this.productService.GetProductAsync(request.ProductId);
                                  if (productDto != null)
                                  {
                                      return this.MapDtoToResponseMessage(productDto);
                                  }

                                  statusCode = 404;
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, $"Error when getting product: {request?.ProductId}");
                                  statusCode = 500;
                              }

                              return new ProductGetResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private async Task HandleGetProductWithPriceRequestAsync(CancellationToken cancellationToken)
        {
            await this.messageBus
                          .Rpc
                          .RespondAsync<ProductWithPriceRequestMessage, ProductWithPriceResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (string.IsNullOrEmpty(request?.ProductId))
                                  {
                                      statusCode = 400;
                                  }

                                  var productDto = await this.productService.GetProductWithPriceAsync(request.ProductId, request.DayOfWeek);
                                  if (productDto != null)
                                  {
                                      return this.MapDtoToPriceResponseMessage(productDto);
                                  }

                                  statusCode = 404;
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, $"Error when getting product: {request?.ProductId}");
                                  statusCode = 500;
                              }

                              return new ProductWithPriceResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private async Task HandleGetProductsRequestAsync(CancellationToken cancellationToken)
        {
            await this.messageBus
                          .Rpc
                          .RespondAsync<ProductsPaginationRequestMessage, ProductsPaginationResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  var skip = request == null ? 0 : request.Skip;
                                  var limit = request == null ? 20 : request.Limit;

                                  var productDtos = await this.productService.GetProductsAsync(skip, limit);
                                  if (productDtos?.Count() > 0)
                                  {
                                      return this.MapDtoToResponseMessage(productDtos);
                                  }

                                  statusCode = 404;
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, $"Error when getting products");
                                  statusCode = 500;
                              }

                              return new ProductsPaginationResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private ProductDto MapCreateRequestToDto(ProductMessageBase request)
        {
            return new ProductDto
            {
                EntryDate = request.EntryDate,
                Name = request.Name,
                Price = request.Price
            };
        }

        private ProductGetResponseMessage MapDtoToResponseMessage(ProductDto product)
        {
            return new ProductGetResponseMessage
            {
                EntryDate = product.EntryDate,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        private ProductsPaginationResponseMessage MapDtoToResponseMessage(IEnumerable<ProductDto> products)
        {
            return new ProductsPaginationResponseMessage
            {
                Products = products
                            .Select(x => MapDtoToResponseMessage(x))
                            .ToList()
            };
        }

        private ProductWithPriceResponseMessage MapDtoToPriceResponseMessage(ProductDto product)
        {
            return new ProductWithPriceResponseMessage
            {
                EntryDate = product.EntryDate,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}