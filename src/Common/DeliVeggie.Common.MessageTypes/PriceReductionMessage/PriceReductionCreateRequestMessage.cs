namespace DeliVeggie.Common.MessageTypes.PriceReductionMessage
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PriceReductionMessageBase" />
    public class PriceReductionCreateRequestMessage : PriceReductionMessageBase
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; } = 204;
    }
}
