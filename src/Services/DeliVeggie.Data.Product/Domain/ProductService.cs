
namespace DeliVeggie.Product.Service.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Abstract.Domain;
    using DeliVeggie.Product.Service.Abstract.Repository;
    using DeliVeggie.Product.Service.Dto;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IProductService" />
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IPriceReductionService priceReductionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="priceReductionService">The price reduction service.</param>
        public ProductService(IProductRepository productRepository,
            IPriceReductionService priceReductionService)
        {
            this.productRepository = productRepository;
            this.priceReductionService = priceReductionService;
        }

        /// <summary>
        /// Adds the new product asynchronous.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task AddNewProductAsync(ProductDto product)
        {
            return this.productRepository.AddNewProductAsync(product);
        }

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task DeleteProductAsync(string productId)
        {
            return this.productRepository.DeleteProductAsync(productId);
        }

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task<ProductDto> GetProductAsync(string productId)
        {
            return this.productRepository.GetProductAsync(productId);
        }

        /// <summary>
        /// Gets the product with price asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public async Task<ProductDto> GetProductWithPriceAsync(string productId, int dayOfWeek)
        {
            var reduction = await this.priceReductionService.GetPriceReductionAsync(dayOfWeek);
            var product = await this.productRepository.GetProductAsync(productId);
            product.Price -= product.Price * reduction.Reduction;

            return product;
        }

        /// <summary>
        /// Gets the products asynchronous.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task<IEnumerable<ProductDto>> GetProductsAsync(int skip, int limit)
        {
            return this.productRepository.GetProductsAsync(skip, limit);
        }

        /// <summary>
        /// Updates the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="product">The product.</param>
        /// <returns>Asynchronous operation.</returns>
        public Task UpdateProductAsync(string productId, ProductDto product)
        {
            return this.productRepository.UpdateProductAsync(productId, product);
        }

        /// <summary>
        /// Gets the count asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<long> GetCountAsync()
        {
            return this.productRepository.GetCountAsync();
        }
    }
}
