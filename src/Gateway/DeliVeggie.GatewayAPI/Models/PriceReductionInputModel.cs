namespace DeliVeggie.GatewayAPI.Models
{
    /// <summary>
    /// Price reduction input model.
    /// </summary>
    public class PriceReductionInputModel
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
