
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
    /// Reduction controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reductions")]
    [ApiController]
    public class ReductionController : ControllerBase
    {
        private readonly ILogger<ReductionController> logger;
        private readonly IPriceReductionService priceReductionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReductionController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="priceReductionService">The price reduction service.</param>
        public ReductionController(ILogger<ReductionController> logger,
            IPriceReductionService priceReductionService)
        {
            this.logger = logger;
            this.priceReductionService = priceReductionService;
        }

        /// <summary>
        /// Gets the price reductions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPriceReductions()
        {
            try
            {
                var priceReductionDtos = await this.priceReductionService.GetPriceReductionsAsync();
                return this.Ok(this.MapDtosToViewModel(priceReductionDtos));
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
        /// Gets the price reduction.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        [HttpGet("{dayOfWeek:int}")]
        public async Task<IActionResult> GetPriceReduction(int dayOfWeek)
        {
            try
            {
                if (dayOfWeek <= 0 || dayOfWeek > 7)
                {
                    return this.BadRequest(new { message = "DayOfWeek should be greater than zero & less than 8." });
                }

                var priceReductionDto = await this.priceReductionService.GetPriceReductionAsync(dayOfWeek);
                return this.Ok(this.MapDtoToViewModel(priceReductionDto));
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
        /// Adds the new price reduction.
        /// </summary>
        /// <param name="inputModel">The input model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewPriceReduction([FromBody] PriceReductionInputModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var priceReductionDto = new PriceReductionDto
                {
                    DayOfWeek = inputModel.DayOfWeek,
                    Reduction = inputModel.Reduction
                };

                await this.priceReductionService.AddPriceReductionAsync(priceReductionDto);
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
        /// Updates the price reduction.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="inputModel">The input model.</param>
        /// <returns></returns>
        [HttpPut("{dayOfWeek:int}")]
        public async Task<IActionResult> UpdatePriceReduction(int dayOfWeek, [FromBody] PriceReductionInputModel inputModel)
        {
            try
            {
                if (dayOfWeek <= 0 || dayOfWeek > 7)
                {
                    return this.BadRequest(new { message = "DayOfWeek should be greater than zero & less than 8." });
                }

                var priceReductionDto = new PriceReductionDto
                {
                    Reduction = inputModel.Reduction
                };

                await this.priceReductionService.UpdatePriceReductionAsync(dayOfWeek, priceReductionDto);
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
        /// Deletes the price reduction.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        [HttpDelete("{dayOfWeek:int}")]
        public async Task<IActionResult> DeletePriceReduction(int dayOfWeek)
        {
            try
            {
                if (dayOfWeek <= 0 || dayOfWeek > 7)
                {
                    return this.BadRequest(new { message = "DayOfWeek should be greater than zero & less than 8." });
                }

                await this.priceReductionService.DeletePriceReductionAsync(dayOfWeek);
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

        private PriceReductionViewModel MapDtoToViewModel(PriceReductionDto priceReductionDto)
        {
            return new PriceReductionViewModel
            {
                DayOfWeek = priceReductionDto.DayOfWeek,
                Reduction = priceReductionDto.Reduction
            };
        }

        private IEnumerable<PriceReductionViewModel> MapDtosToViewModel(IEnumerable<PriceReductionDto> priceReductionDtos)
        {
            return priceReductionDtos
                ?.Select(x => this.MapDtoToViewModel(x))
                ?.ToList();
        }
    }
}
