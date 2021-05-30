
namespace DeliVeggie.GatewayAPI.Services.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.Common.Infrastructure.Exceptions;
    using DeliVeggie.Common.MessageTypes.PriceReductionMessage;
    using DeliVeggie.GatewayAPI.Services.Abstract;
    using DeliVeggie.GatewayAPI.Services.Dto;
    using EasyNetQ;

    /// <summary>
    /// Price reduction message bus.
    /// </summary>
    /// <seealso cref="IPriceReductionMessageBus" />
    public class PriceReductionMessageBus : IPriceReductionMessageBus
    {
        private readonly IBus messageBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMessageBus" /> class.
        /// </summary>
        /// <param name="messageBus">The message bus.</param>
        public PriceReductionMessageBus(IBus messageBus)
        {
            this.messageBus = messageBus;
        }

        /// <summary>
        /// Adds the price reduction asynchronous.
        /// </summary>
        /// <param name="priceReduction">The price reduction.</param>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task AddPriceReductionAsync(PriceReductionDto priceReduction, CancellationToken cancellationToken)
        {
            var message = this.MapDtoToCreateRequestMessage(priceReduction);
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<PriceReductionCreateRequestMessage, PriceReductionCreateRequestMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }
        }

        /// <summary>
        /// Deletes the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task DeletePriceReductionAsync(int dayOfWeek, CancellationToken cancellationToken)
        {
            var message = new PriceReductionDeleteRequestMessage { DayOfWeek = dayOfWeek };
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<PriceReductionDeleteRequestMessage, PriceReductionDeleteResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }
        }

        /// <summary>
        /// Gets the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task<PriceReductionDto> GetPriceReductionAsync(int dayOfWeek, CancellationToken cancellationToken)
        {
            var message = new PriceReductionGetRequestMessage { DayOfWeek = dayOfWeek };
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<PriceReductionGetRequestMessage, PriceReductionGetResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }

            return this.MapMessageToDto(response);
        }

        /// <summary>
        /// Gets the price reductions asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task<IEnumerable<PriceReductionDto>> GetPriceReductionsAsync(CancellationToken cancellationToken)
        {
            var message = new PriceReductionPaginationRequestMessage();
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<PriceReductionPaginationRequestMessage, PriceReductionPaginationResponseMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }

            return this.MapMessageToDto(response.PriceReductions);
        }

        /// <summary>
        /// Updates the price reduction asynchronous.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="priceReduction">The price reduction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="HttpException">Invalid response from data service</exception>
        public async Task UpdatePriceReductionAsync(int dayOfWeek, PriceReductionDto priceReduction, CancellationToken cancellationToken)
        {
            var message = this.MapDtoToUpdateRequestMessage(priceReduction);
            message.DayOfWeek = dayOfWeek;
            var response = await this.messageBus
                          .Rpc
                          .RequestAsync<PriceReductionUpdateRequestMessage, PriceReductionUpdateRequestMessage>(message, cancellationToken);

            if (!this.IsSuccess(response.StatusCode))
            {
                throw new HttpException((System.Net.HttpStatusCode)response.StatusCode, "Invalid response from data service");
            }
        }

        private PriceReductionDto MapMessageToDto(PriceReductionMessageBase response)
        {
            return new PriceReductionDto
            {
                DayOfWeek = response.DayOfWeek,
                Reduction = response.Reduction
            };
        }

        private IEnumerable<PriceReductionDto> MapMessageToDto(IEnumerable<PriceReductionMessageBase> priceReductions)
        {
            return priceReductions
                 .Select(x => this.MapMessageToDto(x))
                 .ToList();
        }

        private PriceReductionCreateRequestMessage MapDtoToCreateRequestMessage(PriceReductionDto priceReduction)
        {
            return new PriceReductionCreateRequestMessage
            {
                DayOfWeek = priceReduction.DayOfWeek,
                Reduction = priceReduction.Reduction
            };
        }

        private PriceReductionUpdateRequestMessage MapDtoToUpdateRequestMessage(PriceReductionDto priceReduction)
        {
            return new PriceReductionUpdateRequestMessage
            {
                DayOfWeek = priceReduction.DayOfWeek,
                Reduction = priceReduction.Reduction
            };
        }

        private bool IsSuccess(int statusCode)
        {
            return statusCode >= 200 && statusCode <= 299;
        }
    }
}
