using NameSearch.App.Factories;
using NameSearch.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NameSearch.App.Tests.Factories
{
    /// <summary>
    /// Unit tests for PeopleFactory
    /// </summary>
    public class PeopleFactoryTests
    {
        /// <summary>
        /// Gets the valid input return people.
        /// </summary>
        [Fact]
        public void Get_ValidInput_ReturnPeople()
        {
            // Arrange
            var personEntity = MockDataFactory.GetTestPerson();
            // Act
            var result = PeopleFactory.Get(personEntity).ToList();
            // Assert
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count(), personEntity.Addresses.Count());
            Assert.Equal(result.First().FirstName, personEntity.FirstName);
            Assert.Equal(result.First().LastName, personEntity.LastName);
            Assert.Equal(result.First().Phone, personEntity.Phones?.FirstOrDefault().PhoneNumber);
            Assert.Equal(result.First().AgeRange, personEntity.AgeRange);

            foreach (var addressEntity in personEntity.Addresses)
            {
                var exists = result.Exists(x => x.Address1 == addressEntity.Address1
                && x.Address2 == addressEntity.Address2
                && x.City == addressEntity.City
                && x.State == addressEntity.State
                && x.Zip == addressEntity.Zip
                && x.Plus4 == addressEntity.Plus4
                && x.Country == addressEntity.Country
                && x.Latitude == addressEntity.Latitude
                && x.Longitude == addressEntity.Longitude);

                Assert.True(exists);
            }
        }
    }
}