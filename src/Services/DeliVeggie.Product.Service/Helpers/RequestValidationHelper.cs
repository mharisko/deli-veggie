
namespace DeliVeggie.Product.Service.Helpers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public static class RequestValidationHelper
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if the specified model is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(object model)
        {
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, validationResults, true);
        }
    }
}
