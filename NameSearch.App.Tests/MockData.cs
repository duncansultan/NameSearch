using NameSearch.Models.Domain.Api.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace NameSearch.App.Tests
{
    /// <summary>
    ///     Domain Test Data for Unit Tests
    /// </summary>
    public static class MockData
    {
        /// <summary>
        /// Gets the test people.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Models.Domain.Api.Request.Person> GetTestPeople()
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
        /// Gets the test search names.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Models.Entities.Name> GetTestSearchNames()
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

        /// <summary>
        /// Gets the API response.
        /// </summary>
        /// <returns></returns>
        public static ApiResponse GetApiResponse()
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
        /// Gets the example json data.
        /// </summary>
        /// <returns></returns>
        public static string GetExampleJsonData()
        {
            return "{\r\n  \"count_person\": 1,\r\n  \"person\": [\r\n    {\r\n      \"id\": \"Person.fbb71b50-0000-4b57-aba0-eafef8ce9c57.Durable\",\r\n      \"name\": \"Giaan Qiuntero\",\r\n      \"firstname\": \"Giaan\",\r\n      \"middlename\": null,\r\n      \"lastname\": \"Qiuntero\",\r\n      \"age_range\": \"60-64\",\r\n      \"gender\": \"Male\",\r\n      \"found_at_address\": {\r\n        \"id\": \"Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable\",\r\n        \"location_type\": \"Address\",\r\n        \"street_line_1\": \"102 Syrws St\",\r\n        \"street_line_2\": null,\r\n        \"city\": \"Lynden\",\r\n        \"postal_code\": \"98264\",\r\n        \"zip4\": \"9999\",\r\n        \"state_code\": \"WA\",\r\n        \"country_code\": \"US\",\r\n        \"lat_long\": {\r\n          \"latitude\": 48.966444,\r\n          \"longitude\": -121.960843,\r\n          \"accuracy\": \"RoofTop\"\r\n        },\r\n        \"is_active\": null,\r\n        \"delivery_point\": null,\r\n        \"link_to_person_start_date\": \"2017-01-01\",\r\n        \"link_to_person_end_date\": null\r\n      },\r\n      \"current_addresses\": [\r\n        {\r\n          \"id\": \"Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable\",\r\n          \"location_type\": \"Address\",\r\n          \"street_line_1\": \"102 Syrws St\",\r\n          \"street_line_2\": null,\r\n          \"city\": \"Lynden\",\r\n          \"postal_code\": \"98264\",\r\n          \"zip4\": \"9999\",\r\n          \"state_code\": \"WA\",\r\n          \"country_code\": \"US\",\r\n          \"lat_long\": {\r\n            \"latitude\": 48.966444,\r\n            \"longitude\": -121.960843,\r\n            \"accuracy\": \"RoofTop\"\r\n          },\r\n          \"is_active\": null,\r\n          \"delivery_point\": null,\r\n          \"link_to_person_start_date\": \"2017-01-01\"\r\n        }\r\n      ],\r\n      \"historical_addresses\": [\r\n        {\r\n          \"id\": \"Location.d1a40ed5-a70a-46f8-80a9-bb4ac27e3a21.Durable\",\r\n          \"location_type\": \"Address\",\r\n          \"street_line_1\": \"21 Syrws St\",\r\n          \"street_line_2\": null,\r\n          \"city\": \"Lynden\",\r\n          \"postal_code\": \"98264\",\r\n          \"zip4\": \"9999\",\r\n          \"state_code\": \"WA\",\r\n          \"country_code\": \"US\",\r\n          \"lat_long\": {\r\n            \"latitude\": 48.966444,\r\n            \"longitude\": -121.960843,\r\n            \"accuracy\": \"RoofTop\"\r\n          },\r\n          \"is_active\": true,\r\n          \"delivery_point\": \"SingleUnit\",\r\n          \"link_to_person_start_date\": \"2017-01-01\",\r\n          \"link_to_person_end_date\": null\r\n        }\r\n      ],\r\n      \"phones\": [\r\n        {\r\n          \"id\": \"Phone.3de36fef-a2df-4b08-cfe3-bc7128b6f5b4.Durable\",\r\n          \"phone_number\": \"+12061115121\",\r\n          \"line_type\": \"Landline\"\r\n        },\r\n        {\r\n          \"id\": \"Phone.3de56fef-a2df-4b08-cfe3-bc7128b6f5b4.Durable\",\r\n          \"phone_number\": \"+12061115122\",\r\n          \"line_type\": \"Landline\"\r\n        }\r\n      ],\r\n      \"associated_people\": [\r\n        {\r\n          \"id\": \"Person.f9640101-4157-41f5-a48b-86372e9c2acd.Durable\",\r\n          \"name\": \"Drama Number\",\r\n          \"firstname\": \"Drama\",\r\n          \"middlename\": null,\r\n          \"lastname\": \"Number\",\r\n          \"relation\": \"Unrecognized\"\r\n        },\r\n        {\r\n          \"id\": \"Person.fe5cefe2-687a-4ca1-a18f-9eaefad5891b.Durable\",\r\n          \"name\": \"Faaeega Wachesstock\",\r\n          \"firstname\": \"Faaeega\",\r\n          \"middlename\": null,\r\n          \"lastname\": \"Wachesstock\",\r\n          \"relation\": \"Household\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n  \"warnings\": [\r\n    \"Partial Address\"\r\n  ],\r\n  \"error\": null,\r\n}\r\n";
        }

        /// <summary>
        /// Gets the response content as json.
        /// </summary>
        /// <returns></returns>
        public static string GetResponseContentAsJson()
        {
            var person = GetPersonObject("Person.fbb71b50-0000-4b57-aba0-eafef8ce9c57.Durable", "Duncan Sultan", "Duncan", "", "Sultan", "25-29","Male" , new JArray(), new JArray(), new JArray(), new JArray());
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
    }
}
