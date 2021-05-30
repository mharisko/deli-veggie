using System.Linq;
using MongoDB.Driver;

namespace DeliVeggie.Product.Service.Mongo.Core.Repository
{
    public interface IMongoRepository<T, TKey> : IQueryable<T>
        where T : IMongoDataObject<TKey>
    {
        /// <summary>
        /// Gets the Mongo collection (to perform advanced operations).
        /// </summary>
        /// <remarks>
        /// One can argue that exposing this property (and with that, access to it's Database property for instance
        /// (which is a "parent")) is not the responsibility of this class. Use of this property is highly discouraged;
        /// for most purposes you can use the MongoRepositoryManager&lt;T&gt;
        /// </remarks>
        /// <value>The Mongo collection (to perform advanced operations).</value>
        IMongoCollection<T> Collection { get; }

    }
}
