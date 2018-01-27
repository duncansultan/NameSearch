using Newtonsoft.Json;
using System.Collections.Generic;

namespace NameSearch.Models.Domain.Api.Response
{
    /// <summary>
    /// WhitePages Find Person Api Search Result
    /// Identify, enhance, and update records from a single data attribute. Leverage over 30+ years of address history, 600M+ phone-to-person matches, 275M unique person-to-address matches, and more. A Find Person request allows you to identify a single person and find details including demographics, location and phones associated with that person. Using a RESTful GET API request, you’ll receive every record related to the name given. Of course, the more complete the information given the more specific the results.
    /// See https://pro.whitepages.com/developer/documentation/find-person-api/
    /// </summary>
    public class FindPersonResponse : IFindPersonResponse
    {
        /// <summary>
        /// Gets or sets the Total number of people found.
        /// </summary>
        /// <value>
        /// The Total number of people found.
        /// </value>
        [JsonProperty("count_person")]
        public int CountPerson { get; set; }
        /// <summary>
        /// Gets or sets the person Array of objects.
        /// </summary>
        /// <value>
        /// The person Array of objects.
        /// </value>
        [JsonProperty("person")]
        public IList<IPerson> Person { get; set; }
        /// <summary>
        /// Gets or sets the Array o warnings messages describing search and input address validation. Possible values are:
        /// </summary>
        /// <value>
        /// The Array o warnings messages describing search and input address validation. Possible values are:
        /// </value>
        /// <example>
        /// Missing Input Name
        /// Missing Input Address
        /// Invalid Country Code
        /// International Address
        /// Partial Address
        /// </example>
        [JsonProperty("warnings")]
        public string Warnings { get; set; }
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
