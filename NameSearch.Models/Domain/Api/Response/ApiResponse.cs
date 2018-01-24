using System.Net.Http.Headers;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    ///     Api Response
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// The request URI
        /// </summary>
        public string RequestUri { get; set; }
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int? StatusCode { get; set; }
        /// <summary>
        /// The response object
        /// </summary>
        public string Content { get; set; }
    }
}
