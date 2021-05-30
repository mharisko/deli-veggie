
namespace DeliVeggie.GatewayAPI.Services.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.Common.Infrastructure.Exceptions;
    using DeliVeggie.Common.MessageTypes.ProductMessage;
    using DeliVeggie.GatewayAPI.Services.Abstract;
    using DeliVeggie.GatewayAPI.Services.Dto;
    using EasyNetQ;

    /// <summary>
    /// Product message bus.
    /// </summary>
    /// <seealso cref="IProductMessageBus" />
    public class ProductMessageBus : IProductMessageBus
    {
        private readonly IBus messageBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMessageBus" /> class.
        /// </summary>
        /// <param name="messageBus">The message bus.</param>
        public ProductMessageBus(IBus messageBus)
        {
            this.messageBus = messageBus;
        }

        /// <summary>
        /// Handles the create request asynchronous.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task AddNewProductAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            var message = this.MapDtoToCreatedRequestMessage(productDto);
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<ProductCreateRequestMessage, ProductCreateResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }
        }

        /// <summary>
        /// Handles the update request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="productDto">The product dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task UpdateProductAsync(string productId, ProductDto productDto, CancellationToken cancellationToken)
        {
            var message = this.MapDtoToUpdateRequestMessage(productDto);
            message.Id = productId;
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<ProductUpdateRequestMessage, ProductUpdateResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }
        }

        /// <summary>
        /// Handles the delete request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task DeleteProductAsync(string productId, CancellationToken cancellationToken)
        {
            var message = new ProductDeleteRequestMessage { ProductId = productId };
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<ProductDeleteRequestMessage, ProductDeleteResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }
        }

        /// <summary>
        /// Handles the get product request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task<ProductDto> GetProductAsync(string productId, CancellationToken cancellationToken)
        {
            var message = new ProductGetRequestMessage { ProductId = productId };
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<ProductGetRequestMessage, ProductGetResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }

            return this.MapMessageToDto(response);
        }

        /// <summary>
        /// Handles the get product with price request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ProductDto> GetProductWithPriceAsync(string productId, int dayOfWeek, CancellationToken cancellationToken)
        {
            var message = new ProductWithPriceRequestMessage { ProductId = productId, DayOfWeek = dayOfWeek };
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<ProductWithPriceRequestMessage, ProductWithPriceResponseMessage>(message, cancellationToken);

            return this.MapMessageToDto(response);
        }

        /// <summary>
        /// Handles the get products request asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task<(IEnumerable<ProductDto> Products, long? RecordsTotal)> GetProductsAsync(int skip, int limit, CancellationToken cancellationToken)
        {
            var message = new ProductsPaginationRequestMessage { Skip = skip, Limit = limit };
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<ProductsPaginationRequestMessage, ProductsPaginationResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }

            return (this.MapMessageToDto(response.Products), response.RecordsTotal);
        }

        private ProductDto MapMessageToDto(ProductMessageBase response)
        {
            return new ProductDto
            {
                Id = response.Id,
                EntryDate = response.EntryDate,
                Name = response.Name,
                Price = response.Price
            };
        }

        private IEnumerable<ProductDto> MapMessageToDto(IEnumerable<ProductMessageBase> response)
        {
            return response
                 .Select(x => this.MapMessageToDto(x))
                 .ToList();
        }

        private ProductCreateRequestMessage MapDtoToCreatedRequestMessage(ProductDto product)
        {
            return new ProductCreateRequestMessage
            {
                EntryDate = product.EntryDate,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        private ProductUpdateRequestMessage MapDtoToUpdateRequestMessage(ProductDto product)
        {
            return new ProductUpdateRequestMessage
            {
                EntryDate = product.EntryDate,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        private bool IsSuccess(int statusCode)
        {
            return statusCode >= 200 && statusCode <= 299;
        }
    }
}
