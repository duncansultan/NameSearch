using System.IO;
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
            name.SearchPriorityLevel = 1;
            var contextObj = Repository.Create(name);
            Repository.Save();

            var exists = Repository.GetExists<Name>(x => x.Id == name.Id);
            Assert.True(exists);
        }
    }
}
