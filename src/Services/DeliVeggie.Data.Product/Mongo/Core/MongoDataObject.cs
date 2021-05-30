using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeliVeggie.Product.Service.Mongo.Core
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public class MongoDataObject<Tkey> : IMongoDataObject<Tkey>
    {
        /// <summary>
        /// Gets or sets the Id of the Entity.
        /// </summary>
        /// <value>Id of the Entity.</value>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public virtual Tkey Id { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }
    }
}
