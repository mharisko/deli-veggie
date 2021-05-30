
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
    /// <seealso cref="IPriceReductionService" />
    public class PriceReductionService : IPriceReductionService
    {
        private readonly IPriceReductionRepository priceReductionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceReductionService"/> class.
        /// </summary>
        /// <param name="priceReductionRepository">The price reduction repository.</param>
        public PriceReductionService(IPriceReductionRepository priceReductionRepository)
        {
            this.priceReductionRepository = priceReductionRepository;
        }

        /// <summary>
        /// Adds the price reduction asynchronous.
        /// </summary>
        /// <param name="priceReduction">The price reduction.</param>
        /// <returns></returns>
        public Task AddPriceReductionAsync(PriceReductionDto priceReduction)
        {
            return this.priceReductionRepository.AddPriceReductionAsync(priceReduction);
        }

        /// <summary>
        /// Deletes the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public Task DeletePriceReductionAsync(int dayOfWeek)
        {
            return this.priceReductionRepository.DeletePriceReductionAsync(dayOfWeek);
        }

        /// <summary>
        /// Gets the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public Task<PriceReductionDto> GetPriceReductionAsync(int dayOfWeek)
        {
            return this.priceReductionRepository.GetPriceReductionAsync(dayOfWeek);
        }

        /// <summary>
        /// Gets the price reductions asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PriceReductionDto>> GetPriceReductionsAsync()
        {
            return this.priceReductionRepository.GetPriceReductionsAsync();
        }

        /// <summary>
        /// Updates the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="priceReduction">The price reduction.</param>
        /// <returns></returns>
        public Task UpdatePriceReductionAsync(int dayOfWeek, PriceReductionDto priceReduction)
        {
            return this.priceReductionRepository.UpdatePriceReductionAsync(dayOfWeek, priceReduction);
        }
    }
}
