
namespace DeliVeggie.Product.Service.Abstract.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Dto;

    public interface IProductRepository
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
        Task<long> UpdateProductAsync(string productId, ProductDto product);

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<long> DeleteProductAsync(string productId);

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        Task<ProductDto> GetProductAsync(string productId);

        /// <summary>
        /// Gets the products asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductsAsync(int skip, int limit);

        /// <summary>
        /// Gets the count asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<long> GetCountAsync();
    }
}
