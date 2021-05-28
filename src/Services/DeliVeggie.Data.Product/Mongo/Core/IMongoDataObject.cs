using MongoDB.Bson.Serialization.Attributes;

namespace DeliVeggie.Product.Service.Mongo.Core
{
    /// <summary>
    /// Generic Entity interface.
    /// </summary>
    /// <typeparam name="TKey">The type used for the entity's Id.</typeparam>
    public interface IMongoDataObject<TKey>
    {
        /// <summary>
        /// Gets or sets the Id of the Entity.
        /// </summary>
        /// <value>Id of the Entity.</value>
        [BsonId]
        TKey Id { get; set; }
    }
    
}
