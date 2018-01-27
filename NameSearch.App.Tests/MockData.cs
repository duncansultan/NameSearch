﻿using NameSearch.Models.Domain.Api.Response;
using NameSearch.Models.Entities;
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

        public static PersonSearchJob GetPersonSearchJob()
        {
            var personSearchJob = new PersonSearchJob();
            personSearchJob.PersonSearchResults = GetPersonSearchResults();
            return personSearchJob;
        }

        public static List<PersonSearchResult> GetPersonSearchResults()
        {
            var personSearchResults = new List<PersonSearchResult>
            {
                GetPersonSearchResult()
            };
            return personSearchResults;
        }

        private static PersonSearchResult GetPersonSearchResult()
        {
            return new PersonSearchResult
            {
                Data = GetExampleJsonData(),
                HttpStatusCode = 200,
                NumberOfResults = 1
            };
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
        /// Gets the example json data.
        /// </summary>
        /// <returns></returns>
        public static string GetExampleJsonData()
        {
            return Properties.Resources.TestResponseJson;
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
