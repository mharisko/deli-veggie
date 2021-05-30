
namespace DeliVeggie.GatewayAPI.Services.Implementation
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.GatewayAPI.Services.Abstract;
    using DeliVeggie.GatewayAPI.Services.Dto;

    /// <summary>
    /// Price reduction service.
    /// </summary>
    /// <seealso cref="IPriceReductionService" />
    public class PriceReductionService : IPriceReductionService
    {
        private readonly IPriceReductionMessageBus priceReductionMessageBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceReductionService" /> class.
        /// </summary>
        /// <param name="priceReductionMessageBus">The price reduction message bus.</param>
        public PriceReductionService(IPriceReductionMessageBus priceReductionMessageBus)
        {
            this.priceReductionMessageBus = priceReductionMessageBus;
        }

        /// <summary>
        /// Adds price reduction async.
        /// </summary>
        /// <param name="priceReduction">The price reduction.</param>
        public Task AddPriceReductionAsync(PriceReductionDto priceReduction)
        {
            return this.priceReductionMessageBus.AddPriceReductionAsync(priceReduction, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public Task DeletePriceReductionAsync(int dayOfWeek)
        {
            return this.priceReductionMessageBus.DeletePriceReductionAsync(dayOfWeek, CancellationToken.None);
        }

        /// <summary>
        /// Gets the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public Task<PriceReductionDto> GetPriceReductionAsync(int dayOfWeek)
        {
            return this.priceReductionMessageBus.GetPriceReductionAsync(dayOfWeek, CancellationToken.None);
        }

        /// <summary>
        /// Gets the price reductions asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PriceReductionDto>> GetPriceReductionsAsync()
        {
            return this.priceReductionMessageBus.GetPriceReductionsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Updates the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="priceReduction">The price reduction.</param>
        /// <returns></returns>
        public Task UpdatePriceReductionAsync(int dayOfWeek, PriceReductionDto priceReduction)
        {
            return this.priceReductionMessageBus.UpdatePriceReductionAsync(dayOfWeek, priceReduction, CancellationToken.None);
        }
    }
}
