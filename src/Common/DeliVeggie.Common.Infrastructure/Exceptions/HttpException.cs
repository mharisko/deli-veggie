
namespace DeliVeggie.Common.Infrastructure.Exceptions
{
    using System;
    using System.Net;

    public class HttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public HttpException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            this.HttpStatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        public HttpException(HttpStatusCode statusCode, string message)
            : base(string.IsNullOrEmpty(message) ? statusCode.ToString() : message)
        {
            this.HttpStatusCode = statusCode;
        }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode
        {
            get { return (int)this.HttpStatusCode; }
        }
    }
}
