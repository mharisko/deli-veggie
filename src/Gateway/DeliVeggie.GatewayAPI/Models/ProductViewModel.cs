using System;

namespace DeliVeggie.GatewayAPI.Models
{
    public class ProductViewModel : ProductInputModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        /// <value>
        /// The entry date.
        /// </value>
        public DateTime EntryDate { get; set; }
    }
}
