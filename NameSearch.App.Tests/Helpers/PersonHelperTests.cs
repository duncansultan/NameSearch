using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Helpers;
using NameSearch.App.Services;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Domain;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace NameSearch.App.Tests.Helpers
{
    /// <summary>
    /// Unit tests for PersonHelper
    /// </summary>
    public class PersonHelperTests
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The people search person helper
        /// </summary>
        private readonly PersonHelper PersonHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonHelperTests"/> class.
        /// </summary>
        public PersonHelperTests()
        {
            MockRepository = MockRepositoryFactory.Get();

            var mapper = MapperFactory.Get();
            PersonHelper = new PersonHelper(MockRepository.Object, mapper);
        }

        /// <summary>
        /// Gets the people valid input return people.
        /// </summary>
        [Fact]
        public void GetPeople_ReturnPeople()
        {
            // Arrange
            // Act
            var result = PersonHelper.GetPeople();
            // Assert
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
            Assert.NotNull(result);
            MockRepository.Verify(c => c.GetAll<Models.Entities.Person>(null, It.IsAny<string>(), null, null), Times.Once);
        }
    }
}