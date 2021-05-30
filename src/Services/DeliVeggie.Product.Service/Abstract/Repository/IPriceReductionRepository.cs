
namespace DeliVeggie.Product.Service.Abstract.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Dto;

    public interface IPriceReductionRepository
    {
        /// <summary>
        /// Adds the price reduction asynchronous.
        /// </summary>
        /// <param name="priceReduction">The price reduction.</param>
        /// <returns></returns>
        Task AddPriceReductionAsync(PriceReductionDto priceReduction);

        /// <summary>
        /// Updates the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="priceReduction">The price reduction.</param>
        /// <returns></returns>
        Task UpdatePriceReductionAsync(int dayOfWeek, PriceReductionDto priceReduction);

        /// <summary>
        /// Deletes the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        Task DeletePriceReductionAsync(int dayOfWeek);

        /// <summary>
        /// Gets the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        Task<PriceReductionDto> GetPriceReductionAsync(int dayOfWeek);

        /// <summary>
        /// Gets the price reductions asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PriceReductionDto>> GetPriceReductionsAsync();
    }
}
