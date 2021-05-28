
namespace DeliVeggie.Product.Service.Abstract.MessageBus
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageBus
    {
        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task StartAsync(CancellationToken cancellationToken);
    }
}
