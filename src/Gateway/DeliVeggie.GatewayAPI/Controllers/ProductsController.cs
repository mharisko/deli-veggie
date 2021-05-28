using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeliVeggie.GatewayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return this.Ok();
        }
    }
}
