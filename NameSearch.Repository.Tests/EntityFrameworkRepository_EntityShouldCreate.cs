using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
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
            var name = new Name();
            name.Value = "Duncan";
            name.Description = null;
            name.SearchPriorityLevel = (int)Models.Domain.SearchPriorityTypes.High;
            var contextObj = Repository.Create(name);
            Repository.Save();

            var exists = Repository.GetExists<Name>(x => x.Id == name.Id);
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
            firstAddress.Address2 = "Dallas";
            firstAddress.City = "TX";
            firstAddress.Zip = "75083";
            firstAddress.Plus4 = "1234";
            firstAddress.Country = "US";
            firstAddress.Longitude = null;
            firstAddress.Lattitue = null;
            firstAddress.IsCurrent = true;

            var secondAddress = new Address();
            firstAddress.Address1 = "456 Sunny Trail Court";
            firstAddress.Address2 = "Frisco";
            firstAddress.City = "TX";
            firstAddress.Zip = "12346";
            firstAddress.Plus4 = "";
            firstAddress.Country = "US";
            firstAddress.Lattitue = 40.712775;
            firstAddress.Longitude = -74.005973;
            firstAddress.IsCurrent = true;

            var thirdAddress = new Address();
            firstAddress.Address1 = "123 Falcon Drive";
            firstAddress.Address2 = "Raleigh";
            firstAddress.City = "NC";
            firstAddress.Zip = "12346";
            firstAddress.Plus4 = null;
            firstAddress.Country = "US";
            firstAddress.Lattitue = 32.776664;
            firstAddress.Longitude = -96.796988;
            firstAddress.IsCurrent = true;

            var contextObj = Repository.Create(person);
            Repository.Save();

            var exists = Repository.GetExists<Person>(x => x.Id == person.Id);
            Assert.True(exists, "Person does not exist in database.");

            var savedPerson = Repository.GetById<Person>(person.Id);
            var anyAddresses = savedPerson.Addresses.Any();
            Assert.True(anyAddresses, "Person has no Addresses.");
            //Assert.Collection(savedPerson.Addresses, )
        }
    }
}
