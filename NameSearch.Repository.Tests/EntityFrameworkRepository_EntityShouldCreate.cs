using System.Collections.Generic;
using System.Linq;
using NameSearch.Context;
using NameSearch.Models.Entities;
using Xunit;

namespace NameSearch.Repository.Tests
{
    public class EntityFrameworkRepository_EntityShouldCreate
    {
        private readonly EntityFrameworkRepository Repository;

        public EntityFrameworkRepository_EntityShouldCreate()
        {
            Repository = new EntityFrameworkRepository(new ApplicationDbContext());
        }

        [Fact]
        public void CreateName()
        {
            var name = new SearchName();
            name.Value = "Duncan";
            name.Description = null;
            var contextObj = Repository.Create(name);
            Repository.Save();

            var exists = Repository.GetExists<SearchName>(x => x.Id == name.Id);
            Assert.True(exists);
        }

        [Fact]
        public void CreatePersonWithAddresses()
        {
            var person = new Person();
            person.FirstName = "Duncan";
            person.LastName = "Sultan";
            person.Alias = "Sultan of Swing";
            person.Age = 33;
            person.Addresses = new List<Address>();

            var firstAddress = new Address();
            firstAddress.Address1 = "123 Oak Rd";
            firstAddress.Address2 = "";
            firstAddress.City = "Dallas";
            firstAddress.State = "TX";
            firstAddress.Zip = "75083";
            firstAddress.Plus4 = "1234";
            firstAddress.Country = "US";
            firstAddress.Longitude = null;
            firstAddress.Lattitue = null;
            firstAddress.IsCurrent = true;
            person.Addresses.Add(firstAddress);

            var secondAddress = new Address();
            secondAddress.Address1 = "456 Sunny Trail Court";
            secondAddress.Address2 = "";
            secondAddress.City = "Frisco";
            secondAddress.State = "TX";
            secondAddress.Zip = "12346";
            secondAddress.Plus4 = "";
            secondAddress.Country = "US";
            secondAddress.Lattitue = 40.712775;
            secondAddress.Longitude = -74.005973;
            secondAddress.IsCurrent = true;
            person.Addresses.Add(secondAddress);

            var thirdAddress = new Address();
            thirdAddress.Address1 = "123 Falcon Drive";
            thirdAddress.Address2 = "#123";
            thirdAddress.City = "Raleigh";
            thirdAddress.State = "NC";
            thirdAddress.Zip = "12346";
            thirdAddress.Plus4 = null;
            thirdAddress.Country = "US";
            thirdAddress.Lattitue = 32.776664;
            thirdAddress.Longitude = -96.796988;
            thirdAddress.IsCurrent = true;
            person.Addresses.Add(thirdAddress);

            var contextObj = Repository.Create(person);
            Repository.Save();

            var exists = Repository.GetExists<Person>(x => x.Id == person.Id);
            Assert.True(exists, "Person does not exist in database.");

            var savedPerson = Repository.GetById<Person>(person.Id);
            var anyAddresses = savedPerson.Addresses.Any();
            Assert.True(anyAddresses, "Person has no Addresses.");
        }
    }
}
