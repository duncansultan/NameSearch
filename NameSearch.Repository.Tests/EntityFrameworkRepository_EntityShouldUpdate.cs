using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using NameSearch.Context;
using NameSearch.Models.Entities;
using Xunit;


namespace NameSearch.Repository.Tests
{
    public class EntityFrameworkRepository_EntityShouldUpdate
    {
        private readonly EntityFrameworkRepository Repository;

        public EntityFrameworkRepository_EntityShouldUpdate()
        {
            Repository = new EntityFrameworkRepository(new ApplicationDbContext());
        }

        [Fact]
        public void UpdateName()
        {
            var name = Repository.GetFirst<Name>();
            var id = name.Id;
            name.Description = "Test";
            Repository.Update(name);
            Repository.Save();

            var updatedName = Repository.GetFirst<Name>(x => x.Id == id);
            Assert.Equal("Test", updatedName.Description);
        }

        [Fact]
        public void UpdatePersonWithAddresses()
        {
            var person = Repository.GetFirst<Person>();
            var id = person.Id;
            person.Age = 66;
            Repository.Update(person);
            Repository.Save();

            var updatedPerson = Repository.GetFirst<Person>(x => x.Id == id);
            Assert.Equal(66, updatedPerson.Age);
        }
    }
}
