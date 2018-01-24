using System;
using System.Collections.Generic;
using System.Linq;
using NameSearch.Context;
using NameSearch.Models.Entities;
using Xunit;

namespace NameSearch.Repository.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityFrameworkRepository_EntityShouldCreate
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly EntityFrameworkRepository Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository_EntityShouldCreate"/> class.
        /// </summary>
        public EntityFrameworkRepository_EntityShouldCreate()
        {
            Repository = new EntityFrameworkRepository(new ApplicationDbContext());
        }

        /// <summary>
        /// Creates the person search job.
        /// </summary>
        [Fact]
        public void CreatePersonSearchJob()
        {
            //Arrange
            //Act
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var savedPersonSearchJob = Repository.GetFirst<PersonSearchJob>(x => x.Id == personSearchJob.Id);

            //Assert
            Assert.Equal(personSearchJob.Id, savedPersonSearchJob.Id);
        }

        /// <summary>
        /// Creates the name import.
        /// </summary>
        [Fact]
        public void CreateNameImport()
        {
            //Arrange
            //Act
            var nameImport = new NameImport();
            Repository.Create(nameImport);
            Repository.Save();

            var savedNameImport = Repository.GetFirst<NameImport>(x => x.Id == nameImport.Id);

            //Assert
            Assert.Equal(nameImport.Id, savedNameImport.Id);
        }

        /// <summary>
        /// Creates the person search job with results.
        /// </summary>
        [Fact]
        public void CreatePersonSearchJobWithResults()
        {
            //Arrange
            //Act
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResults = new List<PersonSearchResult>
            {
                new PersonSearchResult
                {
                    PersonSearchJobId = personSearchJob.Id,
                    Data = GetExampleJsonData(),
                    Error = null,
                    Warnings = "Missing Input Name",
                    HttpStatusCode = 200,
                    NumberOfResults = 1
                },
                new PersonSearchResult
                {
                    PersonSearchJobId = personSearchJob.Id,
                    Data = GetExampleJsonData(),
                    Error = null,
                    Warnings = "Missing Input Address",
                    HttpStatusCode = 200,
                    NumberOfResults = 1
                },
                new PersonSearchResult
                {
                    PersonSearchJobId = personSearchJob.Id,
                    Data = GetExampleJsonData(),
                    Error = null,
                    Warnings = "Partial Address",
                    HttpStatusCode = 200,
                    NumberOfResults = 1
                }
            };
            foreach (var name in personSearchResults)
            {
                Repository.Create(name);
                Repository.Save();
            }

            var savedPersonSearchJob = Repository.GetFirst<PersonSearchJob>(x => x.Id == personSearchJob.Id, includeProperties: "PersonSearchResults");
            var savedPersonSearchResults = Repository.Get<PersonSearchResult>(x => x.PersonSearchJobId == personSearchJob.Id);

            //Assert
            Assert.Equal(personSearchJob.Id, savedPersonSearchJob.Id);
            Assert.Equal(personSearchResults.Count(), savedPersonSearchResults.Count());
            Assert.Equal(personSearchJob.PersonSearchResults.Count(), savedPersonSearchJob.PersonSearchResults.Count());

            foreach (var savedName in savedPersonSearchResults)
            {
                var anyName = personSearchResults.Any(x => x.Id == savedName.Id);
                Assert.True(anyName);
            }
        }

        /// <summary>
        /// Creates the name import with names.
        /// </summary>
        [Fact]
        public void CreateNameImportWithNames()
        {
            //Arrange
            //Act
            var nameImport = new NameImport
            {
                FileName = $"NameImportTest-{DateTime.Now}.csv"
            };
            Repository.Create(nameImport);
            Repository.Save();

            var names = new List<Name>
            {
                new Name
                {
                    NameImportId = nameImport.Id,
                    Value = "Duncan",
                    Description = "Scottish"
                },
                new Name
                {
                    NameImportId = nameImport.Id,
                    Value = "Andrew",
                    Description = "English"
                },
                new Name
                {
                    NameImportId = nameImport.Id,
                    Value = "O'Reily",
                    Description = "Irish"
                }
            };
            foreach (var name in names)
            {
                Repository.Create(name);
                Repository.Save();
            }

            var savedNameImport = Repository.GetFirst<NameImport>(x => x.Id == nameImport.Id);
            var savedNames = Repository.Get<Name>(x => x.NameImportId == nameImport.Id);

            //Assert
            Assert.Equal(nameImport.Id, savedNameImport.Id);
            Assert.Equal(nameImport.FileName, savedNameImport.FileName);
            Assert.Equal(names.Count(), savedNames.Count());
            Assert.Equal(names.Count(), savedNameImport.Names.Count());

            foreach (var savedName in savedNames)
            {
                var anyName = names.Any(x => x.Id == savedName.Id);
                Assert.True(anyName);
            }
        }

        /// <summary>
        /// Creates the person with addresses.
        /// </summary>
        [Fact]
        public void CreatePersonWithAddresses()
        {
            //Arrange
            //Act
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResult = new PersonSearchResult
            {
                PersonSearchJobId = personSearchJob.Id,
                Data = GetExampleJsonData(),
                Error = null,
                Warnings = "Missing Input Name",
                HttpStatusCode = 200,
                NumberOfResults = 1
            };
            Repository.Create(personSearchResult);
            Repository.Save();

            var person = new Person
            {
                PersonSearchResultId = personSearchResult.Id,
                FirstName = "Duncan",
                LastName = "Sultan",
                Alias = "Sultan of Swing",
                Age = 33
            };
            Repository.Create(person);
            Repository.Save();

            var addresses = new List<Address>()
            {
                new Address()
                {
                    PersonId = person.Id,
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
                    PersonId = person.Id,
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
                    PersonId = person.Id,
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
            foreach (var address in addresses)
            {
                Repository.Create(address);
                Repository.Save();
            }

            var savedPerson = Repository.GetFirst<Person>(x => x.Id == person.Id);
            var savedAddresses = Repository.Get<Address>(x => x.PersonId == person.Id);

            //Assert
            Assert.Equal(person.Id, savedPerson.Id);
            Assert.Equal(person.FirstName, savedPerson.FirstName);
            Assert.Equal(person.LastName, savedPerson.LastName);
            Assert.Equal(person.Alias, savedPerson.Alias);
            Assert.Equal(person.Age, savedPerson.Age);
            Assert.Equal(addresses.Count(), savedAddresses.Count());
            Assert.Equal(addresses.Count(), savedPerson.Addresses.Count());

            foreach (var savedAddress in savedAddresses)
            {
                var anyAddress = addresses.Any(x => x.Id == savedAddress.Id);
                Assert.True(anyAddress);
            }
        }

        /// <summary>
        /// Creates the person with phones.
        /// </summary>
        [Fact]
        public void CreatePersonWithPhones()
        {
            //Arrange
            //Act
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResult = new PersonSearchResult
            {
                PersonSearchJobId = personSearchJob.Id,
                Data = GetExampleJsonData(),
                Error = null,
                Warnings = "Missing Input Name",
                HttpStatusCode = 200,
                NumberOfResults = 1
            };
            Repository.Create(personSearchResult);
            Repository.Save();

            var person = new Person
            {
                PersonSearchResultId = personSearchResult.Id,
                FirstName = "Duncan",
                LastName = "Sultan",
                Alias = "Sultan of Swing",
                Age = 33
            };
            Repository.Create(person);
            Repository.Save();

            var phones = new List<Phone>()
            {
                new Phone()
                {
                    PersonId = person.Id,
                    PhoneNumber = "123-456-7890"

                },
                new Phone
                {
                    PersonId = person.Id,
                    PhoneNumber = "1234567890"
                },
                new Phone
                {
                    PersonId = person.Id,
                    PhoneNumber = "(123) 456-7890"
                },
                new Phone
                {
                    PersonId = person.Id,
                    PhoneNumber = "(123) 456-7890x1234"
                },
                new Phone
                {
                    PersonId = person.Id,
                    PhoneNumber = "+1 (123) 456-7890"
                }
            };
            foreach (var phone in phones)
            {
                Repository.Create(phone);
                Repository.Save();
            }

            var savedPerson = Repository.GetFirst<Person>(x => x.Id == person.Id);
            var savedPhones = Repository.Get<Phone>(x => x.PersonId == person.Id);

            //Assert
            Assert.Equal(person.Id, savedPerson.Id);
            Assert.Equal(person.FirstName, savedPerson.FirstName);
            Assert.Equal(person.LastName, savedPerson.LastName);
            Assert.Equal(person.Alias, savedPerson.Alias);
            Assert.Equal(person.Age, savedPerson.Age);
            Assert.Equal(phones.Count(), savedPhones.Count());
            Assert.Equal(phones.Count(), savedPerson.Phones.Count());

            foreach (var savedPhone in savedPhones)
            {
                var anyPhone = phones.Any(x => x.Id == savedPhone.Id);
                Assert.True(anyPhone);
            }
        }

        /// <summary>
        /// Creates the person with associates.
        /// </summary>
        [Fact]
        public void CreatePersonWithAssociates()
        {
            //Arrange
            //Act
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResult = new PersonSearchResult
            {
                PersonSearchJobId = personSearchJob.Id,
                Data = GetExampleJsonData(),
                Error = null,
                Warnings = "Missing Input Name",
                HttpStatusCode = 200,
                NumberOfResults = 1
            };
            Repository.Create(personSearchResult);
            Repository.Save();

            var person = new Person
            {
                PersonSearchResultId = personSearchResult.Id,
                FirstName = "Duncan",
                LastName = "Sultan",
                Alias = "Sultan of Swing",
                Age = 33
            };
            Repository.Create(person);
            Repository.Save();

            var associates = new List<Associate>()
            {
                new Associate()
                {
                    PersonId = person.Id,
                    Name = "Bob Smith",
                    Relation = "Grandfather"
                },
                new Associate
                {
                    PersonId = person.Id,
                    Name = "Joe Blow",
                    Relation = "Friend"
                },
                new Associate
                {
                    PersonId = person.Id,
                    Name = "Tommy Boy",
                    Relation = "Uncle"
                }
            };
            foreach (var associate in associates)
            {
                Repository.Create(associate);
                Repository.Save();
            }

            var savedPerson = Repository.GetFirst<Person>(x => x.Id == person.Id);
            var savedAssociate = Repository.Get<Associate>(x => x.PersonId == person.Id);

            //Assert
            Assert.Equal(person.Id, savedPerson.Id);
            Assert.Equal(person.FirstName, savedPerson.FirstName);
            Assert.Equal(person.LastName, savedPerson.LastName);
            Assert.Equal(person.Alias, savedPerson.Alias);
            Assert.Equal(person.Age, savedPerson.Age);
            Assert.Equal(associates.Count(), savedAssociate.Count());
            Assert.Equal(associates.Count(), savedPerson.Associates.Count());

            foreach (var associate in savedAssociate)
            {
                var anyAssociate = associates.Any(x => x.Id == associate.Id);
                Assert.True(anyAssociate);
            }
        }

        /// <summary>
        /// Gets the example json data.
        /// </summary>
        /// <returns></returns>
        private static string GetExampleJsonData()
        {
            return "{\r\n  \"count_person\": 1,\r\n  \"person\": [\r\n    {\r\n      \"id\": \"Person.fbb71b50-0000-4b57-aba0-eafef8ce9c57.Durable\",\r\n      \"name\": \"Giaan Qiuntero\",\r\n      \"firstname\": \"Giaan\",\r\n      \"middlename\": null,\r\n      \"lastname\": \"Qiuntero\",\r\n      \"age_range\": \"60-64\",\r\n      \"gender\": \"Male\",\r\n      \"found_at_address\": {\r\n        \"id\": \"Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable\",\r\n        \"location_type\": \"Address\",\r\n        \"street_line_1\": \"102 Syrws St\",\r\n        \"street_line_2\": null,\r\n        \"city\": \"Lynden\",\r\n        \"postal_code\": \"98264\",\r\n        \"zip4\": \"9999\",\r\n        \"state_code\": \"WA\",\r\n        \"country_code\": \"US\",\r\n        \"lat_long\": {\r\n          \"latitude\": 48.966444,\r\n          \"longitude\": -121.960843,\r\n          \"accuracy\": \"RoofTop\"\r\n        },\r\n        \"is_active\": null,\r\n        \"delivery_point\": null,\r\n        \"link_to_person_start_date\": \"2017-01-01\",\r\n        \"link_to_person_end_date\": null\r\n      },\r\n      \"current_addresses\": [\r\n        {\r\n          \"id\": \"Location.18cab1f6-b0fb-400b-912e-dc715c777102.Durable\",\r\n          \"location_type\": \"Address\",\r\n          \"street_line_1\": \"102 Syrws St\",\r\n          \"street_line_2\": null,\r\n          \"city\": \"Lynden\",\r\n          \"postal_code\": \"98264\",\r\n          \"zip4\": \"9999\",\r\n          \"state_code\": \"WA\",\r\n          \"country_code\": \"US\",\r\n          \"lat_long\": {\r\n            \"latitude\": 48.966444,\r\n            \"longitude\": -121.960843,\r\n            \"accuracy\": \"RoofTop\"\r\n          },\r\n          \"is_active\": null,\r\n          \"delivery_point\": null,\r\n          \"link_to_person_start_date\": \"2017-01-01\"\r\n        }\r\n      ],\r\n      \"historical_addresses\": [\r\n        {\r\n          \"id\": \"Location.d1a40ed5-a70a-46f8-80a9-bb4ac27e3a21.Durable\",\r\n          \"location_type\": \"Address\",\r\n          \"street_line_1\": \"21 Syrws St\",\r\n          \"street_line_2\": null,\r\n          \"city\": \"Lynden\",\r\n          \"postal_code\": \"98264\",\r\n          \"zip4\": \"9999\",\r\n          \"state_code\": \"WA\",\r\n          \"country_code\": \"US\",\r\n          \"lat_long\": {\r\n            \"latitude\": 48.966444,\r\n            \"longitude\": -121.960843,\r\n            \"accuracy\": \"RoofTop\"\r\n          },\r\n          \"is_active\": true,\r\n          \"delivery_point\": \"SingleUnit\",\r\n          \"link_to_person_start_date\": \"2017-01-01\",\r\n          \"link_to_person_end_date\": null\r\n        }\r\n      ],\r\n      \"phones\": [\r\n        {\r\n          \"id\": \"Phone.3de36fef-a2df-4b08-cfe3-bc7128b6f5b4.Durable\",\r\n          \"phone_number\": \"+12061115121\",\r\n          \"line_type\": \"Landline\"\r\n        },\r\n        {\r\n          \"id\": \"Phone.3de56fef-a2df-4b08-cfe3-bc7128b6f5b4.Durable\",\r\n          \"phone_number\": \"+12061115122\",\r\n          \"line_type\": \"Landline\"\r\n        }\r\n      ],\r\n      \"associated_people\": [\r\n        {\r\n          \"id\": \"Person.f9640101-4157-41f5-a48b-86372e9c2acd.Durable\",\r\n          \"name\": \"Drama Number\",\r\n          \"firstname\": \"Drama\",\r\n          \"middlename\": null,\r\n          \"lastname\": \"Number\",\r\n          \"relation\": \"Unrecognized\"\r\n        },\r\n        {\r\n          \"id\": \"Person.fe5cefe2-687a-4ca1-a18f-9eaefad5891b.Durable\",\r\n          \"name\": \"Faaeega Wachesstock\",\r\n          \"firstname\": \"Faaeega\",\r\n          \"middlename\": null,\r\n          \"lastname\": \"Wachesstock\",\r\n          \"relation\": \"Household\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n  \"warnings\": [\r\n    \"Partial Address\"\r\n  ],\r\n  \"error\": null,\r\n}\r\n";
        }
    }
}
