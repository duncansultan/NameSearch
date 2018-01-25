using System.Net.Http.Headers;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    ///     Api Response
    /// </summary>
    public interface IApiResponse
    {
        /// <summary>
        /// The request URI
        /// </summary>
        string RequestUri { get; set; }
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        int? StatusCode { get; set; }
        /// <summary>
        /// The response object
        /// </summary>
        string Content { get; set; }
    }
}
