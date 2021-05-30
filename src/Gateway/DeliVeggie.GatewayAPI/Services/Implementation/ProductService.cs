
namespace DeliVeggie.GatewayAPI.Services.Implementation
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.GatewayAPI.Services.Abstract;
    using DeliVeggie.GatewayAPI.Services.Dto;

    /// <summary>
    /// Product service.
    /// </summary>
    /// <seealso cref="IProductService" />
    public class ProductService : IProductService
    {
        private readonly IProductMessageBus productMessageBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService" /> class.
        /// </summary>
        /// <param name="productMessageBus">The product message bus.</param>
        public ProductService(IProductMessageBus productMessageBus)
        {
            this.productMessageBus = productMessageBus;
        }

        /// <summary>
        /// Adds the new product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task AddNewProductAsync(ProductDto product)
        {
            return this.productMessageBus.AddNewProductAsync(product, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task DeleteProductAsync(string productId)
        {
            return this.productMessageBus.DeleteProductAsync(productId, CancellationToken.None);
        }

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task<ProductDto> GetProductAsync(string productId)
        {
            return this.productMessageBus.GetProductAsync(productId, CancellationToken.None);
        }

        /// <summary>
        /// Gets the product with price asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public Task<ProductDto> GetProductWithPriceAsync(string productId, int dayOfWeek)
        {
            return this.productMessageBus.GetProductWithPriceAsync(productId, dayOfWeek, CancellationToken.None);
        }

        /// <summary>
        /// Gets the products asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>
        /// Asynchronous operation.
        /// </returns>
        public Task<(IEnumerable<ProductDto> Products, long? RecordsTotal)> GetProductsAsync(int skip, int limit)
        {
            return this.productMessageBus.GetProductsAsync(skip, limit, CancellationToken.None);
        }

        /// <summary>
        /// Updates the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="product">The product.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task UpdateProductAsync(string productId, ProductDto product)
        {
            return this.productMessageBus.UpdateProductAsync(productId, product, CancellationToken.None);
        }
    }
}
