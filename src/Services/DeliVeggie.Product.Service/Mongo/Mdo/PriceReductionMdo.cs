
using DeliVeggie.Product.Service.Mongo.Core;

namespace DeliVeggie.Product.Service.Mongo.Mdo
{
    /// <summary>
    /// Price reduction ado.
    /// </summary>
    public class PriceReductionMdo : MongoDataObject<string>
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
