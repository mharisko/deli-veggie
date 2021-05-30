using System;
using System.ComponentModel.DataAnnotations;

namespace DeliVeggie.GatewayAPI.Models
{
    /// <summary>
    /// Product input model.
    /// </summary>
    public class ProductInputModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
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
