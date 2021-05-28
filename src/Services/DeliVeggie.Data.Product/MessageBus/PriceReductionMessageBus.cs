
using System.Threading;
using System.Threading.Tasks;

namespace DeliVeggie.Product.Service.Abstract.MessageBus
{
    /// <summary>
    /// 
    /// </summary>
    public class PriceReductionMessageBus : IPriceReductionMessageBus
    {
        public Task StartAsync(CancellationToken cancellationToken) => throw new System.NotImplementedException();
        public Task StopAsync(CancellationToken cancellationToken) => throw new System.NotImplementedException();
    }
}
