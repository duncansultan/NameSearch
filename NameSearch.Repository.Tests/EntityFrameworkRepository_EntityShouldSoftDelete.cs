using System.IO;
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
            //var name = new Name();
            //name.Value = "Duncan";
            //name.Description = null;
            //name.SearchPriorityLevel = 1;
            //var contextObj = Repository.Create(name);
            //Repository.Save();

            //var exists = Repository.GetExists<Name>(x => x.Id == name.Id);
            //Assert.True(exists);
        }

        [Fact]
        public void DeletePersonWithAddresses()
        {
            var person = new Person();
            //name.Value = "Duncan";
            //name.Description = null;
            //name.SearchPriorityLevel = 1;
            var contextObj = Repository.Create(person);
            Repository.Save();

            var exists = Repository.GetExists<Name>(x => x.Id == person.Id);
            Assert.True(exists);
        }
    }
}
