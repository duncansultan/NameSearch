using NameSearch.Models.Entities;
using System;
using System.Collections.Generic;

namespace NameSearch.Repository.Tests
{
    /// <summary>
    ///     Entity Test Data for Unit Tests
    /// </summary>
    public static class MockData
    {
        /// <summary>
        /// Gets the example json data.
        /// </summary>
        /// <returns></returns>
        public static string GetExampleJsonData()
        {
            return "{\r\n  \"count_person\": 1,\r\n  \"person\": [\r\n    {\r\n      \"id\": \"Person.fbb71b50-0000-4b57-aba0-eafef8ce9c57.Durable\",\r\n      \"name\": \"Giaan Qiuntero\",\r\n      \"firstname\": \"Giaan\",\r\n      \"middlename\": null,\r\n      \"lastname\": \"Qiuntero\",\r\n      \"age_range\": \"60-64\",\r\n      \"gender\": \"Male\",\r\n      \"found_at_address\": {\r\n        \"id\": \"Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable\",\r\n        \"location_type\": \"Address\",\r\n        \"street_line_1\": \"102 Syrws St\",\r\n        \"street_line_2\": null,\r\n        \"city\": \"Lynden\",\r\n        \"postal_code\": \"98264\",\r\n        \"zip4\": \"9999\",\r\n        \"state_code\": \"WA\",\r\n        \"country_code\": \"US\",\r\n        \"lat_long\": {\r\n          \"latitude\": 48.966444,\r\n          \"longitude\": -121.960843,\r\n          \"accuracy\": \"RoofTop\"\r\n        },\r\n        \"is_active\": null,\r\n        \"delivery_point\": null,\r\n        \"link_to_person_start_date\": \"2017-01-01\",\r\n        \"link_to_person_end_date\": null\r\n      },\r\n      \"current_addresses\": [\r\n        {\r\n          \"id\": \"Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable\",\r\n          \"location_type\": \"Address\",\r\n          \"street_line_1\": \"102 Syrws St\",\r\n          \"street_line_2\": null,\r\n          \"city\": \"Lynden\",\r\n          \"postal_code\": \"98264\",\r\n          \"zip4\": \"9999\",\r\n          \"state_code\": \"WA\",\r\n          \"country_code\": \"US\",\r\n          \"lat_long\": {\r\n            \"latitude\": 48.966444,\r\n            \"longitude\": -121.960843,\r\n            \"accuracy\": \"RoofTop\"\r\n          },\r\n          \"is_active\": null,\r\n          \"delivery_point\": null,\r\n          \"link_to_person_start_date\": \"2017-01-01\"\r\n        }\r\n      ],\r\n      \"historical_addresses\": [\r\n        {\r\n          \"id\": \"Location.d1a40ed5-a70a-46f8-80a9-bb4ac27e3a21.Durable\",\r\n          \"location_type\": \"Address\",\r\n          \"street_line_1\": \"21 Syrws St\",\r\n          \"street_line_2\": null,\r\n          \"city\": \"Lynden\",\r\n          \"postal_code\": \"98264\",\r\n          \"zip4\": \"9999\",\r\n          \"state_code\": \"WA\",\r\n          \"country_code\": \"US\",\r\n          \"lat_long\": {\r\n            \"latitude\": 48.966444,\r\n            \"longitude\": -121.960843,\r\n            \"accuracy\": \"RoofTop\"\r\n          },\r\n          \"is_active\": true,\r\n          \"delivery_point\": \"SingleUnit\",\r\n          \"link_to_person_start_date\": \"2017-01-01\",\r\n          \"link_to_person_end_date\": null\r\n        }\r\n      ],\r\n      \"phones\": [\r\n        {\r\n          \"id\": \"Phone.3de36fef-a2df-4b08-cfe3-bc7128b6f5b4.Durable\",\r\n          \"phone_number\": \"+12061115121\",\r\n          \"line_type\": \"Landline\"\r\n        },\r\n        {\r\n          \"id\": \"Phone.3de56fef-a2df-4b08-cfe3-bc7128b6f5b4.Durable\",\r\n          \"phone_number\": \"+12061115122\",\r\n          \"line_type\": \"Landline\"\r\n        }\r\n      ],\r\n      \"associated_people\": [\r\n        {\r\n          \"id\": \"Person.f9640101-4157-41f5-a48b-86372e9c2acd.Durable\",\r\n          \"name\": \"Drama Number\",\r\n          \"firstname\": \"Drama\",\r\n          \"middlename\": null,\r\n          \"lastname\": \"Number\",\r\n          \"relation\": \"Unrecognized\"\r\n        },\r\n        {\r\n          \"id\": \"Person.fe5cefe2-687a-4ca1-a18f-9eaefad5891b.Durable\",\r\n          \"name\": \"Faaeega Wachesstock\",\r\n          \"firstname\": \"Faaeega\",\r\n          \"middlename\": null,\r\n          \"lastname\": \"Wachesstock\",\r\n          \"relation\": \"Household\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n  \"warnings\": [\r\n    \"Partial Address\"\r\n  ],\r\n  \"error\": null,\r\n}\r\n";
        }
        /// <summary>
        /// Gets the name import.
        /// </summary>
        /// <returns></returns>
        public static NameImport GetNameImport()
        {
            return new NameImport
            {
                FileName = $"NameImportTest-{DateTime.Now}.csv"
            };
        }
        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <param name="nameImportId">The name import identifier.</param>
        /// <returns></returns>
        public static List<Name> GetNames(long nameImportId)
        {
            return new List<Name>
            {
                new Name
                {
                    NameImportId = nameImportId,
                    Value = "Mwangi",
                    Description = "Kenya"
                },
                new Name
                { NameImportId = nameImportId,
                    Value = "Omondi",
                    Description = "Kenya"
                },
                new Name
                { NameImportId = nameImportId,
                    Value = "Ndunde",
                    Description = "Kenya"
                },
                new Name
                { NameImportId = nameImportId,
                    Value = "Twshibwabwa",
                    Description = "Congo"
                },
                new Name
                { NameImportId = nameImportId,
                    Value = "Nihobontige",
                    Description = "Rwanda"
                }
            };
        }
        /// <summary>
        /// Gets the person search job.
        /// </summary>
        /// <returns></returns>
        public static PersonSearchJob GetPersonSearchJob()
        {
            return new PersonSearchJob();
        }
        /// <summary>
        /// Gets the person search result.
        /// </summary>
        /// <param name="personSearchRequestId">The person search job identifier.</param>
        /// <returns></returns>
        public static PersonSearchResult GetPersonSearchResult(long personSearchRequestId)
        {
            return new PersonSearchResult
            {
                PersonSearchRequestId = personSearchRequestId,
                Data = GetExampleJsonData(),
                Error = null,
                Warnings = "Missing Input Name",
                HttpStatusCode = 200,
                NumberOfResults = 1
            };
        }
        /// <summary>
        /// Gets the person search results.
        /// </summary>
        /// <param name="personSearchRequestId">The person search job identifier.</param>
        /// <returns></returns>
        public static List<PersonSearchResult> GetPersonSearchResults(long personSearchRequestId)
        {
            return new List<PersonSearchResult>
            {
               new PersonSearchResult
                {
                    PersonSearchRequestId = personSearchRequestId,
                    Data = GetExampleJsonData(),
                    Error = null,
                    Warnings = "Missing Input Name",
                    HttpStatusCode = 200,
                    NumberOfResults = 1
                },
                new PersonSearchResult
                {
                    PersonSearchRequestId = personSearchRequestId,
                    Data = GetExampleJsonData(),
                    Error = null,
                    Warnings = "Missing Input Address",
                    HttpStatusCode = 200,
                    NumberOfResults = 1
                },
                new PersonSearchResult
                {
                    PersonSearchRequestId = personSearchRequestId,
                    Data = GetExampleJsonData(),
                    Error = null,
                    Warnings = "Partial Address",
                    HttpStatusCode = 200,
                    NumberOfResults = 1
                }
            };
        }
        public static List<PersonSearchRequest> GetPersonSearchRequests(long personSearchJobId)
        {
            return new List<PersonSearchRequest>
            {
               new PersonSearchRequest
                {
                    PersonSearchJobId = personSearchJobId,
                    Name = "John Adams",
                    Address1 = "123 Smith",
                    Address2 = "5555",
                    City = "Plano",
                    State  = "TX",
                    Zip  = "77777",
                    Country  = "US"
                },
                new PersonSearchRequest
                {
                    PersonSearchJobId = personSearchJobId,
                    Name = "John Williamson",
                    Address1 = "999 South St",
                    Address2 = "12",
                    City = "Fort Worth",
                    State  = "TX",
                    Zip  = "72277",
                    Country  = "US"
                },
                new PersonSearchRequest
                {
                    PersonSearchJobId = personSearchJobId,
                    Name = "John Hancock",
                    Address1 = "456 Oak Dr",
                    Address2 = "",
                    City = "Denton",
                    State  = "TX",
                    Zip  = "77767",
                    Country  = "US"
                }
            };
        }
        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="personSearchResultId">The person search result identifier.</param>
        /// <returns></returns>
        public static Person GetPerson(long personSearchResultId)
        {
            return new Person
            {
                PersonSearchResultId = personSearchResultId,
                FirstName = "Duncan",
                LastName = "Sultan",
                Alias = "Sultan of Swing",
                AgeRange = 33
            };
        }
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public static List<Address> GetAddresses(long personId)
        {
            return new List<Address>()
            {
                new Address()
                {
                    PersonId = personId,
                    Address1 = "123 Oak Rd",
                    Address2 = "",
                    City = "Dallas",
                    State = "TX",
                    Zip = "75083",
                    Plus4 = "1234",
                    Country = "US",
                    Longitude = null,
                    Lattitue = null,
                    IsCurrent = true
                },
                new Address
                {
                    PersonId = personId,
                    Address1 = "456 Sunny Trail Court",
                    Address2 = "",
                    City = "Frisco",
                    State = "TX",
                    Zip = "12346",
                    Plus4 = "",
                    Country = "US",
                    Lattitue = 40.712775,
                    Longitude = -74.005973,
                    IsCurrent = true
                },
                new Address
                {
                    PersonId = personId,
                    Address1 = "123 Falcon Drive",
                    Address2 = "#123",
                    City = "Raleigh",
                    State = "NC",
                    Zip = "12346",
                    Plus4 = null,
                    Country = "US",
                    Lattitue = 32.776664,
                    Longitude = -96.796988,
                    IsCurrent = true
                }
            };
        }
        /// <summary>
        /// Gets the associates.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public static List<Associate> GetAssociates(long personId)
        {
            return new List<Associate>()
            {
                new Associate()
                {
                    PersonId = personId,
                    Name = "Bob Smith",
                    Relation = "Grandfather"
                },
                new Associate
                {
                    PersonId = personId,
                    Name = "Joe Blow",
                    Relation = "Friend"
                },
                new Associate
                {
                    PersonId = personId,
                    Name = "Tommy Boy",
                    Relation = "Uncle"
                }
            };
        }
        /// <summary>
        /// Gets the phones.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public static List<Phone> GetPhones(long personId)
        {
            return new List<Phone>()
            {
                new Phone()
                {
                    PersonId = personId,
                    PhoneNumber = "123-456-7890"

                },
                new Phone
                {
                    PersonId = personId,
                    PhoneNumber = "1234567890"
                },
                new Phone
                {
                    PersonId = personId,
                    PhoneNumber = "(123) 456-7890"
                },
                new Phone
                {
                    PersonId = personId,
                    PhoneNumber = "(123) 456-7890x1234"
                },
                new Phone
                {
                    PersonId = personId,
                    PhoneNumber = "+1 (123) 456-7890"
                }
            };
        }
    }
}
