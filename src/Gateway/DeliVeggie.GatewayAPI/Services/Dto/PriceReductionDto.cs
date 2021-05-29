
namespace DeliVeggie.GatewayAPI.Services.Dto
{
    /// <summary>
    /// Price reduction document.
    /// </summary>
    public class PriceReductionDto
    {
        /// <summary>
        /// Gets or sets the day of week.
        /// </summary>
        /// <value>
        /// The day of week.
        /// </value>
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the reduction.
        /// </summary>
        /// <value>
        /// The reduction.
        /// </value>
        public double Reduction { get; set; }
    }
}
