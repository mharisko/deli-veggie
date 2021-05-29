
namespace DeliVeggie.Common.MessageTypes.PriceReductionMessage
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Price reduction document.
    /// </summary>
    public class PriceReductionMessageBase
    {
        /// <summary>
        /// Gets or sets the day of week.
        /// </summary>
        /// <value>
        /// The day of week.
        /// </value>
        [Required]
        [Range(minimum: 1, maximum: 7)]
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the reduction.
        /// </summary>
        /// <value>
        /// The reduction.
        /// </value>
        [Range(minimum: 0, maximum: 100)]
        public double Reduction { get; set; }
    }
}
