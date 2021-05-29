using System.Collections.Generic;

namespace DeliVeggie.Common.MessageTypes.ProductMessage
{
    public class ProductsPaginationResponseMessage
    {
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public IEnumerable<ProductMessageBase> Products { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; } = 200;
    }
}
