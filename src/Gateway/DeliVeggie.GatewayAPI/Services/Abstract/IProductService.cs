
namespace DeliVeggie.GatewayAPI.Services.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    /// <summary>
    /// Product service.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Adds the new product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task AddNewProductAsync(ProductDto product);

        /// <summary>
        /// Updates the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task UpdateProductAsync(string productId, ProductDto product);

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task DeleteProductAsync(string productId);

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<ProductDto> GetProductAsync(string productId);

        /// <summary>
        /// Gets the product with price asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        Task<ProductDto> GetProductWithPriceAsync(string productId, int dayOfWeek);

        /// <summary>
        /// Gets the products asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<(IEnumerable<ProductDto> Products, long? RecordsTotal)> GetProductsAsync(int skip, int limit);
    }
}
