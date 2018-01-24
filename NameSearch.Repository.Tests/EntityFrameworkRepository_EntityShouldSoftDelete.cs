using System;
using NameSearch.Context;
using NameSearch.Models.Entities;
using Xunit;

namespace NameSearch.Repository.Tests
{
    /// <summary>
    ///     Unit tests for Deleting Entities
    /// </summary>
    public class EntityFrameworkRepository_EntityShouldSoftDelete
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository_EntityShouldSoftDelete"/> class.
        /// </summary>
        public EntityFrameworkRepository_EntityShouldSoftDelete()
        {
            Repository = new EntityFrameworkRepository(new ApplicationDbContext());
            InitialDataBuilder.Build(Repository);
        }

        /// <summary>
        /// Deletes the name import.
        /// </summary>
        [Fact]
        public void DeleteNameImport()
        {
            //Arrange
            var nameImport = Repository.GetFirst<NameImport>();

            //Act
            Repository.Delete(nameImport);
            Repository.Save();

            //Assert
            Assert.False(nameImport.IsActive);
            Assert.NotNull(nameImport.ModifiedDateTime);
            Assert.True(nameImport.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Deletes the name.
        /// </summary>
        [Fact]
        public void DeleteName()
        {
            //Arrange
            var name = Repository.GetFirst<Name>();

            //Act
            Repository.Delete(name);
            Repository.Save();

            //Assert
            Assert.False(name.IsActive);
            Assert.NotNull(name.ModifiedDateTime);
            Assert.True(name.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Deletes the person search job.
        /// </summary>
        [Fact]
        public void DeletePersonSearchJob()
        {
            //Arrange
            var personSearchJob = Repository.GetFirst<PersonSearchJob>();

            //Act
            Repository.Delete(personSearchJob);
            Repository.Save();

            //Assert
            Assert.False(personSearchJob.IsActive);
            Assert.NotNull(personSearchJob.ModifiedDateTime);
            Assert.True(personSearchJob.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Deletes the person.
        /// </summary>
        [Fact]
        public void DeletePerson()
        {
            //Arrange
            var person = Repository.GetFirst<Person>();

            //Act
            Repository.Delete(person);
            Repository.Save();

            //Assert
            Assert.False(person.IsActive);
            Assert.NotNull(person.ModifiedDateTime);
            Assert.True(person.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Deletes the address.
        /// </summary>
        [Fact]
        public void DeleteAddress()
        {
            //Arrange
            var address = Repository.GetFirst<Address>();

            //Act
            Repository.Delete(address);
            Repository.Save();

            //Assert
            Assert.False(address.IsActive);
            Assert.NotNull(address.ModifiedDateTime);
            Assert.True(address.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Deletes the associate.
        /// </summary>
        [Fact]
        public void DeleteAssociate()
        {
            //Arrange
            var associate = Repository.GetFirst<Associate>();

            //Act
            Repository.Delete(associate);
            Repository.Save();

            //Assert
            Assert.False(associate.IsActive);
            Assert.NotNull(associate.ModifiedDateTime);
            Assert.True(associate.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Deletes the phone.
        /// </summary>
        [Fact]
        public void DeletePhone()
        {
            //Arrange
            var phone = Repository.GetFirst<Phone>();

            //Act
            Repository.Delete(phone);
            Repository.Save();

            //Assert
            Assert.False(phone.IsActive);
            Assert.NotNull(phone.ModifiedDateTime);
            Assert.True(phone.ModifiedDateTime.Value.Date == DateTime.Today);
        }
    }
}
