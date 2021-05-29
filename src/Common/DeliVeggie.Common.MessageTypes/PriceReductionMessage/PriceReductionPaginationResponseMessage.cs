
namespace DeliVeggie.Common.MessageTypes.PriceReductionMessage
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class PriceReductionPaginationResponseMessage
    {
        /// <summary>
        /// Gets or sets the price reductions.
        /// </summary>
        /// <value>
        /// The price reductions.
        /// </value>
        public IEnumerable<PriceReductionMessageBase> PriceReductions { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; } = 200;
    }
}
