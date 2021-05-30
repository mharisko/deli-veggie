
namespace DeliVeggie.Product.Service.Mongo.Mdo
{
    using System;
    using DeliVeggie.Product.Service.Mongo.Core;

    /// <summary>
    /// Product ado.
    /// </summary>
    public class ProductMdo : MongoDataObject<string>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public double Price { get; set; }
    }
}
