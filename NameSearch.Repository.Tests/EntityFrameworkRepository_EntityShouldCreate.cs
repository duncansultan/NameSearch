using System.Linq;
using NameSearch.Context;
using NameSearch.Models.Entities;
using Xunit;

namespace NameSearch.Repository.Tests
{
    /// <summary>
    ///     Unit tests for Creating New Entities
    /// </summary>
    public class EntityFrameworkRepository_EntityShouldCreate
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

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
            var personSearchJob = MockData.GetPersonSearchJob();
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
            var nameImport = MockData.GetNameImport();
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
            var personSearchJob = MockData.GetPersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResults = MockData.GetPersonSearchResults(personSearchJob.Id);
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
            var nameImport = MockData.GetNameImport();
            Repository.Create(nameImport);
            Repository.Save();

            //Act
            var names = MockData.GetNames(nameImport.Id);
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
            var personSearchJob = MockData.GetPersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResult = MockData.GetPersonSearchResult(personSearchJob.Id);
            Repository.Create(personSearchResult);
            Repository.Save();

            //Act
            var person = MockData.GetPerson(personSearchResult.Id);
            Repository.Create(person);
            Repository.Save();

            var addresses = MockData.GetAddresses(person.Id);
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
            var personSearchJob = MockData.GetPersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResult = MockData.GetPersonSearchResult(personSearchJob.Id);
            Repository.Create(personSearchResult);
            Repository.Save();

            //Act
            var person = MockData.GetPerson(personSearchResult.Id);
            Repository.Create(person);
            Repository.Save();

            var phones = MockData.GetPhones(person.Id);
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
            var personSearchJob = MockData.GetPersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            var personSearchResult = MockData.GetPersonSearchResult(personSearchJob.Id);
            Repository.Create(personSearchResult);
            Repository.Save();

            //Act
            var person = MockData.GetPerson(personSearchResult.Id);
            Repository.Create(person);
            Repository.Save();

            var associates = MockData.GetAssociates(person.Id);
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
    }
}
