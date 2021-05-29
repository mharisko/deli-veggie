namespace DeliVeggie.Common.MessageTypes.ProductMessage
{
    public class ProductGetResponseMessage : ProductMessageBase
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; } = 200;
    }
}
