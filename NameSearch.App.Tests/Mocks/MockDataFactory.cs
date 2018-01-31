using NameSearch.Models.Domain;
using NameSearch.Models.Domain.Api.Response;
using NameSearch.Models.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Domain Test Data for Unit Tests
    /// </summary>
    public static class MockDataFactory
    {
        #region Entities

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public static List<Models.Entities.Address> GetAddresses()
        {
            return new List<Models.Entities.Address>()
            {
                new Models.Entities.Address()
                {
                    PersonId = 1,
                    Address1 = "123 Oak Rd",
                    Address2 = "",
                    City = "Dallas",
                    State = "TX",
                    Zip = "75083",
                    Plus4 = "1234",
                    Country = "US",
                    Longitude = null,
                    Latitude = null,
                    IsCurrent = true
                },
                new Models.Entities.Address
                {
                    PersonId = 1,
                    Address1 = "456 Sunny Trail Court",
                    Address2 = "",
                    City = "Frisco",
                    State = "TX",
                    Zip = "12346",
                    Plus4 = "",
                    Country = "US",
                    Latitude = 40.712775,
                    Longitude = -74.005973,
                    IsCurrent = true
                },
                new Models.Entities.Address
                {
                    PersonId = 1,
                    Address1 = "123 Falcon Drive",
                    Address2 = "#123",
                    City = "Raleigh",
                    State = "NC",
                    Zip = "12346",
                    Plus4 = null,
                    Country = "US",
                    Latitude = 32.776664,
                    Longitude = -96.796988,
                    IsCurrent = true
                }
            };
        }

        /// <summary>
        /// Gets the name import.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static NameImport GetNameImport(long id)
        {
            return new NameImport()
            {
                Id = id,
                Names = GetTestSearchNames()
            };
        }

        /// <summary>
        /// Gets the person search job.
        /// </summary>
        /// <returns></returns>
        public static Models.Entities.PersonSearchJob GetPersonSearchJob()
        {
            var personSearchJob = new PersonSearchJob();
            personSearchJob.PersonSearchRequests = GetPersonSearchRequests();
            return personSearchJob;
        }

        /// <summary>
        /// Gets the person search request.
        /// </summary>
        /// <returns></returns>
        public static Models.Entities.PersonSearchRequest GetPersonSearchRequest()
        {
            return new PersonSearchRequest
            {
                Id = 1,
                PersonSearchJobId = 1,
                Name = "Omondi",
                Address1 = "123 Smith Rd",
                Address2 = "456",
                City = "Dallas",
                State = "TX",
                Zip = "81201",
                Country = "USA"
            };
        }

        /// <summary>
        /// Gets the person search requests.
        /// </summary>
        /// <returns></returns>
        public static List<Models.Entities.PersonSearchRequest> GetPersonSearchRequests()
        {
            var personSearchRequest = new List<PersonSearchRequest>
            {
                GetPersonSearchRequest()
            };
            return personSearchRequest;
        }

        /// <summary>
        /// Gets the person search result.
        /// </summary>
        /// <returns></returns>
        public static Models.Entities.PersonSearchResult GetPersonSearchResult()
        {
            return new PersonSearchResult
            {
                Data = GetExampleJsonData(),
                HttpStatusCode = 200,
                NumberOfResults = 1
            };
        }

        /// <summary>
        /// Gets the person search results.
        /// </summary>
        /// <returns></returns>
        public static List<Models.Entities.PersonSearchResult> GetPersonSearchResults()
        {
            var personSearchResults = new List<PersonSearchResult>
            {
                GetPersonSearchResult()
            };
            return personSearchResults;
        }

        /// <summary>
        /// Gets the test person.
        /// </summary>
        /// <returns></returns>
        public static Models.Entities.Person GetTestPerson()
        {
            var personEntity = new Models.Entities.Person();
            personEntity.Addresses = GetAddresses();
            return personEntity;
        }

        /// <summary>
        /// Gets the test search names.
        /// </summary>
        /// <returns></returns>
        public static List<Models.Entities.Name> GetTestSearchNames()
        {
            var people = new List<Models.Entities.Name>
            {
                new Models.Entities.Name
                {
                    Id = 1,
                    Value = "Mwangi",
                    Description = "Kenya",
                    IsActive = true,
                    ModifiedDateTime = DateTime.Now,
                    NameImportId = 1,
                    CreatedDateTime = DateTime.Now
                }
            };
            return people;
        }

        #endregion Entities

        #region Domain

        /// <summary>
        /// Gets the API response.
        /// </summary>
        /// <returns></returns>
        public static IApiResponse GetApiResponse()
        {
            var apiResponse = new ApiResponse
            {
                RequestUri = "https://proapi.whitepages.com/3.0/person?name=Giaan+Qiuntero&address.city=Lynden&address.state_code=WA&api_key=KEYVAL",
                StatusCode = 200,
                Content = GetResponseContentAsJson()
            };
            return apiResponse;
        }

        /// <summary>
        /// Gets the API response.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public static IApiResponse GetApiResponse(Models.Domain.Api.Request.IPerson person)
        {
            var apiResponse = new ApiResponse
            {
                RequestUri = "https://proapi.whitepages.com/3.0/person?name=Giaan+Qiuntero&address.city=Lynden&address.state_code=WA&api_key=KEYVAL",
                StatusCode = 200,
                Content = GetResponseContentAsJson()
            };
            return apiResponse;
        }

        /// <summary>
        /// Gets the test names.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTestNames()
        {
            return GetTestSearchNames().Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Gets the test people.
        /// </summary>
        /// <returns></returns>
        public static List<Models.Domain.Api.Request.Person> GetTestPeople()
        {
            var people = new List<Models.Domain.Api.Request.Person>
            {
                new Models.Domain.Api.Request.Person
                {
                    Address1 = "",
                    Address2 = "",
                    City = "",
                    Country = "",
                    Name = "Mwangi",
                    State = "NC",
                    Zip = ""
                }
            };
            return people;
        }

        /// <summary>
        /// Gets the test search criteria.
        /// </summary>
        /// <returns></returns>
        public static SearchCriteria GetTestSearchCriteria()
        {
            return new SearchCriteria
            {
                City = "Raleigh",
                State = "NC",
                Country = "USA"
            };
        }

        #endregion Domain

        #region Json

        /// <summary>
        /// Gets the address object.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="location_type">Type of the location.</param>
        /// <param name="street_line_1">The street line 1.</param>
        /// <param name="street_line_2">The street line 2.</param>
        /// <param name="city">The city.</param>
        /// <param name="postal_code">The postal code.</param>
        /// <param name="zip4">The zip4.</param>
        /// <param name="state_code">The state code.</param>
        /// <param name="country_code">The country code.</param>
        /// <param name="lat_long">The lat long.</param>
        /// <param name="is_active">if set to <c>true</c> [is active].</param>
        /// <param name="delivery_point">The delivery point.</param>
        /// <returns></returns>
        public static JObject GetAddressObject(string id,
            string location_type,
            string street_line_1,
            string street_line_2,
            string city,
            string postal_code,
            string zip4,
            string state_code,
            string country_code,
            JObject lat_long,
            bool is_active,
            string delivery_point)
        {
            var address = new JObject
            {
                new JProperty("id", id),
                new JProperty("location_type", location_type),
                new JProperty("street_line_1", street_line_1),
                new JProperty("street_line_2", street_line_2),
                new JProperty("city", city),
                new JProperty("postal_code", postal_code),
                new JProperty("zip4", zip4),
                new JProperty("state_code", state_code),
                new JProperty("country_code", country_code),
                new JProperty("lat_long", lat_long),
                new JProperty("is_active", is_active),
                new JProperty("delivery_point", delivery_point)
            };
            return address;
        }

        /// <summary>
        /// Gets the associated person object.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="relation">The relation.</param>
        /// <returns></returns>
        public static JObject GetAssociatedPersonObject(string id, string name, string relation)
        {
            var associatedPerson = new JObject
            {
                new JProperty("id", id),
                new JProperty("name", name),
                new JProperty("relation", relation)
            };
            return associatedPerson;
        }

        /// <summary>
        /// Gets the example j object.
        /// </summary>
        /// <returns></returns>
        public static JObject GetExampleJObject()
        {
            return JObject.Parse(GetExampleJsonData());
        }

        /// <summary>
        /// Gets the example json data.
        /// </summary>
        /// <returns></returns>
        public static string GetExampleJsonData()
        {
            return Properties.Resources.TestResponseJson;
        }

        /// <summary>
        /// Gets the person object.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="age_range">The age range.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="current_addresses">The current addresses.</param>
        /// <param name="historical_addresses">The historical addresses.</param>
        /// <param name="phones">The phones.</param>
        /// <param name="associated_people">The associated people.</param>
        /// <returns></returns>
        public static JObject GetPersonObject(string id,
            string name,
            string firstName,
            string middleName,
            string lastName,
            string age_range,
            string gender,
            JArray current_addresses,
            JArray historical_addresses,
            JArray phones,
            JArray associated_people)
        {
            var person = new JObject
            {
                new JProperty("id", id),
                new JProperty("name", name),
                new JProperty("middleName", middleName),
                new JProperty("lastName", lastName),
                new JProperty("ageRange", age_range),
                new JProperty("gender", gender),
                new JProperty("current_addresses", current_addresses),
                new JProperty("historical_addresses", historical_addresses),
                new JProperty("phones", phones),
                new JProperty("associated_people", associated_people)
            };
            return person;
        }

        /// <summary>
        /// Gets the phone object.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="phone_number">The phone number.</param>
        /// <param name="line_type">Type of the line.</param>
        /// <returns></returns>
        public static JObject GetPhoneObject(string id, string phone_number, string line_type)
        {
            var phone = new JObject
            {
                new JProperty("id", id),
                new JProperty("phone_number", phone_number),
                new JProperty("line_type", line_type)
            };
            return phone;
        }

        /// <summary>
        /// Gets the response content as json.
        /// </summary>
        /// <returns></returns>
        public static string GetResponseContentAsJson()
        {
            var person = GetPersonObject("Person.fbb71b50-0000-4b57-aba0-eafef8ce9c57.Durable", "Duncan Sultan", "Duncan", "", "Sultan", "25-29", "Male", new JArray(), new JArray(), new JArray(), new JArray());
            var responseObject = GetResponseObject(10, person, "", "");
            return responseObject.ToString();
        }

        /// <summary>
        /// Gets the response object.
        /// </summary>
        /// <param name="countPerson">The count person.</param>
        /// <param name="person">The person.</param>
        /// <param name="warnings">The warnings.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static JObject GetResponseObject(int countPerson, JObject person, string warnings, string error)
        {
            var jObject = new JObject
            {
                new JProperty("count_person", countPerson),
                new JProperty("person", person),
                new JProperty("warnings", warnings),
                new JProperty("error", error)
            };
            return jObject;
        }

        #endregion Json
    }
}