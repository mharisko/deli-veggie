
namespace DeliVeggie.GatewayAPI.Services.Abstract
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.GatewayAPI.Services.Dto;

    /// <summary>
    /// Price reduction message bus.
    /// </summary>
    /// <seealso cref="IMessageBus" />
    public interface IPriceReductionMessageBus : IMessageBus
    {
        /// <summary>
        /// Adds the price reduction asynchronous.
        /// </summary>
        /// <param name="priceReduction">The price reduction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddPriceReductionAsync(PriceReductionDto priceReduction, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeletePriceReductionAsync(int dayOfWeek, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PriceReductionDto> GetPriceReductionAsync(int dayOfWeek, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the price reductions asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<PriceReductionDto>> GetPriceReductionsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="priceReduction">The price reduction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdatePriceReductionAsync(int dayOfWeek, PriceReductionDto priceReduction, CancellationToken cancellationToken);
    }
}
