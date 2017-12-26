using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using NameSearch.Context;
using NameSearch.Models.Entities;
using Xunit;


namespace NameSearch.Repository.Tests
{
    public class EntityFrameworkRepository_EntityShouldSoftDelete
    {
        private readonly EntityFrameworkRepository Repository;

        public EntityFrameworkRepository_EntityShouldSoftDelete()
        {
            Repository = new EntityFrameworkRepository(new ApplicationDbContext());
        }

        [Fact]
        public void DeleteName()
        {
            var name = Repository.GetFirst<Name>();
            var id = name.Id;
            Repository.Delete(name);
            Repository.Save();

            var exists = Repository.GetExists<Name>(x => x.Id == id);
            Assert.False(exists);
        }

        [Fact]
        public void DeletePersonWithAddresses()
        {
            var person = Repository.GetFirst<Person>(includeProperties: "Addresses");
            var id = person.Id;
            Repository.Delete(person);
            Repository.Save();
            Repository.Save();

            var exists = Repository.GetExists<Person>(x => x.Id == id);
            Assert.False(exists);
            var anyAddresses = Repository.Get<Address>(x => x.PersonId == id);

            Assert.False(anyAddresses.Any());


        }
    }
}
