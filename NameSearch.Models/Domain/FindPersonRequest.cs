using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NameSearch.Models.Entities.Abstracts;

namespace NameSearch.Models.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="NameSearch.Models.Entities.Abstracts.AddressBase" />
    public class FindPersonRequest : AddressBase
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// To the json.
        /// </summary>
        /// <returns></returns>
        //public string ToRequestQueryString()
        //{
            //string queryString;

            //var queryStringParams = new List<KeyValuePair<string, string>>
            //{
            //    new KeyValuePair<string, string>("api_key", ApiKey),
            //    new KeyValuePair<string, string>("name", Name),
            //    new KeyValuePair<string, string>("address.street_line_1", Address1),
            //    new KeyValuePair<string, string>("address.street_line_2", Address2),
            //    new KeyValuePair<string, string>("address.city", City),
            //    new KeyValuePair<string, string>("address.postal_code", Zip),
            //    new KeyValuePair<string, string>("address.state_code", State),
            //    new KeyValuePair<string, string>("address.country_code", Country)
            //};

            //if (string.IsNullOrWhiteSpace(ApiKey)) throw new ArgumentNullException(ApiKey, "ApiKey Property must have a value");
            //if (queryStringParams.Where(x => x.Key != "api_key").All(x => string.IsNullOrWhiteSpace(x.Value))) throw new ArgumentException("At least one query string parameter must have a value.");

            //var queryStringFragments = queryStringParams
            //    .Where(x => !string.IsNullOrWhiteSpace(x.Value))
            //    .Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}")
            //    .ToList();

            //var queryString = string.Join("&", queryStringFragments);

            //return queryString;
        //}
    }
}
