

namespace DeliVeggie.Product.Service.Abstract.MessageBus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.Common.MessageTypes.PriceReductionMessage;
    using DeliVeggie.Product.Service.Abstract.Domain;
    using DeliVeggie.Product.Service.Dto;
    using DeliVeggie.Product.Service.Helpers;
    using EasyNetQ;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// 
    /// </summary>
    internal class PriceReductionMessageBusService : IPriceReductionMessageBusService
    {
        private readonly IPriceReductionService priceReductionService;
        private readonly IBus messageBus;
        private readonly ILogger<PriceReductionMessageBusService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceReductionMessageBusService"/> class.
        /// </summary>
        /// <param name="priceReductionService">The price reduction service.</param>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="logger">The logger.</param>
        public PriceReductionMessageBusService(
            IPriceReductionService priceReductionService,
            IBus messageBus,
            ILogger<PriceReductionMessageBusService> logger)
        {
            this.priceReductionService = priceReductionService;
            this.messageBus = messageBus;
            this.logger = logger;
        }

        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("PriceReductionMessageBus Started.");

            try
            {
                var createTask = this.HandleCreateRequestAsync(cancellationToken);
                var getTask = this.HandleGetPriceReductionRequestAsync(cancellationToken);
                var deleteTask = this.HandleDeleteRequestAsync(cancellationToken);
                var updateTask = this.HandleUpdateRequestAsync(cancellationToken);
                var paginationTask = this.HandleGetPriceReductionsRequestAsync(cancellationToken);

                return Task.WhenAll(createTask, getTask, deleteTask, updateTask, paginationTask);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("PriceReductionMessageBus Stopping.");
            return Task.CompletedTask;
        }

        private Task HandleCreateRequestAsync(CancellationToken cancellationToken)
        {
            return this.messageBus
                            .Rpc
                            .RespondAsync<PriceReductionCreateRequestMessage, PriceReductionCreateResponseMessage>(async request =>
                            {
                                var statusCode = 204;
                                try
                                {
                                    if (request == null || !RequestValidationHelper.IsValid(request))
                                    {
                                        statusCode = 400;
                                    }
                                    else
                                    {
                                        var priceReductionToAdd = this.MapCreateRequestToDto(request);
                                        await this.priceReductionService.AddPriceReductionAsync(priceReductionToAdd);
                                        this.logger.LogInformation($"PriceReduction successfully created.");
                                    }

                                }
                                catch (System.Exception ex)
                                {
                                    this.logger.LogError(ex, "Error when creating a product");
                                    statusCode = 500;
                                }

                                return new PriceReductionCreateResponseMessage { StatusCode = statusCode };

                            }, cancellationToken);
        }

        private Task HandleUpdateRequestAsync(CancellationToken cancellationToken)
        {
            return this.messageBus
                          .Rpc
                          .RespondAsync<PriceReductionUpdateRequestMessage, PriceReductionUpdateResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (request == null || !RequestValidationHelper.IsValid(request))
                                  {
                                      statusCode = 400;
                                  }
                                  else
                                  {
                                      var priceReductionToUpdate = this.MapCreateRequestToDto(request);
                                      await this.priceReductionService.UpdatePriceReductionAsync(priceReductionToUpdate.DayOfWeek, priceReductionToUpdate);
                                      this.logger.LogInformation($"PriceReduction successfully updated.");
                                  }
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, "Error when updating a product");
                                  statusCode = 500;
                              }

                              return new PriceReductionUpdateResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private Task HandleDeleteRequestAsync(CancellationToken cancellationToken)
        {
            return this.messageBus
                          .Rpc
                          .RespondAsync<PriceReductionDeleteRequestMessage, PriceReductionDeleteResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (request?.DayOfWeek >= 0)
                                  {
                                      await this.priceReductionService.DeletePriceReductionAsync(request.DayOfWeek);
                                      this.logger.LogInformation($"Price reduction {request.DayOfWeek} successfully Deleted.");
                                  }
                                  else
                                  {
                                      statusCode = 400;
                                  }
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, "Error when updating a price reduction");
                                  statusCode = 500;
                              }

                              return new PriceReductionDeleteResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private Task HandleGetPriceReductionRequestAsync(CancellationToken cancellationToken)
        {
            return this.messageBus
                          .Rpc
                          .RespondAsync<PriceReductionGetRequestMessage, PriceReductionGetResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  if (request?.DayOfWeek >= 0)
                                  {
                                      var priceReductionDto = await this.priceReductionService.GetPriceReductionAsync(request.DayOfWeek);
                                      if (priceReductionDto != null)
                                      {
                                          return this.MapDtoToResponseMessage(priceReductionDto);
                                      }

                                      statusCode = 404;
                                  }
                                  else
                                  {
                                      statusCode = 400;

                                  }
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, $"Error when getting price reduction: {request?.DayOfWeek}");
                                  statusCode = 500;
                              }

                              return new PriceReductionGetResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private Task HandleGetPriceReductionsRequestAsync(CancellationToken cancellationToken)
        {
            return this.messageBus
                          .Rpc
                          .RespondAsync<PriceReductionPaginationRequestMessage, PriceReductionPaginationResponseMessage>(async request =>
                          {
                              var statusCode = 200;
                              try
                              {
                                  var priceReductionDtos = await this.priceReductionService.GetPriceReductionsAsync();
                                  if (priceReductionDtos?.Count() > 0)
                                  {
                                      return this.MapDtoToResponseMessage(priceReductionDtos);
                                  }

                                  statusCode = 404;
                              }
                              catch (System.Exception ex)
                              {
                                  this.logger.LogError(ex, $"Error when getting products");
                                  statusCode = 500;
                              }

                              return new PriceReductionPaginationResponseMessage { StatusCode = statusCode };

                          }, cancellationToken);
        }

        private PriceReductionDto MapCreateRequestToDto(PriceReductionMessageBase request)
        {
            return new PriceReductionDto
            {
                DayOfWeek = request.DayOfWeek,
                Reduction = request.Reduction
            };
        }

        private PriceReductionGetResponseMessage MapDtoToResponseMessage(PriceReductionDto priceReductionDto)
        {
            return new PriceReductionGetResponseMessage
            {
                DayOfWeek = priceReductionDto.DayOfWeek,
                Reduction = priceReductionDto.Reduction
            };
        }

        private PriceReductionPaginationResponseMessage MapDtoToResponseMessage(IEnumerable<PriceReductionDto> priceReductionDtos)
        {
            return new PriceReductionPaginationResponseMessage
            {
                PriceReductions = priceReductionDtos
                            .Select(x => this.MapDtoToResponseMessage(x))
                            .ToList()
            };
        }
    }
}
