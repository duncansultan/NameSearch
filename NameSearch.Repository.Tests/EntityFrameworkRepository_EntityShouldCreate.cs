using NameSearch.Context;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using NameSearch.Repository.Tests.Mocks;
using System.Linq;
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
        /// Creates the person with addresses.
        /// </summary>
        [Fact]
        public void CreatePersonWithAddresses()
        {
            //Arrange
            var personSearch = MockDataFactory.GetPersonSearch();
            Repository.Create(personSearch);
            Repository.Save();

            //Act
            var person = MockDataFactory.GetPerson(personSearch.Id);
            Repository.Create(person);
            Repository.Save();

            var savedPerson = Repository.GetFirst<Person>(x => x.Id == person.Id);
            var savedAddresses = Repository.Get<Address>(x => x.PersonId == person.Id);

            //Assert
            Assert.Equal(person.Id, savedPerson.Id);
            Assert.Equal(person.FirstName, savedPerson.FirstName);
            Assert.Equal(person.LastName, savedPerson.LastName);
            Assert.Equal(person.Alias, savedPerson.Alias);
            Assert.Equal(person.AgeRange, savedPerson.AgeRange);
            Assert.Equal(person.Addresses.Count(), savedAddresses.Count());
        }

        /// <summary>
        /// Creates the person with associates.
        /// </summary>
        [Fact]
        public void CreatePersonWithAssociates()
        {
            //Arrange
            var personSearch = MockDataFactory.GetPersonSearch();
            Repository.Create(personSearch);
            Repository.Save();

            //Act
            var person = MockDataFactory.GetPerson(personSearch.Id);
            Repository.Create(person);
            Repository.Save();

            var savedPerson = Repository.GetFirst<Person>(x => x.Id == person.Id);
            var savedAssociate = Repository.Get<Associate>(x => x.PersonId == person.Id);

            //Assert
            Assert.Equal(person.Id, savedPerson.Id);
            Assert.Equal(person.FirstName, savedPerson.FirstName);
            Assert.Equal(person.LastName, savedPerson.LastName);
            Assert.Equal(person.Alias, savedPerson.Alias);
            Assert.Equal(person.AgeRange, savedPerson.AgeRange);
            Assert.Equal(person.Associates.Count(), savedAssociate.Count());
        }

        /// <summary>
        /// Creates the person with phones.
        /// </summary>
        [Fact]
        public void CreatePersonWithPhones()
        {
            //Arrange
            var personSearch = MockDataFactory.GetPersonSearch();
            Repository.Create(personSearch);
            Repository.Save();

            //Act
            var person = MockDataFactory.GetPerson(personSearch.Id);
            Repository.Create(person);
            Repository.Save();

            var savedPerson = Repository.GetFirst<Person>(x => x.Id == person.Id);
            var savedPhones = Repository.Get<Phone>(x => x.PersonId == person.Id);

            //Assert
            Assert.Equal(person.Id, savedPerson.Id);
            Assert.Equal(person.FirstName, savedPerson.FirstName);
            Assert.Equal(person.LastName, savedPerson.LastName);
            Assert.Equal(person.Alias, savedPerson.Alias);
            Assert.Equal(person.AgeRange, savedPerson.AgeRange);
            Assert.Equal(person.Phones.Count(), savedPhones.Count());
        }
    }
}