
namespace DeliVeggie.GatewayAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DeliVeggie.Common.Infrastructure.Exceptions;
    using DeliVeggie.GatewayAPI.Models;
    using DeliVeggie.GatewayAPI.Services.Abstract;
    using DeliVeggie.GatewayAPI.Services.Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Products controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="productService">The product service.</param>
        public ProductsController(ILogger<ProductsController> logger,
            IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts(int skip, int limit)
        {
            try
            {
                var paginationResult = await this.productService.GetProductsAsync(skip, limit);
                return this.Ok(new
                {
                    Data = this.MapDtosToViewModel(paginationResult.Products),
                    paginationResult.RecordsTotal
                });
            }
            catch (HttpException ex) when (ex.StatusCode < 500)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return this.BadRequest(new { message = "Invalid product id." });
                }

                var product = await this.productService.GetProductAsync(productId);
                return this.Ok(this.MapDtoToViewModel(product));
            }
            catch (HttpException ex) when (ex.StatusCode < 500)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the product with price.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        [HttpGet("{productId}/with-reduction/dayOfWeek")]
        public async Task<IActionResult> GetProductWithPrice(string productId, int dayOfWeek)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return this.BadRequest(new { message = "Invalid product id." });
                }

                if (dayOfWeek <= 0 || dayOfWeek > 7)
                {
                    return this.BadRequest(new { message = "DayOfWeek should be greater than zero & less than 8." });
                }

                var product = await this.productService.GetProductWithPriceAsync(productId, dayOfWeek);
                return this.Ok(this.MapDtoToViewModel(product));
            }
            catch (HttpException ex) when (ex.StatusCode < 500)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds the new product.
        /// </summary>
        /// <param name="productInput">The product input.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductInputModel productInput)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var productDto = new ProductDto
                {
                    Name = productInput.Name,
                    Price = productInput.Price
                };

                await this.productService.AddNewProductAsync(productDto);
                return this.NoContent();
            }
            catch (HttpException ex) when (ex.StatusCode < 500)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="productInput">The product input.</param>
        /// <returns></returns>
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, [FromBody] ProductInputModel productInput)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var productDto = new ProductDto
                {
                    Name = productInput.Name,
                    Price = productInput.Price
                };

                await this.productService.UpdateProductAsync(productId, productDto);
                return this.NoContent();
            }
            catch (HttpException ex) when (ex.StatusCode < 500)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return this.BadRequest(new { message = "Invalid product id." });
                }

                await this.productService.DeleteProductAsync(productId);
                return this.NoContent();
            }
            catch (HttpException ex) when (ex.StatusCode < 500)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private ProductViewModel MapDtoToViewModel(ProductDto productDto)
        {
            return new ProductViewModel
            {
                EntryDate = productDto.EntryDate,
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price
            };
        }

        private IEnumerable<ProductViewModel> MapDtosToViewModel(IEnumerable<ProductDto> productDtos)
        {
            return productDtos
                ?.Select(x => this.MapDtoToViewModel(x))
                ?.ToList();
        }
    }
}
