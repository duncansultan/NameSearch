﻿using NameSearch.Context;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using NameSearch.Repository.Tests.Mocks;
using System;
using Xunit;

namespace NameSearch.Repository.Tests
{
    /// <summary>
    ///     Unit tests for Updating Entities
    /// </summary>
    public class EntityFrameworkRepository_EntityShouldUpdate
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository_EntityShouldUpdate"/> class.
        /// </summary>
        public EntityFrameworkRepository_EntityShouldUpdate()
        {
            Repository = new EntityFrameworkRepository(new ApplicationDbContext());
            InitialDataBuilder.Build(Repository);
        }

        /// <summary>
        /// Updates the address.
        /// </summary>
        [Fact]
        public void UpdateAddress()
        {
            //Arrange
            var address = Repository.GetFirst<Address>();
            address.Address1 = $"Updated-{address.Address1}";
            address.Address2 = $"Updated-{address.Address2}";
            address.City = $"Updated-{address.City}";
            address.State = $"Updated-{address.State}";
            address.Zip = $"Updated-{address.Zip}";
            address.Plus4 = $"Updated-{address.Plus4}";
            address.IsCurrent = !address.IsCurrent;
            address.IsHistorical = !address.IsHistorical;
            address.Accuracy = $"Updated-{address.Accuracy}";

            //Act
            Repository.Update(address);
            Repository.Save();

            //Assert
            Assert.NotNull(address.ModifiedDateTime);
            Assert.True(address.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Updates the associate.
        /// </summary>
        [Fact]
        public void UpdateAssociate()
        {
            //Arrange
            var associate = Repository.GetFirst<Associate>();
            associate.ExternalId = $"Updated-{associate.ExternalId}";
            associate.Name = $"Updated-{associate.Name}";
            associate.Relation = $"Updated-{associate.Relation}";

            //Act
            Repository.Update(associate);
            Repository.Save();

            //Assert
            Assert.NotNull(associate.ModifiedDateTime);
            Assert.True(associate.ModifiedDateTime.Value.Date == DateTime.Today);
        }
        /// <summary>
        /// Updates the person.
        /// </summary>
        [Fact]
        public void UpdatePerson()
        {
            //Arrange
            var person = Repository.GetFirst<Person>();
            person.AgeRange = "23-32";
            person.LastName = $"Updated-{person.LastName}";
            person.FirstName = $"Updated-{person.FirstName}";
            person.Alias = $"Updated-{person.Alias}";

            //Act
            Repository.Update(person);
            Repository.Save();

            //Assert
            Assert.NotNull(person.ModifiedDateTime);
            Assert.True(person.ModifiedDateTime.Value.Date == DateTime.Today);
        }

        /// <summary>
        /// Updates the phone.
        /// </summary>
        [Fact]
        public void UpdatePhone()
        {
            //Arrange
            var phone = Repository.GetFirst<Phone>();
            phone.ExternalId = $"Updated-{phone.ExternalId}";
            phone.PhoneNumber = $"Updated-{phone.PhoneNumber}";

            //Act
            Repository.Update(phone);
            Repository.Save();

            //Assert
            Assert.NotNull(phone.ModifiedDateTime);
            Assert.True(phone.ModifiedDateTime.Value.Date == DateTime.Today);
        }
    }
}