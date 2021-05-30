
namespace DeliVeggie.GatewayAPI.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.GatewayAPI.Services.Dto;
    
    /// <summary>
    /// Product message bus.
    /// </summary>
    /// <seealso cref="IMessageBus" />
    public interface IProductMessageBus : IMessageBus
    {
        /// <summary>
        /// Handles the create request asynchronous.
        /// </summary>
        /// <param name="productDto">The product dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddNewProductAsync(ProductDto productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Handles the update request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="productDto">The product dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateProductAsync(string productId, ProductDto productDto, CancellationToken cancellationToken);

        /// <summary>
        /// Handles the delete request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteProductAsync(string productId, CancellationToken cancellationToken);

        /// <summary>
        /// Handles the get product request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ProductDto> GetProductAsync(string productId, CancellationToken cancellationToken);

        /// <summary>
        /// Handles the get product with price request asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ProductDto> GetProductWithPriceAsync(string productId, int dayOfWeek, CancellationToken cancellationToken);

        /// <summary>
        /// Handles the get products request asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<(IEnumerable<ProductDto> Products, long? RecordsTotal)> GetProductsAsync(int skip, int limit, CancellationToken cancellationToken);
    }
}
